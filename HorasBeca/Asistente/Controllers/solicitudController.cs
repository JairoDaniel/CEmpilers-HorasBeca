using Asistente.Constants;
using Asistente.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace Asistente.Controllers
{
    [RoutePrefix("asistente")]
    public class solicitudController : ApiController
    {

        [Route("obtenerSolicitudes")]
        [HttpGet]
        public IHttpActionResult getSolicitudes()
        {
            List<solicitud> solicitudes = new List<solicitud>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD] WHERE estado_sistema='pendiente' OR estado_sistema='avalada' OR estado_sistema='noAvalada'", connection);
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

        [Route("solicitudesPendientes")]
        [HttpGet]
        public IHttpActionResult getSolicitudesPendientes()
        {
            List<solicitud> solicitudes = new List<solicitud>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD] WHERE estado_sistema='pendiente'", connection);
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

        [Route("solicitudesAvaladas")]
        [HttpGet]
        public IHttpActionResult getSolicitudesAvaladas()
        {
            List<solicitud> solicitudes = new List<solicitud>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD] WHERE estado_sistema='avalada'", connection);
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

        [Route("solicitudesNoAvaladas")]
        [HttpGet]
        public IHttpActionResult getSolicitudesNoAvaladas()
        {
            List<solicitud> solicitudes = new List<solicitud>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD] WHERE estado_sistema='noAvalada'", connection);
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
        
        [Route("avalarSolicitud")]
        [HttpPost]
        public void avalar(solicitud pSolicitud)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.avalar", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_solicitud", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.id_solicitud);
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

        [Route("noAvalarSolicitud")]
        [HttpPost]
        public void noAvalar(solicitud pSolicitud)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.no_avalar", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_solicitud", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.id_solicitud);
                command.Parameters.AddWithValue("@observacion", SqlDbType.VarChar).Value = pSolicitud.observacion;
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
                pSolicitud.tipo_beca = reader.GetString(9);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.tipo_beca = null;
            }
            try
            {
                pSolicitud.email = reader.GetString(10);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.email = null;
            }
            try
            {
                pSolicitud.ponderado_general = reader.GetFloat(11);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.ponderado_general = 0;
            }
            try
            {
                pSolicitud.ponderado_semestral = reader.GetFloat(12);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.ponderado_semestral = 0;
            }
            try
            {
                pSolicitud.cumple_requisitos = reader.GetString(13);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.cumple_requisitos = null;
            }
            try
            {
                pSolicitud.cuenta_bancaria = reader.GetString(14);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.cuenta_bancaria = null;
            }
            //NO SE CONSIDERAN LOS SCREENS 15/16/17
            try
            {
                pSolicitud.estado_estudiante = reader.GetString(18);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.estado_estudiante = null;
            }
            try
            {
                pSolicitud.estado_sistema = reader.GetString(19);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.estado_sistema = null;
            }
            try
            {
                pSolicitud.tiene_nombramiento = reader.GetString(20);
            }
            catch
            {
                pSolicitud.tiene_nombramiento = null;
            }
            try
            {
                pSolicitud.horas_nombradas = reader.GetInt32(21);
            }
            catch
            {
                pSolicitud.horas_nombradas = 0;
            }
            try
            {
                pSolicitud.tipo_beca_nombrada = reader.GetString(22);
            }
            catch
            {
                pSolicitud.tipo_beca_nombrada = null;
            }
            try
            {
                pSolicitud.lugar_nombramiento = reader.GetString(23);
            }
            catch
            {
                pSolicitud.lugar_nombramiento = null;
            }

            return pSolicitud;
        }
        
        private fecha leerFecha(fecha pFecha, SqlDataReader reader)
        {
            try
            {
                pFecha.fecha_inicio = reader.GetDateTime(0).ToString("dd-MM-yyyy");
               
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {   }

            try
            {
                pFecha.fecha_final = reader.GetDateTime(1).ToString("dd-MM-yyyy");
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {   }
            return pFecha;
        }

        [Route("getPeriodo")]
        [HttpGet]
        public IHttpActionResult getPeriodo()
        {
            List<fecha> fecha = new List<fecha>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.get_periodo", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        fecha pFecha = new fecha();
                        fecha.Add(leerFecha(pFecha, reader));
                    }
                    return Json(fecha);

                } 
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    return Json(fecha);
                } 
                finally { connection.Close(); }
            }
        }

        public fecha get_periodo()
        {
            fecha fecha = new fecha();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.get_periodo", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        fecha = leerFecha(fecha,reader);
                    }
                    return fecha;

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    return fecha;
                }
                finally { connection.Close(); }
            }
        }


        [Route("setPeriodo")]
        [HttpPost]
        public void setPeriodo(fecha pfecha)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.ingresar_periodo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@fecha_inicio", SqlDbType.Date).Value = Convert.ToDateTime(pfecha.fecha_inicio);
                command.Parameters.AddWithValue("@fecha_final", SqlDbType.Date).Value = Convert.ToDateTime(pfecha.fecha_final);
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

        private string leerEstadoSistema(string estado, SqlDataReader reader)
        {
            try
            {
                estado = reader.GetString(0);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                return estado = "null";
            }
            return estado;
        }
        public string get_estado_sistema(int id)
        {
            string estado="";
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.get_estado_sistema", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        estado = leerEstadoSistema(estado,reader);
                    }
                    return estado;

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    return estado;
                }
                finally { connection.Close(); }
            }
        }
    }


}

