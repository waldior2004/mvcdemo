using com.msc.services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.msc.infraestructure.utils;

namespace com.msc.frontend.mvc.Controllers
{
    public class ReporteController : Controller
    {
        
        [Authorization]
        public ActionResult EnergiaConten()
        {
            var lstPuer = (HttpContext.Application["proxySistema"] as ISistema).ObtPuerto();
            this.loadSelectPuerto(lstPuer, 0);

            return View();
        }

        [Authorization]
        public ActionResult Trazabilidad()
        {
            return View();
        }
	}
}