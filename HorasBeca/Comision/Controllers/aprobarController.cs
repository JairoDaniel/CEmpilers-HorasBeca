using Comision.Constants;
using Comision.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Comision.Controllers
{
    [RoutePrefix("comision")]
    public class aprobarController : ApiController
    {
        [Route("aprobarSolicitud")]
        [HttpPost]
        public void register(aprobar solicitud)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.aprobar", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_solicitud", SqlDbType.Int).Value = Convert.ToInt32(solicitud.id_solicitud);
                command.Parameters.AddWithValue("@horas", SqlDbType.VarChar).Value = solicitud.horas;
                command.Parameters.AddWithValue("@responsable", SqlDbType.VarChar).Value = solicitud.responsable;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
                finally { connection.Close(); }
            }
        }
    }
}
