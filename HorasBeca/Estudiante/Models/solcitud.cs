using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estudiante.Models
{
    public class solicitud
    {
        public int id_solicitud { get; set; }
        public DateTime fecha { get; set; }
        public int semestre { get; set; }
        public int cedula { get; set; }
        public int carnet { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public int telefono { get; set; }
        public string email { get; set; }
        public double ponderado_general { get; set; }
        public double ponderado_semestral { get; set; }
        public string cuenta_bancaria { get; set; }
        public string tipo_beca { get; set; }
        public byte[] screen_ponderado_general { get; set; }
        public byte[] screen_ponderado_semestral { get; set; }
        public byte[] screen_cuenta_bancaria { get; set; }
        public string estado_estudiante { get; set; }
        public string estado_sistema { get; set; }
    }
}