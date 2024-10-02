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
    public class TarifarioController : Controller
    {
        List<Tarifario> lst = new List<Tarifario>();
        Respuesta result = new Respuesta();
        // GET: Tarifario
        [Authorization]
        public ActionResult Index()
        {
            try
            {
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifario();
                foreach (var item in result)
                {
                    lst.Add(item.SetTarifario());
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
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifario(id);

                return View(result.SetTarifario());
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimTarifario(Convert.ToInt32(item)).SetRespuesta();
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimTarifario(Convert.ToInt32(id)).SetRespuesta();

                result.Metodo = "/Tarifario/Index";
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
        public ActionResult New()
        {
            try
            {
                var lstE = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("015");
                this.loadSelectTablas(lstE, 0, "Estado", "015");
                var lstM = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("003");
                this.loadSelectTablas(lstM, 0, "Moneda", "003");
                var lstP = (HttpContext.Application["proxySistema"] as ISistema).ObtProducto();
                var lstPv = (HttpContext.Application["proxySistema"] as ISistema).ObtProveedor();

                this.loadSelectProductos(lstP, 0);
                this.loadSelectProveedores(lstPv, 0);

                return View("Edit", new Tarifario { Id = 0 });

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
        public JsonResult New(Tarifario obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditTarifario(obj.GetTarifarioDTO()).SetRespuesta();
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
                //Grupo 001 Terminales
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifario(id);

                var lstE = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("015");
                this.loadSelectTablas(lstE, 0, "Estado", "015");
                var lstM = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("003");
                this.loadSelectTablas(lstM, 0, "Moneda", "003");
                var lstP = (HttpContext.Application["proxySistema"] as ISistema).ObtProducto();
                var lstPv = (HttpContext.Application["proxySistema"] as ISistema).ObtProveedor();

                //this.loadSelectEstados(lstE, result.Estado.Id);
                
                this.loadSelectProductos(lstP, result.Producto.Id);
                this.loadSelectProveedores(lstPv, result.Proveedor.Id);
                
                var objR = result.SetTarifario();
                return View(objR);
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
        public JsonResult Edit(Tarifario obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditTarifario(obj.GetTarifarioDTO()).SetRespuesta();
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
    }
}