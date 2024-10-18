using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dTipoPrecio : conexion
    {
        public DataTable listarTipoPrecio()//muestra usuarios
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select *,descripcion from tipoPrecio";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable tabla = new DataTable();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
    }
}
