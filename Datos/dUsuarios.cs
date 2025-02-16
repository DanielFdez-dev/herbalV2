using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dUsuarios : conexion
    {
        public bool login(string usuario, string pass, out int idEmpleado, out string nombre, out string permisos, out int identificador, out string error)
        {
            error = "";
            idEmpleado = 0;
            nombre = string.Empty;
            permisos = string.Empty;
            bool resul;
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select top 1 idEmpleado, nombre, permisos from empleados where activo=1 and usuario=@usuario and pass=@pass;";
                        command.Parameters.AddWithValue("@usuario", usuario);
                        command.Parameters.AddWithValue("@pass", pass);
                        command.CommandType = CommandType.Text;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                idEmpleado = Convert.ToInt32(reader["idEmpleado"]);
                                nombre = reader["nombre"].ToString();
                                permisos = reader["permisos"].ToString();
                                
                            }
                            resul = true;
                            identificador = 1;
                        }
                        else
                        {
                            resul = false;
                            identificador = 0;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                resul = false;
                identificador = 2;
                error = e.Message;
            }

            //identificador 0=usuario o contraseña incorrectos, 1=encontrado, 2=error
            return resul;
        }
        public DataTable listarUsuarios()//muestra usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "listarEmpleados";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }

        public void agregarUsuario(string usuario, string nombre, string pass, string permisos)//agrega usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "agregarUsuario";
                    command.Parameters.AddWithValue("usuario", usuario.ToUpper());
                    command.Parameters.AddWithValue("nombre", nombre.ToUpper());
                    command.Parameters.AddWithValue("pass", pass);
                    command.Parameters.AddWithValue("permisos", permisos);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void eliminarUsuario(int idUsuario)//elimina usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "eliminarUsuario";
                    command.Parameters.AddWithValue("idEmpleado", idUsuario);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void modificarUsuario(int idUsuario, string usuario, string nombre, string pass, string permisos)//modificar usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "modificarUsuario";
                    command.Parameters.AddWithValue("idEmpleado", idUsuario);
                    command.Parameters.AddWithValue("usuario", usuario.ToUpper());
                    command.Parameters.AddWithValue("nombre", nombre.ToUpper());
                    command.Parameters.AddWithValue("pass", pass);
                    command.Parameters.AddWithValue("permisos", permisos);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
