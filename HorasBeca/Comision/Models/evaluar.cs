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
}