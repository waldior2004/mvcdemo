using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.interfaces;
using com.msc.services.dto.DataMapping;
using System;
using System.Web;
using System.Web.Mvc;

namespace com.msc.frontend.mvc.Controllers
{
    public class CascadeController : Controller
    {
        Respuesta result = new Respuesta();

        [HttpPost]
        public JsonResult ControlesxPagina(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySeguridad"] as ISeguridad).ObtControl().FindAll(p => p.Pagina.Id == id);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult SucusalesxEmpresa(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtSucursal().FindAll(p => p.Empresa.Id == id);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult AreaSolicitantexEmpresa(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAreaSolicitante().FindAll(p => p.Empresa.Id == id);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult SubFamiliasxFamilia(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtSubFamilia().FindAll(p => p.Familia.Id == id);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult ViajesxNave(string id, int port)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtViajexNave(id, port);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult DepartamentoxPais(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtDepartamento(id);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult ProvinciaxDepartamento(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtProvincia(id);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult DistritoxProvincia(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtDistrito(id);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult ImpuestosxTipo(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtImpuesto().FindAll(p => p.TipoImpuesto.Id == id);
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }

        [HttpPost]
        public JsonResult BancosxPais(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtBanco().FindAll(p => p.Pais.Id == id);
                foreach (var item in lst)
                {
                    item.Descripcion = item.Codigo + " - " + item.Descripcion;
                }
                return Json(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return null;
            }
        }
	}
}