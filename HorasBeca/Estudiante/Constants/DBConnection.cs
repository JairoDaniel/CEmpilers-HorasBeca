using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Estudiante.Constants
{
    public class DBConnection
    {
        public static SqlConnection getConnection()
        {

            SqlConnection connection = new SqlConnection("Data Source = DESKTOP-BBHJE8R\\SQLEXPRESS; Initial Catalog = HorasBecaDB; Integrated Security = true; ");
            return connection;
        }
    }
}