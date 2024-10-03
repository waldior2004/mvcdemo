using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace com.msc.frontend.mvc.Controllers
{
    public class PaginaController : Controller
    {
        List<Pagina> lst = new List<Pagina>();
        Respuesta result = new Respuesta();

        // GET: Pagina Ajax
        [Authorization]
        public ActionResult Index()
        {
            try
            {
                var result = (HttpContext.Application["proxySeguridad"] as ISeguridad).ObtPagina();
                foreach (var item in result)
                {
                    lst.Add(item.SetPagina());
                }

                return View(lst);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        public ActionResult View(int id)
        {
            try
            {
                var result = (HttpContext.Application["proxySeguridad"] as ISeguridad).ObtPagina(id);
                return View(result.SetPagina());
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        public ActionResult New()
        {
            try
            {
                var lst = (HttpContext.Application["proxySeguridad"] as ISeguridad).ObtPagina();
                this.loadSelectPaginas(lst, 0, 0);
                return View("Edit", new Pagina { Id = 0, Descripcion = "", IdPagina = 0 });
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
        [ValidateAntiForgeryToken]
        public JsonResult New(Pagina obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = string.Empty;
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors += modelError.ErrorMessage + "<br/>";
                        }
                    }
                    result = MessagesApp.BackAppMessage(MessageCode.InvalidFields, ViewData.ModelState);
                    result.Descripcion = modelErrors;
                }
                else
                {
                    result = (HttpContext.Application["proxySeguridad"] as ISeguridad).EditPagina(obj.GetPaginaDTO()).SetRespuesta();
                }

                result.Metodo = "/Pagina/Index";
                return Json(result);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return Json(result);
            }
        }

        [Authorization]
        public ActionResult Edit(int id)
        {
            try
            {
                var lst = (HttpContext.Application["proxySeguridad"] as ISeguridad).ObtPagina();
                var result = (HttpContext.Application["proxySeguridad"] as ISeguridad).ObtPagina(id);
                var tmp = result.SetPagina();
                this.loadSelectPaginas(lst, id, tmp.IdPagina);
                return View(tmp);
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
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Pagina obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = string.Empty;
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors += modelError.ErrorMessage + "<br/>";
                        }
                    }
                    result = MessagesApp.BackAppMessage(MessageCode.InvalidFields, ViewData.ModelState);
                    result.Descripcion = modelErrors;
                }
                else
                {
                    result = (HttpContext.Application["proxySeguridad"] as ISeguridad).EditPagina(obj.GetPaginaDTO()).SetRespuesta();
                }

                result.Metodo = "/Pagina/Index";
                return Json(result);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return Json(result);
            }
        }

        [Authorization]
        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                if (id.IndexOf(",") >= 0)
                {
                    var OK = 0;
                    var Fail = 0;
                    var Message = "";
                    var codes = id.Split(',');
                    foreach (var item in codes)
                    {
                        if (item != "")
                        {
                            result = (HttpContext.Application["proxySeguridad"] as ISeguridad).ElimPagina(Convert.ToInt32(item)).SetRespuesta();
                            if (result.Id == 0)
                            {
                                OK++;
                                Message += string.Format("OK({0})", item);
                            }
                            else
                            {
                                Fail++;
                                Message += string.Format("Error({0}|{1})", item, result.Descripcion);
                            }
                        }
                    }
                    if (Fail > 0)
                    {
                        result.Id = -1;
                    }
                    result.Message = Message;
                }
                else
                    result = (HttpContext.Application["proxySeguridad"] as ISeguridad).ElimPagina(Convert.ToInt32(id)).SetRespuesta();

                result.Metodo = "/Pagina/Index";
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