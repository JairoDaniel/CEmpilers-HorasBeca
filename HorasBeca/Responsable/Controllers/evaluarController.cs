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
        private int castear_int(SqlDataReader reader)
        {
            int num = 0;
            try
            {
                num = reader.GetInt32(0);

            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            { num = 0; }
            return num;
        }

        private string castearNombre(SqlDataReader reader)
        {
            string dato = "";
            try
            {
                dato = dato + reader.GetInt32(0).ToString() + ", ";
                dato = dato + reader.GetString(1) + ", ";
                dato = dato + reader.GetString(2) + ", ";
                dato = dato + reader.GetString(3);

                return dato;
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            { return dato; }
        }


        private List<int> getIds(string nombre)
        {
            List<int> ids = new List<int>();
            using (SqlConnection connection = DBConnection.getConnection())
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id_solicitud from [APROBADA] where responsable=" + nombre, connection);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ids.Add(castear_int(reader));
                    }
                    reader.Close();
                    return ids;
                }
                catch (SqlException ex)
                {
                    return ids;
                }
                finally { connection.Close(); }
            }

        }

        
        public string getAsistente(int id, string responsable)
        {
           
            string nombre = "" ;
            using (SqlConnection connection = DBConnection.getConnection())
            {

                SqlCommand command = new SqlCommand("dbo.get_carnet_nombre", connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        nombre = castearNombre(reader);
                    }
                    reader.Close();
                    return nombre;
                }
                catch (SqlException ex)
                {
                    return nombre;
                }
                finally { connection.Close(); }
            }
        }

        [Route("getAsistentes")]
        [HttpGet]
        public IHttpActionResult getAsistentes()
        {
            string responsable = "'nereo'";
            List<int> ids = new List<int>();
            ids = getIds(responsable);
            int largo = ids.Count();
            List<string> nombres = new List<string>();
            for (int i=0; i<largo;i++)
            {
                nombres.Add(getAsistente(ids[i], responsable));
            }
            return Json(nombres);

            
        }
    }
}
