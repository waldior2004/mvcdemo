using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace com.msc.frontend.mvc.Controllers
{
    public class OrdenCompraController : Controller
    {
        List<OrdenCompra> lst = new List<OrdenCompra>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult Index()
        {
            try
            {
                TempData["BackUrl"] = null;

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtOrdenCompra();
                foreach (var item in result)
                {
                    lst.Add(item.SetOrdenCompra());
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
                if (TempData["BackUrl"] != null)
                    ViewBag.UrlBack = TempData["BackUrl"];
                else
                    ViewBag.UrlBack = "/OrdenCompra/Index";

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtOrdenCompra(id);
                return View(result.SetOrdenCompra());
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
                var lstE = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("015");
                this.loadSelectTablas(lstE, 0, "Estado", "015");

                return View("Edit", new OrdenCompra { Id = 0, Estado = new Tabla { Codigo = "002" } });
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
        public JsonResult New(OrdenCompra obj)
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
                    string usuario = (Session["Usuario"] as ExternoDTO).Usuario;
                    obj.User = usuario;
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditOrdenCompra(obj.GetOrdenCompraDTO()).SetRespuesta();
                }

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
                var lstE = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("015");
                this.loadSelectTablas(lstE, 0, "Estado", "015");

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtOrdenCompra(id);

                return View(result.SetOrdenCompra());
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
        public JsonResult Edit(OrdenCompra obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditOrdenCompra(obj.GetOrdenCompraDTO()).SetRespuesta();
                }

                result.Metodo = "/OrdenCompra/Index";
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimOrdenCompra(Convert.ToInt32(item)).SetRespuesta();
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimOrdenCompra(Convert.ToInt32(id)).SetRespuesta();

                result.Metodo = "/OrdenCompra/Index";
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
        public JsonResult Enviar(int id)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).EnviarOrdenCompra(Convert.ToInt32(id)).SetRespuesta();

                result.Metodo = "/OrdenCompra/Index";
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
        public ActionResult Facturar(int id)
        {
            try
            {
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtOrdenCompra(id);
                return View(result.SetOrdenCompra());
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
        public ActionResult Facturar(int Id, string NumFactura)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).FactOrdenCompra(Id, NumFactura).SetRespuesta();
                if (result.Id == 0)
                {
                    return RedirectToAction("Facturar", new { Id = Id });
                }
                else
                {
                    TempData["Message"] = result.Descripcion;
                    return RedirectToAction("ErrorJson", "Home");
                }
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        public FileResult Imprimir(int Id)
        {
            try
            {
                var objRetorno = (HttpContext.Application["proxySistema"] as ISistema).ImprimirOrdenCompra(Id);
                var tbl = Reporting.ToDataTable(objRetorno);
                var bytes = Printing.PrintPDF(tbl, "dsOrdenCompra", "OrdenCompra");
                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Pdf, objRetorno.Select(p => p.CODIGO).First() + ".pdf");
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }
    }
}