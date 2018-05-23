using Responsable.Constants;
using Responsable.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Responsable.Controllers
{
    [RoutePrefix("responsable")]
    public class evaluarController : ApiController
    {
        [Route("evaluarAsistente")]
        [HttpPost]
        public void register(evaluar evaluacion)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.evaluar", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@carnet", SqlDbType.Int).Value = Convert.ToInt32(evaluacion.carnet);
                command.Parameters.AddWithValue("@observacion", SqlDbType.VarChar).Value = evaluacion.observacion;
                command.Parameters.AddWithValue("@responsable", SqlDbType.VarChar).Value = evaluacion.responsable;

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
