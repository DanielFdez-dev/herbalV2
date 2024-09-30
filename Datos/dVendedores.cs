using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dVendedores : conexion
    {
        public DataTable listarVendedores()//muestra usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "listarVendedores";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
        public void agregarVendedor(string nombre, int comision, int idEstado)//agrega usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "agregarVendedor";
                    command.Parameters.AddWithValue("nombre", nombre.ToUpper());
                    command.Parameters.AddWithValue("comision", comision);
                    command.Parameters.AddWithValue("idEstado", idEstado);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void eliminarVendedor(int idVendedor)//elimina usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "eliminarVendedor";
                    command.Parameters.AddWithValue("idVendedor", idVendedor);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void modificarVendedor(int idVendedor, string nombre, int comision, int idEstado)//modificar usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "modificarVendedor";
                    command.Parameters.AddWithValue("idVendedor", idVendedor);
                    command.Parameters.AddWithValue("nombre", nombre.ToUpper());
                    command.Parameters.AddWithValue("comision", comision);
                    command.Parameters.AddWithValue("idEstado", idEstado);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void buscarVendedor(int idVendedor, out string nombreVendedor)
        {
            nombreVendedor = "-";
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select nombre from vendedores where idVendedor=@idVendedor";
                    command.Parameters.AddWithValue("idVendedor", idVendedor);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        nombreVendedor = reader["nombre"].ToString();
                    }
                }
            }
        }
    }
}
