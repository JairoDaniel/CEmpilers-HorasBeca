using Estudiante.Constants;
using Estudiante.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace Estudiante.Controllers
{
    [RoutePrefix("estudiante")]
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
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD]", connection);
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

        [Route("ingresarSolicitud")]
        [HttpPost]
        public void nuevaSolicitud(solicitud pSolicitud)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.insertar_solicitud", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@semestre", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.semestre);
                command.Parameters.AddWithValue("@cedula", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.cedula);
                command.Parameters.AddWithValue("@carnet", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.carnet);
                command.Parameters.AddWithValue("@nombre", SqlDbType.VarChar).Value = pSolicitud.nombre;
                command.Parameters.AddWithValue("@apellido1", SqlDbType.VarChar).Value = pSolicitud.apellido1;
                command.Parameters.AddWithValue("@apellido2", SqlDbType.VarChar).Value = pSolicitud.apellido2;
                command.Parameters.AddWithValue("@telefono", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.telefono);
                command.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = pSolicitud.email;
                command.Parameters.AddWithValue("@ponderado_general", SqlDbType.Float).Value = Convert.ToDouble(pSolicitud.ponderado_general);
                command.Parameters.AddWithValue("@ponderado_semestral", SqlDbType.Float).Value = Convert.ToDouble(pSolicitud.ponderado_semestral);
                command.Parameters.AddWithValue("@cuenta_bancaria", SqlDbType.VarChar).Value = pSolicitud.cuenta_bancaria;
                command.Parameters.AddWithValue("@tipo_beca", SqlDbType.VarChar).Value = pSolicitud.tipo_beca;
                command.Parameters.AddWithValue("@screen_ponderado_general", SqlDbType.Image).Value = pSolicitud.screen_ponderado_general;
                command.Parameters.AddWithValue("@screen_ponderado_semestral", SqlDbType.Image).Value = pSolicitud.screen_ponderado_semestral;
                command.Parameters.AddWithValue("@screen_cuenta_bancaria", SqlDbType.Image).Value = pSolicitud.screen_cuenta_bancaria;
                command.Parameters.AddWithValue("@estado_estudiante", SqlDbType.VarChar).Value = pSolicitud.estado_estudiante;
                command.Parameters.AddWithValue("@estado_sistema", SqlDbType.VarChar).Value = pSolicitud.estado_sistema;

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
                pSolicitud.fecha = reader.GetDateTime(1);
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
                pSolicitud.ponderado_general = reader.GetDouble(10);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.ponderado_general = 0;
            }
            try
            {
                pSolicitud.ponderado_semestral = reader.GetDouble(11);
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
    }
}
