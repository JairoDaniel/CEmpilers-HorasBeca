using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comision.Models
{
    public class evaluar
    {
        public int id_solicitud { get; set; }
        public int carnet { get; set; }
        public int horas { get; set; }
        public string responsable { get; set; }
        public string observacion { get; set; }
    }

    public class cancelacion
    {
        public int id_solicitud { get; set; }
        public string observacion { get; set; }
    }

    public class solicitud
    {
        public int id_solicitud { get; set; }
        public string fecha { get; set; }
        public int semestre { get; set; }
        public int cedula { get; set; }
        public int carnet { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public int telefono { get; set; }
        public string tipo_beca { get; set; }
        public string email { get; set; }
        public float ponderado_general { get; set; }
        public float ponderado_semestral { get; set; }
        public string cumple_requisitos { get; set; }
        public string cuenta_bancaria { get; set; }
        public string screen_ponderado_general { get; set; }
        public string screen_ponderado_semestral { get; set; }
        public string screen_cuenta_bancaria { get; set; }
        public string estado_estudiante { get; set; }
        public string estado_sistema { get; set; }
        public string tiene_nombramiento { get; set; }
        public int horas_nombradas { get; set; }
        public string tipo_beca_nombrada { get; set; }
        public string lugar_nombramiento { get; set; }
        public string observacion { get; set; }

    }

    public class aprobada
    {
        public int id_solicitud { get; set; }
        public int carnet { get; set; }
        public int horas { get; set; }
        public int responsable { get; set; }
        public int horas_extra { get; set; }
    }

    public class rechazada
    {
        public int id_solicitud { get; set; }
        public string observacion { get; set; }
    }

}