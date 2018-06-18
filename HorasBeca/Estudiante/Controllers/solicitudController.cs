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

        [Route("obtenerSolicitudesEnviadas/{carnet}")]
        [HttpGet]
        public IHttpActionResult getSolicitudEnviada(String carnet)
        {
            List<solicitud> solicitudes = new List<solicitud>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD] where carnet= '" + carnet +"' and estado_estudiante = 'enviada' ", connection);
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

        [Route("obtenerSolicitudesGuardadas/{carnet}")]
        [HttpGet]
        public IHttpActionResult getSolicitudGuardadas(String carnet)
        {
            List<solicitud> solicitudes = new List<solicitud>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD] where carnet= '" + carnet + "' and estado_estudiante = 'guardada' ", connection);
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

        [Route("obtenerSolicitudesCanceladas/{carnet}")]
        [HttpGet]
        public IHttpActionResult getSolicitudCanceladas(String carnet)
        {
            List<solicitud> solicitudes = new List<solicitud>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [SOLICITUD] where carnet= '" + carnet + "' and estado_estudiante = 'cancelada' ", connection);
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
            int ultimo_id = 0;
            using (SqlConnection connection = DBConnection.getConnection())
            {
                SqlCommand command = new SqlCommand("dbo.insertar_solicitud", connection);
                SqlCommand get_id = new SqlCommand("dbo.get_ultimo_id", connection);
                SqlCommand insert_ta = new SqlCommand("dbo.insertar_solicitud_ta", connection);
                SqlCommand insert_especial = new SqlCommand("dbo.insertar_solicitud_especial", connection);

                command.CommandType = CommandType.StoredProcedure;
                get_id.CommandType = CommandType.StoredProcedure;
                insert_ta.CommandType = CommandType.StoredProcedure;
                insert_especial.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@semestre", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.semestre);
                command.Parameters.AddWithValue("@cedula", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.cedula);
                command.Parameters.AddWithValue("@carnet", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.carnet);
                command.Parameters.AddWithValue("@nombre", SqlDbType.VarChar).Value = pSolicitud.nombre;
                command.Parameters.AddWithValue("@apellido1", SqlDbType.VarChar).Value = pSolicitud.apellido1;
                command.Parameters.AddWithValue("@apellido2", SqlDbType.VarChar).Value = pSolicitud.apellido2;
                command.Parameters.AddWithValue("@telefono", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.telefono);
                command.Parameters.AddWithValue("@tipo_beca", SqlDbType.VarChar).Value = pSolicitud.tipo_beca;
                command.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = pSolicitud.email;
                command.Parameters.AddWithValue("@ponderado_general", SqlDbType.Float).Value = Convert.ToDouble(pSolicitud.ponderado_general);
                command.Parameters.AddWithValue("@ponderado_semestral", SqlDbType.Float).Value = Convert.ToDouble(pSolicitud.ponderado_semestral);
                command.Parameters.AddWithValue("@cumple_requisitos", SqlDbType.VarChar).Value = pSolicitud.cumple_requisitos;
                command.Parameters.AddWithValue("@cuenta_bancaria", SqlDbType.VarChar).Value = pSolicitud.cuenta_bancaria;
                command.Parameters.AddWithValue("@screen_ponderado_general", SqlDbType.VarChar).Value = pSolicitud.screen_ponderado_general;
                command.Parameters.AddWithValue("@screen_ponderado_semestral", SqlDbType.VarChar).Value = pSolicitud.screen_ponderado_semestral;
                command.Parameters.AddWithValue("@screen_cuenta_bancaria", SqlDbType.VarChar).Value = pSolicitud.screen_cuenta_bancaria;
                command.Parameters.AddWithValue("@estado_estudiante", SqlDbType.VarChar).Value = pSolicitud.estado_estudiante;
                command.Parameters.AddWithValue("@estado_sistema", SqlDbType.VarChar).Value = pSolicitud.estado_sistema;
                command.Parameters.AddWithValue("@tiene_nombramiento", SqlDbType.VarChar).Value = pSolicitud.tiene_nombramiento;
                command.Parameters.AddWithValue("@horas_nombradas", SqlDbType.Int).Value = pSolicitud.horas_nombradas;
                command.Parameters.AddWithValue("@tipo_beca_nombrada", SqlDbType.VarChar).Value = pSolicitud.tipo_beca;
                command.Parameters.AddWithValue("@lugar_nombramiento", SqlDbType.VarChar).Value = pSolicitud.lugar_nombramiento;

                try    //--GUARDA LA SOLICITUD GENERICA CON TODOS LOS DATOS COMUNES 
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex){Console.WriteLine(ex);}
                finally { connection.Close(); }


                try //--CAPTA EL ID DE LA SOLICITUD QUE ACABO DE INGRESAR 
                {
                    connection.Open();
                    get_id.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ultimo_id = leerId(reader);
                    }
                }
                catch (SqlException ex){ Console.WriteLine(ex); }
                finally { connection.Close(); }


                //---------------- F LOGIC-----------------
                if (pSolicitud.tipo_beca == "tutoria" || pSolicitud.tipo_beca_nombrada=="asistente")
                {
                    insert_ta.Parameters.AddWithValue("@id_solicitud", SqlDbType.Int).Value = ultimo_id;
                    insert_ta.Parameters.AddWithValue("@id_curso", SqlDbType.VarChar).Value = pSolicitud.id_curso;
                    insert_ta.Parameters.AddWithValue("@nombre_curso", SqlDbType.VarChar).Value = pSolicitud.nombre_curso; 
                    insert_ta.Parameters.AddWithValue("@nota", SqlDbType.Int).Value = Convert.ToInt32(pSolicitud.nota);
                    insert_ta.Parameters.AddWithValue("@responsable", SqlDbType.VarChar).Value = pSolicitud.reponsable;
                    insert_ta.Parameters.AddWithValue("@screen_nota", SqlDbType.VarChar).Value = pSolicitud.screen_notas;
                    try    //--GUARDA LOS DATOS UNICOS DE LA SOLICITUD DE TUTORIA Y ASISTENCIA 
                    {
                        connection.Open();
                        insert_ta.ExecuteNonQuery();
                    }
                    catch (SqlException ex) { Console.WriteLine(ex); }
                    finally { connection.Close(); }
                }
                if (pSolicitud.tipo_beca == "especial")
                {
                    insert_especial.Parameters.AddWithValue("@id_solicitud", SqlDbType.Int).Value = ultimo_id;
                    insert_especial.Parameters.AddWithValue("horas_disponibles",SqlDbType.Int).Value= Convert.ToInt32(pSolicitud.horas_disponibles);
                    try    //--GUARDA LOS DATOS UNICOS DE LA SOLICITUD ESPECIAL
                    {
                        connection.Open();
                        insert_especial.ExecuteNonQuery();
                    }
                    catch (SqlException ex) { Console.WriteLine(ex); }
                    finally { connection.Close(); }
                }

            }
        }

        [Route("obtenerSolicitud")]
        [HttpGet]
        public IHttpActionResult get_solicitud(int id)
        {
            solicitud solicitud = new solicitud();
            using (SqlConnection connection = DBConnection.getConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [SOLICITUD] where id_solicitud= '" + id + "' ", connection);
                SqlCommand get_ta = new SqlCommand("SELECT * FROM [SOLICITUD_TA] WHERE id_solicitud='"+id+"'",connection);
                SqlCommand get_especial = new SqlCommand("SELECT * FROM [SOLICITUD_ESPECIAL] WHERE id_solicitud='" + id + "'", connection);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        solicitud pSolicitud = new solicitud();
                        solicitud = leerJson(pSolicitud, reader);
                    }            
                }
                catch (SqlException ex){return Json(ex);}
                finally { connection.Close(); }

                if (solicitud.tipo_beca=="estudiante" || solicitud.tipo_beca=="asistente")
                {
                    try
                    {
                        SqlDataReader reader = get_ta.ExecuteReader();
                        while (reader.Read())
                        {
                            solicitud = leerTA(solicitud, reader);
                        }
                    }
                    catch (SqlException ex) { return Json(ex); }
                    finally { connection.Close(); }
                }

                if (solicitud.tipo_beca == "especial")
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            solicitud = leerEspecial(solicitud, reader);
                        }
                    }
                    catch (SqlException ex) { return Json(ex); }
                    finally { connection.Close(); }
                }
                return Json(solicitud);
            }
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

        private int leerId(SqlDataReader reader)
        {
            int id = 0;
            try
            {
                id = reader.GetInt32(0);

            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            { }
            return id;
        }

        private fecha leerFecha(fecha pFecha, SqlDataReader reader)
        {
            try
            {
                pFecha.fecha_inicio = reader.GetDateTime(0).ToString("dd-MM-yyyy");

            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            { }

            try
            {
                pFecha.fecha_final = reader.GetDateTime(1).ToString("dd-MM-yyyy");
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            { }
            return pFecha;
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

        private solicitud leerEspecial(solicitud pSolicitud, SqlDataReader reader)
        {
            try
            {
                pSolicitud.horas_disponibles = reader.GetInt32(1);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.id_solicitud = 0;
            }
            return pSolicitud;
        }
        private solicitud leerTA(solicitud pSolicitud, SqlDataReader reader)
        {
            try
            {
                pSolicitud.id_curso = reader.GetString(1);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.id_curso = null;
            }
            try
            {
                pSolicitud.nombre_curso = reader.GetString(2);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.nombre_curso = null;
            }
            try
            {
                pSolicitud.nota = reader.GetInt32(3);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.nota = 0;
            }
            try
            {
                pSolicitud.reponsable = reader.GetString(4);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.reponsable = null;
            }
            try
            {
                pSolicitud.screen_notas = reader.GetString(5);
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                pSolicitud.screen_notas = null;
            }
            return pSolicitud;
        }
    }
}
