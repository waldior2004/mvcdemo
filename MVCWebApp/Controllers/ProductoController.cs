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
    public class ProductoController : Controller
    {
        List<Producto> lst = new List<Producto>();
        Respuesta result = new Respuesta();
        // GET: Producto

        [Authorization]
        public ActionResult Index()
        {
            try
            {
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtProducto();
                foreach (var item in result)
                {
                    lst.Add(item.SetProducto());
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
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtProducto(id);
                
                return View(result.SetProducto());
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimProducto(Convert.ToInt32(item)).SetRespuesta();
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimProducto(Convert.ToInt32(id)).SetRespuesta();

                result.Metodo = "/Producto/Index";
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
                var lstg = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("017");
                var lstu = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("016");
                var lsttp = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("B01");
                var lstf = (HttpContext.Application["proxySistema"] as ISistema).ObtFamilia();
                var lstSub = new List<SubFamiliaDTO>();
                this.loadSelectTablas(lstg, 0, "Grupo Producto", "017");
                this.loadSelectFamilias(lstf, 0);
                this.loadSelectTablas(lstu, 0, "Unidad de Medida", "016");
                this.loadSelectTablas(lsttp, 0, "Tipo Producto", "B01");
                this.loadSelectSubFamilias(lstSub, 0);

                return View("Edit", new Producto { Id = 0 });
                
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
        public JsonResult New(Producto obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditProducto(obj.GetProductoDTO()).SetRespuesta();
                }

                result.Metodo = "/Producto/Index";
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

                var lstg = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("017");
                var lstf = (HttpContext.Application["proxySistema"] as ISistema).ObtFamilia();
                var lstu = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("016");
                var lsttp = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("B01");
                this.loadSelectTablas(lsttp, 0, "Tipo Producto", "B01");
                this.loadSelectTablas(lstu, 0, "Unidad de Medida", "016");
                this.loadSelectTablas(lstg, 0, "Grupo Producto", "017");
                this.loadSelectFamilias(lstf, 0);

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtProducto(id);

                var lstSub = (HttpContext.Application["proxySistema"] as ISistema).ObtSubFamilia().FindAll(p => p.Familia.Id == result.Familia.Id);
                this.loadSelectSubFamilias(lstSub, 0);

                var objR = result.SetProducto();
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
        public JsonResult Edit(Producto obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditProducto(obj.GetProductoDTO()).SetRespuesta();
                }

                result.Metodo = "/Producto/Index";
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