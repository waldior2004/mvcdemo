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
    public class PedidoController : Controller
    {
        List<Pedido> lst = new List<Pedido>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult Index()
        {
            try
            {
                TempData["BackUrl"] = "/Pedido/Index";

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtPedido();
                foreach (var item in result)
                {
                    lst.Add(item.SetPedido());
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
                    ViewBag.UrlBack = "/Pedido/Index";

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtPedido(id);
                return View(result.SetPedido());
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
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtEmpresa();
                this.loadSelectEmpresa(lst, 0);

                var lstT = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("014");
                this.loadSelectTablas(lstT, 0, "Tipo Petición", "014");

                var lstE = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("015");
                this.loadSelectTablas(lstE, 0, "Estado", "015");

                var lstCC = (HttpContext.Application["proxySistema"] as ISistema).ObtCentroCosto();
                this.loadSelectCentroCosto(lstCC, 0);

                var lstSuc = new List<SucursalDTO>();
                this.loadSelectSucursales(lstSuc, 0);

                var lstAreaS = new List<AreaSolicitanteDTO>();
                this.loadSelectAreaSolicitante(lstAreaS, 0);

                return View("Edit", new Pedido { Id = 0, UsuarioSol = (Session["Usuario"] as ExternoDTO).Usuario, FlagAprobacion = 0 });
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
        public JsonResult New(Pedido obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditPedido(obj.GetPedidoDTO()).SetRespuesta();
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
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtEmpresa();
                this.loadSelectEmpresa(lst, 0);

                var lstT = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("014");
                this.loadSelectTablas(lstT, 0, "Tipo Petición", "014");

                var lstE = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("015");
                this.loadSelectTablas(lstE, 0, "Estado", "015");

                var lstCC = (HttpContext.Application["proxySistema"] as ISistema).ObtCentroCosto();
                this.loadSelectCentroCosto(lstCC, 0);

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtPedido(id);

                var lstSuc = (HttpContext.Application["proxySistema"] as ISistema).ObtSucursal().FindAll(p => p.Empresa.Id == result.Empresa.Id);
                this.loadSelectSucursales(lstSuc, 0);

                var lstArea = (HttpContext.Application["proxySistema"] as ISistema).ObtAreaSolicitante().FindAll(p => p.Empresa.Id == result.Empresa.Id);
                this.loadSelectAreaSolicitante(lstArea, 0);

                return View(result.SetPedido());
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
        public JsonResult Edit(Pedido obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditPedido(obj.GetPedidoDTO()).SetRespuesta();
                }

                result.Metodo = "/Pedido/Index";
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimPedido(Convert.ToInt32(item)).SetRespuesta();
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimPedido(Convert.ToInt32(id)).SetRespuesta();

                result.Metodo = "/Pedido/Index";
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