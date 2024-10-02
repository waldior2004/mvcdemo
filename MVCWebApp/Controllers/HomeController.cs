using com.msc.services.dto;
using System.Web.Mvc;

namespace com.msc.frontend.mvc.Controllers
{

    public class HomeController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            var objExterno = (Session["Usuario"] as ExternoDTO);
            ViewBag.Nombres = objExterno.Usuario;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [AllowAnonymous]
        public ActionResult ErrorJson()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

    }
}