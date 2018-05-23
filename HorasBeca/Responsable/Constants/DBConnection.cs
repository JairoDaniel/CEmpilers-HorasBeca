using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Responsable.Constants
{
    public class DBConnection
    {
        public static SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP-059I8H7\\SQLSERVERHB; Initial Catalog = HorasBecaDB; Integrated Security = true;");
            return connection;
        }
    }
}