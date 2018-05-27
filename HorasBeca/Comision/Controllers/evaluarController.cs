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
    public class evaluarController : ApiController
    {
        [Route("aprobarSolicitud")]
        [HttpPost]
        public void aprobar(evaluar solicitud)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.aprobar", connection);
                SqlCommand commandHora = new SqlCommand("dbo.asignar_horas", connection);
                command.CommandType = CommandType.StoredProcedure;
                commandHora.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_solicitud", SqlDbType.Int).Value = Convert.ToInt32(solicitud.id_solicitud);
                command.Parameters.AddWithValue("@horas", SqlDbType.Int).Value = Convert.ToInt32(solicitud.horas);
                command.Parameters.AddWithValue("@responsable", SqlDbType.VarChar).Value = solicitud.responsable;



                commandHora.Parameters.AddWithValue("@carnet", SqlDbType.Int).Value = Convert.ToInt32(solicitud.carnet);
                commandHora.Parameters.AddWithValue("@horas", SqlDbType.Int).Value = Convert.ToInt32(solicitud.horas);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    commandHora.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
                finally { connection.Close(); }
            }
        }

        [Route("rechazarSolicitud")]
        [HttpPost]
        public void rechazar(evaluar solicitud)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.rechazar", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_solicitud", SqlDbType.Int).Value = Convert.ToInt32(solicitud.id_solicitud);
                command.Parameters.AddWithValue("@observacion", SqlDbType.VarChar).Value = solicitud.observacion;

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
