using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dProductos : conexion
    {
        public DataTable listarProductos()//muestra usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "listarProductos";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
        public void agregarProducto(string codigo, string amecop, string descripcion, int stock, decimal precioCosto, decimal precioLab, decimal precioDistribuidor, decimal precioMayoreo, 
            decimal precioLista, int iva, int idClasificacion, int idMarca)//agrega usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "agregarProducto";
                    command.Parameters.AddWithValue("codigo", codigo);
                    command.Parameters.AddWithValue("amecop", amecop);
                    command.Parameters.AddWithValue("descripcion", descripcion.ToUpper());
                    command.Parameters.AddWithValue("stock", stock);
                    command.Parameters.AddWithValue("precioCosto", precioCosto);
                    command.Parameters.AddWithValue("precioLab", precioLab);
                    command.Parameters.AddWithValue("precioDistribuidor", precioDistribuidor);
                    command.Parameters.AddWithValue("precioMayoreo", precioMayoreo);
                    command.Parameters.AddWithValue("precioLista", precioLista);
                    command.Parameters.AddWithValue("iva", iva);
                    command.Parameters.AddWithValue("idClasificacion", idClasificacion);
                    command.Parameters.AddWithValue("idMarca", idMarca);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void modificarProducto(int idProducto, string codigo, string amecop, string descripcion, int stock, decimal precioCosto, decimal precioLab, decimal precioDistribuidor,
            decimal precioMayoreo, decimal precioLista, int iva, int idClasificacion, int idMarca)//agrega usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "modificarProducto";
                    command.Parameters.AddWithValue("idProducto", idProducto);
                    command.Parameters.AddWithValue("codigo", codigo);
                    command.Parameters.AddWithValue("amecop", amecop);
                    command.Parameters.AddWithValue("descripcion", descripcion.ToUpper());
                    command.Parameters.AddWithValue("stock", stock);
                    command.Parameters.AddWithValue("precioCosto", precioCosto);
                    command.Parameters.AddWithValue("precioLab", precioLab);
                    command.Parameters.AddWithValue("precioDistribuidor", precioDistribuidor);
                    command.Parameters.AddWithValue("precioMayoreo", precioMayoreo);
                    command.Parameters.AddWithValue("precioLista", precioLista);
                    command.Parameters.AddWithValue("iva", iva);
                    command.Parameters.AddWithValue("idClasificacion", idClasificacion);
                    command.Parameters.AddWithValue("idMarca", idMarca);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void eliminarProducto(int idProducto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "eliminarProducto";
                    command.Parameters.AddWithValue("idProducto", idProducto);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        public DataTable buscarProducto(int idProducto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select idLote,lote,l.stock,p.precioMayoreo from lotes l left join productos p on l.idProducto=p.idProducto where l.idProducto=@idProducto and l.activo=1";
                    command.Parameters.AddWithValue("@idProducto", idProducto);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
        public int validarStock(int idProducto)
        {
            int stock = 0;
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select stock from productos where activo=1 and idProducto=@idProducto;";
                        command.Parameters.AddWithValue("@idProducto", idProducto);
                        command.CommandType = CommandType.Text;
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            stock = Convert.ToInt32(reader["stock"]);
                        }
                    }
                }
                return stock;
            }
            catch
            {
                return stock;
            }
            
        }
    }
}
