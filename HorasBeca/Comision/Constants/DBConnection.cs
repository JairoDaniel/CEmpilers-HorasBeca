using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Comision.Constants
{
    public class DBConnection
    {
        public static SqlConnection getConnection()
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "DESKTOP-059I8H7\\SQLSERVERHB";
            builder.InitialCatalog = "HorasBecaDB";
            builder.UserID = "horasBecaSQL";
            builder.Password = "horasBecaSQL";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            return connection;
        }
    }
}