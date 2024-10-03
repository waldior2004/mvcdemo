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
	}
}