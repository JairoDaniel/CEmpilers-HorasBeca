using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using Asistente.Controllers;
using Asistente.Models;
using System.Web.Http;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class AsistenteTest
    {




        [TestMethod]
        public void ObtenerSolicitudesParaAsistente()
        {
            var controller = new Asistente.Controllers.solicitudController();
            var actual = controller.getPeriodo();
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(IHttpActionResult));
        }

        [TestMethod]
        public void SolicitudNoAvaladaPorAsistente()
        {
            Asistente.Controllers.solicitudController controller = new Asistente.Controllers.solicitudController();
            Asistente.Models.solicitud solicitud = new Asistente.Models.solicitud();
            solicitud.id_solicitud = 29;
            solicitud.observacion = "observacion TEST";
            controller.noAvalar(solicitud);
            string estado = controller.get_estado_sistema(solicitud.id_solicitud);
            Assert.AreEqual("noAvalada", estado);
        }

        [TestMethod]
        public void SolicitudSiAvaladaPorAsistente()
        {
            Asistente.Controllers.solicitudController controller = new Asistente.Controllers.solicitudController();
            Asistente.Models.solicitud solicitud = new Asistente.Models.solicitud();
            solicitud.id_solicitud = 29;
            controller.avalar(solicitud);
            string estado = controller.get_estado_sistema(solicitud.id_solicitud);
            Assert.AreEqual("avalada", estado);
        }

        [TestMethod]
        public void SetearElPeriodoDeRecepcionDeSolicitudes()
        {
          /*  Asistente.Controllers.solicitudController controller = new Asistente.Controllers.solicitudController();
            Asistente.Models.fecha fecha = new Asistente.Models.fecha();
            fecha.fecha_inicio =
            fecha.fecha_final
            controller.setPeriodo();*/
        }

    }
}
