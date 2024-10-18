using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public abstract class conexion
    {
        private readonly string connectionString;
        public conexion()
        {
            //connectionString = "Data Source=DESKTOP-1S5240M\\PRUEBA; Initial Catalog=herbal; Integrated Security=True;";
            //connectionString = "Data Source=tcp:DESKTOP-1S5240M\\PRUEBA,1433; DataBase= herbal; user id=prueba2; Password= Admin123;";
            connectionString = @"Server=tcp:SERVIDORHP\HERBAL,49500; DataBase= herbal; user id=AppHerbalV2; Password= Admin123;";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
