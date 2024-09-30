using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Listas;

namespace Datos
{
    public class dClientes : conexion
    {
        public DataTable listarClientes()//muestra usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "listarClientes";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
        public void agregarCliente(string nombre, string empresa, string calle, string codigoPostal, string telefono, string municipio, string estado, string rfc, int idTipoPrecio, int plazoPago, 
            string observaciones, decimal adeudo)//agrega usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "agregarCliente";
                    command.Parameters.AddWithValue("nombre", nombre.ToUpper());
                    command.Parameters.AddWithValue("nombreEmpresa", empresa.ToUpper());
                    command.Parameters.AddWithValue("calle", calle.ToUpper());
                    command.Parameters.AddWithValue("codigoPostal", codigoPostal.ToUpper());
                    command.Parameters.AddWithValue("telefono", telefono.ToUpper());
                    command.Parameters.AddWithValue("municipio", municipio.ToUpper());
                    command.Parameters.AddWithValue("estado", estado.ToUpper());
                    command.Parameters.AddWithValue("rfc", rfc.ToUpper());
                    command.Parameters.AddWithValue("idTipoPrecio", idTipoPrecio);
                    command.Parameters.AddWithValue("plazoPago", plazoPago);
                    command.Parameters.AddWithValue("observaciones", observaciones.ToUpper());
                    command.Parameters.AddWithValue("adeudo", adeudo);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void eliminarCliente(int idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "eliminarCliente";
                    command.Parameters.AddWithValue("idCliente", idCliente);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void modificarClientes(int idCliente, string nombre, string empresa, string calle, string codigoPostal, string telefono, string municipio, string estado, string rfc, int idTipoPrecio, int plazoPago, 
            string observaciones, decimal adeudo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "modificarCliente";
                    command.Parameters.AddWithValue("idCliente", idCliente);
                    command.Parameters.AddWithValue("nombre", nombre.ToUpper());
                    command.Parameters.AddWithValue("nombreEmpresa", empresa.ToUpper());
                    command.Parameters.AddWithValue("calle", calle.ToUpper());
                    command.Parameters.AddWithValue("codigoPostal", codigoPostal.ToUpper());
                    command.Parameters.AddWithValue("telefono", telefono.ToUpper());
                    command.Parameters.AddWithValue("municipio", municipio.ToUpper());
                    command.Parameters.AddWithValue("estado", estado.ToUpper());
                    command.Parameters.AddWithValue("rfc", rfc.ToUpper());
                    command.Parameters.AddWithValue("idTipoPrecio", idTipoPrecio);
                    command.Parameters.AddWithValue("plazoPago", plazoPago);
                    command.Parameters.AddWithValue("observaciones", observaciones.ToUpper());
                    command.Parameters.AddWithValue("adeudo", adeudo);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void buscarCliente(int idCliente, out string nombreCliente, out int idTipoPrecio, out string plazoPago)
        {
            nombreCliente = "-";
            plazoPago = "0";
            idTipoPrecio = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select nombre, idTipoPrecio, plazoPago from clientes where idCliente=@idCliente";
                    command.Parameters.AddWithValue("idCliente", idCliente);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        nombreCliente = reader["nombre"].ToString();
                        idTipoPrecio = Convert.ToInt32(reader["idTipoPrecio"]);
                        plazoPago = reader["plazoPago"].ToString();
                    }
                }
            }
        }
        public List<listaCliente>listaClienteNotaVenta(int folioVenta)
        {
            List<listaCliente> lista = new List<listaCliente>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select c.nombre,c.nombreEmpresa,c.calle,c.codigoPostal,c.telefono,c.municipio,c.estado,c.municipio,c.estado,c.rfc,c.adeudo from ventas v " +
                        "left join clientes c on v.idCliente=c.idCliente where folio=@folioVenta;";
                    command.Parameters.AddWithValue("@folioVenta", folioVenta);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var listaTemp = new listaCliente()
                        {
                            nombre = reader["nombre"].ToString(),
                            empresa = reader["nombreEmpresa"].ToString(),
                            colonia = reader["calle"].ToString(),
                            codigoPostal = reader["codigoPostal"].ToString(),
                            telefono = reader["telefono"].ToString(),
                            municipio = reader["municipio"].ToString(),
                            estado = reader["estado"].ToString(),
                            rfc = reader["rfc"].ToString(),
                            adeudos = Convert.ToDecimal(reader["adeudo"]),
                            idCliente=0,
                            plazoPago=0
                        };
                        lista.Add(listaTemp);
                    }
                }
            }

            return lista;
        }
        public List<listaCliente> listaClienteCotizacion(int folioCotizacion)
        {
            List<listaCliente> lista = new List<listaCliente>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select c.nombre,c.nombreEmpresa,c.calle,c.codigoPostal,c.telefono,c.municipio,c.estado,c.municipio,c.estado,c.rfc,c.adeudo from cotizaciones co " +
                        "left join clientes c on co.idCliente=c.idCliente where folio=@folio;";
                    command.Parameters.AddWithValue("@folio", folioCotizacion);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var listaTemp = new listaCliente()
                        {
                            nombre = reader["nombre"].ToString(),
                            empresa = reader["nombreEmpresa"].ToString(),
                            colonia = reader["calle"].ToString(),
                            codigoPostal = reader["codigoPostal"].ToString(),
                            telefono = reader["telefono"].ToString(),
                            municipio = reader["municipio"].ToString(),
                            estado = reader["estado"].ToString(),
                            rfc = reader["rfc"].ToString(),
                            adeudos = Convert.ToDecimal(reader["adeudo"]),
                            idCliente = 0,
                            plazoPago = 0
                        };
                        lista.Add(listaTemp);
                    }
                }
            }

            return lista;
        }
        public int ultimoId()//muestra usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    int result = 0;
                    command.Connection = connection;
                    command.CommandText = "select isnull(max(idCliente),1) as idCliente from clientes;";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        result = Convert.ToInt32(reader["idCliente"]);
                    }
                    return result+1;
                }
            }
        }
    }
}
