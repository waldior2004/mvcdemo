using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace com.msc.frontend.mvc.Controllers
{
    public class ValidarLiquiCController : Controller
    {
        List<CargaLiquiC> lst = new List<CargaLiquiC>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult Index()
        {
            try
            {

                TempData["BackUrl"] = "/ValidarLiquiC/Index";

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtEnviadosC();
                foreach (var item in result)
                {
                    lst.Add(item.SetCargaLiquiC());
                }
                return View(lst.OrderByDescending(p => p.FecRegistro).ToList());
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        [HttpPost]
        public JsonResult Aprobar(string id, string adicional)
        {
            try
            {
                var user = (Session["usuario"] as ExternoDTO);

                result = (HttpContext.Application["proxySistema"] as ISistema).AprobarCargaLiquiC(id, user.Email1, adicional).SetRespuesta();
                result.Metodo = "/ValidarLiquiC/Index";
                return Json(result);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return Json(result);
            }
        }

        [Authorization]
        [HttpPost]
        public JsonResult Rechazar(string id, string adicional)
        {
            try
            {
                var user = (Session["usuario"] as ExternoDTO);

                result = (HttpContext.Application["proxySistema"] as ISistema).RechazarCargaLiquiC(id, user.Email1, adicional).SetRespuesta();
                result.Metodo = "/ValidarLiquiC/Index";
                return Json(result);

            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return Json(result);
            }
        }
       
    }
}