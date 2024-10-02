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
    public class TarifaCEController : Controller
    {
        List<TarifaCE> lst = new List<TarifaCE>();
        Respuesta result = new Respuesta();

        private void SetBitacora(BitacoraActionCode code, string id, string message)
        {
            try
            {
                BitacoraDTO objBit = new BitacoraDTO
                {
                    Accion = MessagesApp.BitacoraActionMessage(code),
                    IdInterno = Convert.ToInt32(id),
                    Tabla = "TarifaCE",
                    User = (Session["Usuario"] as ExternoDTO).Usuario,
                    Tipo = MessagesApp.BitacoraModeMessage(BitacoraModeCode.Manual),
                    Detalle = message
                };
                var bita = (HttpContext.Application["proxySistema"] as ISistema).EditBitacora(objBit).SetRespuesta();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
            }
        }

        [Authorization]
        public ActionResult Index()
        {
            try
            {
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifaCE();
                foreach (var item in result)
                {
                    lst.Add(item.SetTarifaCE());
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
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifaCE(id);
                var lstBitacora = (HttpContext.Application["proxySistema"] as ISistema).ObtBitacora(id, "TarifaCE");
                ViewBag.lstBitacora = lstBitacora;
                return View(result.SetTarifaCE());
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        public ActionResult Historial()
        {
            try
            {
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifaHistoricoCE();
                foreach (var item in result)
                {
                    lst.Add(item.SetTarifaCE());
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
        public ActionResult New()
        {
            try
            {
                //Grupo 001 Terminales
                var lstP = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("001");
                this.loadSelectTablas(lstP, 0, "Terminal", "001");

                //Grupo 002 Período de Tarifa
                var lstPT = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("002");
                this.loadSelectTablas(lstPT, 0, "Período", "002");

                //Grupo 003 Moneda
                var lstMon = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("003");
                this.loadSelectTablas(lstMon, 0, "Moneda", "003");

                return View("Edit", new TarifaCE { Id = 0 });
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
        public JsonResult New(TarifaCE obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditTarifaCE(obj.GetTarifaCEDTO()).SetRespuesta();

                    if (result.Id == 0)
                    {
                        SetBitacora(BitacoraActionCode.Insercion, result.Metodo, "Se registró la tarifa de C.E.");
                    }
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
                var lstP = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("001");

                //Grupo 002 Período de Tarifa
                var lstPT = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("002");

                //Grupo 003 Moneda
                var lstMon = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("003");

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifaCE(id);
                var objR = result.SetTarifaCE();

                this.loadSelectTablas(lstP, objR.IdTerminal, "Terminal", "001");
                this.loadSelectTablas(lstPT, objR.IdPerTar, "Período", "002");
                this.loadSelectTablas(lstMon, objR.IdMoneda, "Moneda", "003");
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
        public JsonResult Edit(TarifaCE obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditTarifaCE(obj.GetTarifaCEDTO()).SetRespuesta();

                    if (result.Id == 0)
                    {
                        SetBitacora(BitacoraActionCode.Actualizacion, obj.Id.ToString(), "Se modificó la tarifa de C.E.");
                    }
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
        [HttpPost]
        public JsonResult Aprobar(string id)
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
                            var objTemp = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifaCE(Convert.ToInt32(item));
                            result = (HttpContext.Application["proxySistema"] as ISistema).AprobarTarifaCE(objTemp).SetRespuesta();
                            if (result.Id == 0)
                            {
                                OK++;
                                Message += string.Format("OK({0})", item);
                                SetBitacora(BitacoraActionCode.Actualizacion, item, "Se aprobó la tarifa de C.E.");
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
                {
                    var objTemp = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifaCE(Convert.ToInt32(id));
                    result = (HttpContext.Application["proxySistema"] as ISistema).AprobarTarifaCE(objTemp).SetRespuesta();
                    if (result.Id == 0) {
                        SetBitacora(BitacoraActionCode.Actualizacion, id, "Se aprobó la tarifa de C.E.");
                    }
                }

                result.Metodo = "/TarifaCE/Index";
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimTarifaCE(Convert.ToInt32(item)).SetRespuesta();
                            if (result.Id == 0)
                            {
                                OK++;
                                Message += string.Format("OK({0})", item);
                                SetBitacora(BitacoraActionCode.Eliminacion, item, "Se eliminó la tarifa de C.E.");
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
                {
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimTarifaCE(Convert.ToInt32(id)).SetRespuesta();

                    if (result.Id == 0)
                    {
                        SetBitacora(BitacoraActionCode.Eliminacion, id, "Se eliminó la tarifa de C.E.");
                    }
                }

                result.Metodo = "/TarifaCE/Index";
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