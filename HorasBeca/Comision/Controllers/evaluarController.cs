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
        public void aprobar(aprobada solicitud)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.aprobar", connection);
                command.CommandType = CommandType.StoredProcedure;
               
                command.Parameters.AddWithValue("@id_solicitud", SqlDbType.Int).Value = Convert.ToInt32(solicitud.id_solicitud);
                command.Parameters.AddWithValue("@horas", SqlDbType.Int).Value = Convert.ToInt32(solicitud.horas);
                command.Parameters.AddWithValue("@responsable", SqlDbType.VarChar).Value = solicitud.responsable;
                command.Parameters.AddWithValue("@horas_extra", SqlDbType.Int).Value = Convert.ToInt32(solicitud.horas_extra);

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

        [Route("rechazarSolicitud")]
        [HttpPost]
        public void rechazar(rechazada solicitud)
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
        private solicitud leerJson(solicitud pSolicitud, SqlDataReader reader)
        {
            try
            {
                pSolicitud.id_solicitud = reader.GetInt32(0);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.id_solicitud = 0;
            }
            try
            {
                pSolicitud.fecha = reader.GetDateTime(1).ToString("dd-MM-yyyy"); 
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
            }
            try
            {
                pSolicitud.semestre = reader.GetInt32(2);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.semestre = 0;
            }
            try
            {
                pSolicitud.cedula = reader.GetInt32(3);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.cedula = 0;
            }
            try
            {
                pSolicitud.carnet = reader.GetInt32(4);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.carnet = 0;
            }
            try
            {
                pSolicitud.nombre = reader.GetString(5);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.nombre = null;
            }
            try
            {
                pSolicitud.apellido1 = reader.GetString(6);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.apellido1 = null;
            }
            try
            {
                pSolicitud.apellido2 = reader.GetString(7);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.apellido2 = null;
            }
            try
            {
                pSolicitud.telefono = reader.GetInt32(8);

            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.telefono = 0;
            }
            try
            {
                pSolicitud.email = reader.GetString(9);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.email = null;
            }
            try
            {
                pSolicitud.ponderado_general = reader.GetFloat(10);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.ponderado_general = 0;
            }
            try
            {
                pSolicitud.ponderado_semestral = reader.GetFloat(11);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.ponderado_semestral = 0;
            }
            try
            {
                pSolicitud.cuenta_bancaria = reader.GetString(12);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.cuenta_bancaria = null;
            }
            try
            {
                pSolicitud.tipo_beca = reader.GetString(13);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.tipo_beca = null;
            }
            //15,16,17
            try
            {
                pSolicitud.estado_estudiante = reader.GetString(17);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.estado_estudiante = null;
            }
            try
            {
                pSolicitud.estado_sistema = reader.GetString(18);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.estado_sistema = null;
            }
            return pSolicitud;
        }

        private cancelacion leerCancelacion(cancelacion pCancelacion, SqlDataReader reader)
        {
            try
            {
                pCancelacion.id_solicitud = reader.GetInt32(0);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pCancelacion.id_solicitud = 0;
            }
            try
            {
                pCancelacion.observacion = reader.GetString(1);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pCancelacion.observacion = "";
            }
            return pCancelacion;
        }
        [Route("solicitudesAvaladas")]
        [HttpGet]
        public IHttpActionResult getSolicitudesPendientes()
        {
            List<solicitud> solicitudes = new List<solicitud>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD] where estado_sistema = 'avalada'", connection);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        solicitud pSolicitud = new solicitud();

                        solicitudes.Add(leerJson(pSolicitud, reader));
                    }
                    return Json(solicitudes);
                }
                catch (SqlException ex)
                {
                    return Json(ex);
                }
                finally { connection.Close(); }
            }

        }

        [Route("solicitudesCanceladas")]
        [HttpGet]
        public IHttpActionResult getSolicitudesCanceladas()
        {
            List<cancelacion> cancelaciones = new List<cancelacion>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD_CANCELACION]", connection);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cancelacion pCancelacion = new cancelacion();

                        cancelaciones.Add(leerCancelacion(pCancelacion, reader));
                    }
                    return Json(cancelaciones);
                }
                catch (SqlException ex)
                {
                    return Json(ex);
                }
                finally { connection.Close(); }
            }
        }
    }
}
