using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dLotes : conexion
    {
        public DataTable listarLotes(int idProducto)//muestra usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "listarLotes";
                    command.Parameters.AddWithValue("idProducto", idProducto);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
        public void agregarLote(string lote, DateTime caducidad, int stock, int idProducto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "agregarLotes";
                    command.Parameters.AddWithValue("lote", lote);
                    command.Parameters.AddWithValue("caducidad", caducidad);
                    command.Parameters.AddWithValue("stock", stock);
                    command.Parameters.AddWithValue("idProducto", idProducto);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void modificarLote(int idLote, DateTime caducidad, int stock, int idProducto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "modificarLote";
                    command.Parameters.AddWithValue("idLote", idLote);
                    command.Parameters.AddWithValue("caducidad", caducidad);
                    command.Parameters.AddWithValue("stock", stock);
                    command.Parameters.AddWithValue("idProducto", idProducto);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void eliminarLote(int idLote, int idProducto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "eliminarLote";
                    command.Parameters.AddWithValue("idLote", idLote);
                    command.Parameters.AddWithValue("idProducto", idProducto);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void validarStocks()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "validarStocks";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
