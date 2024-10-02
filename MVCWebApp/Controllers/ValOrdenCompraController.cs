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
    public class ValOrdenCompraController : Controller
    {
        List<OrdenCompra> lst = new List<OrdenCompra>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult Index()
        {
            try
            {

                TempData["BackUrl"] = "/ValOrdenCompra/Index";

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtOrdenCompraPorValidar();
                foreach (var item in result)
                {
                    lst.Add(item.SetOrdenCompra());
                }
                return View(lst.OrderByDescending(p => p.FechaRegistro).ToList());
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

                result = (HttpContext.Application["proxySistema"] as ISistema).AprobarOrdenCompra(id, user.Email1, adicional, user.Usuario).SetRespuesta();
                result.Metodo = "/ValOrdenCompra/Index";
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

                result = (HttpContext.Application["proxySistema"] as ISistema).RechazarOrdenCompra(id, user.Email1, adicional, user.Usuario).SetRespuesta();
                result.Metodo = "/ValOrdenCompra/Index";
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
        public JsonResult Observar(string id, string adicional)
        {
            try
            {
                var user = (Session["usuario"] as ExternoDTO);

                result = (HttpContext.Application["proxySistema"] as ISistema).ObservarOrdenCompra(id, user.Email1, adicional, user.Usuario).SetRespuesta();
                result.Metodo = "/ValOrdenCompra/Index";
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