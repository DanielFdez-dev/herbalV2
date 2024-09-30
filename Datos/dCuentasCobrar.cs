using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dCuentasCobrar : conexion
    {
        public DataTable listarCuentas()//muestra usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "listarCuentasCobrar";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
        public DataTable listarCuentasCobrarDetalle(int idCuentaCobrar)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select idCuentaCobrarDetalle,fechaAbono,cantidad,observaciones,validacion1,validacion2, tp.descripcion as tipoPago, ib.descripcion as banco from cuentaCobrarDetalle ccd " +
                        "left join tipoPago tp on ccd.idTipoPago=tp.idTipoPago left join institucionBancaria ib on ccd.idBanco=ib.idBanco where idCuentaCobrar=@idCuentaCobrar";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idCuentaCobrar", idCuentaCobrar);
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        } 
        public bool agregarAbono(int idCuentaCobrar, decimal cantidad, int idTipoPago, string observaciones, int idCliente, byte[] documento, string nombreArchivo, string extensionArchivo, int idBanco, string tipoAbono)
        {
            bool result = false;
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaccion = connection.BeginTransaction();
                try
                {
                    string scriptCuentasCobrarDetalle = "insert into cuentaCobrarDetalle values(GETDATE(),@idTipoPago,@cantidad,@idCuentaCobrar,@observaciones, " +
                        "0,0,@documento,@nombreArchivo,@extensionArchivo,@idBanco,(select adeudo from cuentasCobrar where idCuentaCobrar=@idCuentaCobrar)-@cantidad,@tipoAbono);";
                    SqlCommand cuentaCobrarCommand = new SqlCommand(scriptCuentasCobrarDetalle, connection, transaccion);
                    cuentaCobrarCommand.Parameters.AddWithValue("@idTipoPago", idTipoPago);
                    cuentaCobrarCommand.Parameters.AddWithValue("@cantidad", cantidad);
                    cuentaCobrarCommand.Parameters.AddWithValue("@idCuentaCobrar", idCuentaCobrar);
                    cuentaCobrarCommand.Parameters.AddWithValue("@observaciones", observaciones);
                    cuentaCobrarCommand.Parameters.AddWithValue("@idBanco", idBanco);
                    cuentaCobrarCommand.Parameters.AddWithValue("@tipoAbono", tipoAbono);
                    if (documento == null || documento.Length == 0)
                    {
                        cuentaCobrarCommand.Parameters.Add("@documento", SqlDbType.VarBinary).Value = DBNull.Value;
                        cuentaCobrarCommand.Parameters.Add("@nombreArchivo", SqlDbType.VarChar, 20).Value = "N/A";
                        cuentaCobrarCommand.Parameters.Add("@extensionArchivo", SqlDbType.VarChar, 5).Value = "N/A";
                    }
                    else
                    {
                        cuentaCobrarCommand.Parameters.Add("@documento", SqlDbType.VarBinary).Value = documento;
                        cuentaCobrarCommand.Parameters.Add("@nombreArchivo", SqlDbType.VarChar,20).Value = nombreArchivo;
                        cuentaCobrarCommand.Parameters.Add("@extensionArchivo", SqlDbType.VarChar, 5).Value = extensionArchivo;
                    }
                    cuentaCobrarCommand.ExecuteNonQuery();


                    string scriptCuentaCobrarCommand = "update cuentasCobrar set fechaAbono=getdate(), adeudo=adeudo-@cantidad where idCuentaCobrar=@idCuentaCobrar;";
                    SqlCommand cuentacobrarCommand = new SqlCommand(scriptCuentaCobrarCommand, connection, transaccion);
                    cuentacobrarCommand.Parameters.AddWithValue("@cantidad", cantidad);
                    cuentacobrarCommand.Parameters.AddWithValue("@idCuentaCobrar", idCuentaCobrar);
                    cuentacobrarCommand.ExecuteNonQuery();

                    string scriptClienteCommand = "update clientes set adeudo=adeudo-@cantidad where idCliente =@idCliente";
                    SqlCommand clienteCommand = new SqlCommand(scriptClienteCommand, connection, transaccion);
                    clienteCommand.Parameters.AddWithValue("@cantidad", cantidad);
                    clienteCommand.Parameters.AddWithValue("@idCliente", idCliente);
                    clienteCommand.ExecuteNonQuery();

                    string scriptValidacion = "if (select adeudo from cuentasCobrar where idCuentaCobrar=@idCuentaCobrar)<=0 begin " +
                                                "update cuentasCobrar set fechaSaldada=getdate(), activo=0 where idCuentaCobrar=@idCuentaCobrar; end";
                    SqlCommand validacionCommand = new SqlCommand(scriptValidacion, connection, transaccion);
                    validacionCommand.Parameters.AddWithValue("@idCuentaCobrar", idCuentaCobrar);
                    validacionCommand.ExecuteNonQuery();

                    // Confirmar la transacción
                    transaccion.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    transaccion.Rollback();
                    result = false;
                }
            }

            return result;
        }
        public DataTable listarValidacionesPendientes()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select ccd.idCuentaCobrarDetalle,c.nombre,ccd.fechaAbono, tp.descripcion as tipoPago, ib.descripcion as Banco,ccd.observaciones, " +
                        "ccd.cantidad,ccd.validacion1,ccd.validacion2,ccd.nombreDocumento,ccd.extensionDocumento " +
                        "from cuentaCobrarDetalle ccd left join cuentasCobrar cc on ccd.idCuentaCobrar = cc.idCuentaCobrar left join ventas v on cc.folioVenta = v.folio " +
                        "left join clientes c on v.idCliente = c.idCliente left join tipoPago tp on ccd.idTipoPago = tp.idTipoPago " +
                        "left join institucionBancaria ib on ccd.idBanco=ib.idBanco where ccd.validacion1=0 or ccd.validacion2=0; ";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    
                    return tabla;
                }
            }
        }

        public void validacion1(int idCuentaCobrarDetalle)
        {
            using(var connection = GetConnection())
            {
                connection.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update cuentaCobrarDetalle set validacion1=1 where idCuentacobrarDetalle=@idCuentaCobrarDetalle";
                    command.Parameters.AddWithValue("@idCuentaCobrarDetalle", idCuentaCobrarDetalle);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void validacion2(int idCuentaCobrarDetalle)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update cuentaCobrarDetalle set validacion2=1 where idCuentaCobrarDetalle=@idCuentaCobrarDetalle";
                    command.Parameters.AddWithValue("@idCuentaCobrarDetalle", idCuentaCobrarDetalle);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        public byte[] descargaArchivo(int idCuentaCobrarDetalle)
        {
            byte[] fileBytes = null;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select documento from cuentaCobrarDetalle where idCuentaCobrarDetalle=@idCuentaCobrarDetalle";
                    command.Parameters.AddWithValue("@idCuentaCobrarDetalle", idCuentaCobrarDetalle);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fileBytes = reader["documento"] as byte[];
                        }
                    }
                }
            }
            return fileBytes;
        }
    }
}
