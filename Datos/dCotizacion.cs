using Datos.Listas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dCotizacion : conexion
    {
        public bool agregarCotizacion(int folio, DateTime fechaCotizacion, DateTime expiracion, int plazoPago, int cantidad, decimal subtotal, decimal total, decimal totalIva, int descuento, decimal precioDescuento, int idEmpleado, int idVendedor, int idCliente, List<listaVentaDetalle> listaDetalle)
        {
            bool result = false;
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaccion = connection.BeginTransaction();
                try
                {
                    string script = "insert into cotizaciones output inserted.idCotizacion values (@folio, getdate(),@fechaPago,@cantidad,@plazoPago,@subtotal,@total,@totalIva,@descuento,@precioDescuento,@idEmpleado,@idVendedor,@idCliente,'PENDIENTE')";
                    SqlCommand venta = new SqlCommand(script, connection, transaccion);
                    venta.Parameters.AddWithValue("@folio", folio);
                    venta.Parameters.AddWithValue("@plazoPago", plazoPago);
                    venta.Parameters.AddWithValue("@fechaPago", fechaCotizacion);
                    venta.Parameters.AddWithValue("@fechaEntrega", expiracion);
                    venta.Parameters.AddWithValue("@cantidad", cantidad);
                    venta.Parameters.AddWithValue("@subtotal", subtotal);
                    venta.Parameters.AddWithValue("@total", total);
                    venta.Parameters.AddWithValue("@totalIva", totalIva);
                    venta.Parameters.AddWithValue("@descuento", descuento);
                    venta.Parameters.AddWithValue("@precioDescuento", precioDescuento);
                    venta.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    venta.Parameters.AddWithValue("@idVendedor", idVendedor);
                    venta.Parameters.AddWithValue("@idCliente", idCliente);

                    // Obtener el ID de la venta insertada
                    int cotizacionID = (int)venta.ExecuteScalar();


                    string scriptDetalle = "insert into cotizacionDetalle values(@idProducto,@cantidad,@precioUnitario, @total,@idTipoPrecio,@idCotizacion);";
                        
                    foreach (var detalle in listaDetalle)
                    {
                        SqlCommand detalleCommand = new SqlCommand(scriptDetalle, connection, transaccion);
                        detalleCommand.Parameters.AddWithValue("@idProducto", detalle.idLote);
                        detalleCommand.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                        detalleCommand.Parameters.AddWithValue("@precioUnitario", detalle.precioUnitario);
                        detalleCommand.Parameters.AddWithValue("@total", detalle.total);
                        detalleCommand.Parameters.AddWithValue("@idTipoPrecio", detalle.idTipoPrecio);
                        detalleCommand.Parameters.AddWithValue("@idCotizacion", cotizacionID);

                        detalleCommand.ExecuteNonQuery();
                    }

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
        public int ultimoFolioCotizacion()
        {
            int folio = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select isnull(max(folio),0)as folio from cotizaciones";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        folio = Convert.ToInt32(reader["folio"]);
                    }

                }
            }
            return folio + 1;
        }

        public DataTable buscarCotización(int folio, out string mensaje)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "buscarCotizacion";
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.Add("@mensaje", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    DataTable tabla = new DataTable();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        tabla.Load(reader);
                    }

                    // Ahora que el SqlDataReader está cerrado, se puede acceder al parámetro de salida
                    mensaje = command.Parameters["@mensaje"].Value.ToString();
                    return tabla;
                }
            }
        }

        public List<listaVentaDetalleNota> cotizacionDetalleNota(int folioCotizacion)
        {
            List<listaVentaDetalleNota> lista = new List<listaVentaDetalleNota>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT p.codigo,p.descripcion, cd.cantidad,cd.precioUnitario, cd.total, tp.descripcion as tipoPrecio from cotizacionDetalle cd " +
                        "left join productos p on cd.idProducto = p.idProducto left join tipoPrecio tp on cd.idTipoPrecio = tp.idTipoPrecio left join cotizaciones c on cd.idCotizacion = c.idCotizacion where c.folio = @folio; ";
                    command.Parameters.AddWithValue("@folio", folioCotizacion);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var listaTemp = new listaVentaDetalleNota()
                        {
                            codigo = reader["codigo"].ToString(),
                            producto = reader["descripcion"].ToString(),
                            lote = "-",
                            cantidad = Convert.ToInt32(reader["cantidad"]),
                            precioUnitario = Convert.ToDecimal(reader["precioUnitario"]),
                            importe = Convert.ToDecimal(reader["total"]),
                            tipoPrecio = reader["tipoPrecio"].ToString()
                        };
                        lista.Add(listaTemp);
                    }
                }
            }
            return lista;
        }
        public List<listaVentaGeneralNota> cotizacionGeneralNota(int folioCotizacion)
        {
            List<listaVentaGeneralNota> lista = new List<listaVentaGeneralNota>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select c.idCliente, c.fechaCotizacion,c.expiracion,c.cantidad,c.subtotal,c.total,c.precioDescuento,c.descuento,c.plazoPago from cotizaciones c where folio=@folio;";
                    command.Parameters.AddWithValue("@folio", folioCotizacion);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var listaTemp = new listaVentaGeneralNota()
                        {
                            folioVenta = folioCotizacion.ToString(),
                            idCliente = "0",
                            fechaVenta = Convert.ToDateTime(reader["fechaCotizacion"]),
                            fechaVencimiento = Convert.ToDateTime(reader["expiracion"]),
                            cantidad = Convert.ToInt32(reader["cantidad"]),
                            subtotal = Convert.ToDecimal(reader["subtotal"]),
                            total = Convert.ToDecimal(reader["total"]),
                            precioDescuento = Convert.ToDecimal(reader["precioDescuento"]),
                            descuento = Convert.ToInt32(reader["descuento"]),
                            plazoPago = Convert.ToInt32(reader["plazoPago"]),
                            importeConLetra = dConversor.NumeroALetras(Convert.ToDecimal(reader["total"])),
                            tipoOperacion = "COTIZACIÓN"
                        };
                        lista.Add(listaTemp);
                    }
                }
            }
            return lista;
        }
    }
}
