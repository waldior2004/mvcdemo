using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace com.msc.frontend.mvc.Controllers
{
    public class CondEspeCliDiaController : Controller
    {
        List<CondEspeCliDia> lst = new List<CondEspeCliDia>();
        Respuesta result = new Respuesta();

        private void SetBitacora(BitacoraActionCode code, string id, string message)
        {
            try
            {
                BitacoraDTO objBit = new BitacoraDTO
                {
                    Accion = MessagesApp.BitacoraActionMessage(code),
                    IdInterno = Convert.ToInt32(id),
                    Tabla = "CondEspeCli",
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

        private List<CondEspeCliDia> getListaAsignacion(int id)
        {
            List<CondEspeCliDia> lst = new List<CondEspeCliDia>();
            var objCliD = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDetalle(id);
            foreach (var item in objCliD.CondEspeDias)
            {
                lst.Add(item.SetCondEspeCliDia());
            }
            return lst.OrderBy(p=>p.DiaI).ToList();
        }

        [Authorization]
        public ActionResult Detail(int id)
        {
            try
            {
                var objCliD = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDetalle(id);
                ViewBag.MaxDias = objCliD.Dias;
                ViewBag.Terminal = objCliD.Terminal.Descripcion;

                var res = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDia(id);

                foreach (var item in res)
                {
                    lst.Add(item.SetCondEspeCliDia());
                }

                return View(lst.OrderBy(p => p.DiaI).ToList());
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        public ActionResult Edit(int id)
        {
            try
            {
                var objCliD = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDetalle(id);
                ViewBag.MaxDias = objCliD.Dias;
                ViewBag.Terminal = objCliD.Terminal.Descripcion;
                ViewBag.IdDetalle = objCliD.Id;
                ////Grupo 001 Terminales
                //var lstP = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("001");
                //Grupo 008 Transporte
                var lstT = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("008");
                //lstP.AddRange(lstT);
                this.loadSelectTablas(lstT, 0, "Transporte", "008");

                var res = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDia(id);
                foreach (var item in res)
                {
                    lst.Add(item.SetCondEspeCliDia());
                }

                return View(lst.OrderBy(p=>p.DiaI).ToList());
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
        public ActionResult Edit(CondEspeCliDia obj)
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
                    TempData["Message"] = result.Descripcion;
                    return RedirectToAction("ErrorJson", "Home");
                }
                else
                {
                    if (obj.DiaF < obj.DiaI)
                    {
                        TempData["Message"] = "El campo de Día Fin debe ser mayor o igual al de Día Inicio";
                        return RedirectToAction("ErrorJson", "Home");
                    }
                    else
                    {
                        result = (HttpContext.Application["proxySistema"] as ISistema).EditCondEspeCliDia(obj.GetCondEspeCliDiaDTO()).SetRespuesta();
                        if (result.Id == 0)
                        {

                            var objCliD = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDetalle(obj.IdCondEspeCliDetalle);
                            SetBitacora(BitacoraActionCode.Insercion, objCliD.IdCondEspeCli.ToString(), "Se registró una asignación de transporte a la terminal " + objCliD.Terminal.Descripcion);
                            return View("Table", getListaAsignacion(obj.IdCondEspeCliDetalle));
                        }
                        else
                        {
                            TempData["Message"] = result.Descripcion;
                            return RedirectToAction("ErrorJson", "Home");
                        }
                    }
                }
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
        public ActionResult Delete(string id, string idPadre)
        {
            try
            {
                var objCliD = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDetalle(Convert.ToInt32(idPadre));

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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimCondEspeCliDia(Convert.ToInt32(item)).SetRespuesta();
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
                    TempData["Message"] = Message;
                    
                    SetBitacora(BitacoraActionCode.Eliminacion, objCliD.IdCondEspeCli.ToString(), "Se quitó una asignación de transporte a la terminal " + objCliD.Terminal.Descripcion);
                    return View("Table", getListaAsignacion(Convert.ToInt32(idPadre)));
                }
                else
                {
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimCondEspeCliDia(Convert.ToInt32(id)).SetRespuesta();
                    if (result.Id == 0)
                    {
                        SetBitacora(BitacoraActionCode.Eliminacion, objCliD.IdCondEspeCli.ToString(), "Se quitó una asignación de transporte a la terminal " + objCliD.Terminal.Descripcion);
                        return View("Table", getListaAsignacion(Convert.ToInt32(idPadre)));
                    }
                    else
                    {
                        TempData["Message"] = result.Descripcion;
                        return RedirectToAction("ErrorJson", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

    }
}