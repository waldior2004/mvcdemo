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
    public class CondEspeCliDetalleController : Controller
    {
        List<CondEspeCliDetalle> lst = new List<CondEspeCliDetalle>();
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

        [Authorization]
        public ActionResult View(int id)
        {
            try
            {
                if(TempData["Message"] != null)
                    ViewBag.Message = "Resultado Ultima Ejecución: " + TempData["Message"];

                var res = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCli(id);
                var objR = res.SetCondEspeCli();
                return View(objR.CondEspeCliDetalles);
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
                //Grupo 001 Terminales
                var lstP = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("001");
                this.loadSelectTablas(lstP, 0, "Terminal", "001");

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDetalle(id);
                var objR = result.SetCondEspeCliDetalle();

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
        public ActionResult Edit(CondEspeCliDetalle obj)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).EditCondEspeCliDetalle(obj.GetCondEspeCliDetalleDTO()).SetRespuesta();
                if (result.Id == 0)
                {
                    if(obj.Id == 0)
                        SetBitacora(BitacoraActionCode.Insercion, obj.IdCondEspeCli.ToString(), "Se registró un detalle de terminal");
                    else
                        SetBitacora(BitacoraActionCode.Actualizacion, obj.IdCondEspeCli.ToString(), "Se actualizó un detalle de terminal");

                    return RedirectToAction("View", "CondEspeCliDetalle", new { id = obj.IdCondEspeCli });
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

        [Authorization]
        [HttpPost]
        public ActionResult Delete(string id, string idPadre)
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimCondEspeCliDetalle(Convert.ToInt32(item)).SetRespuesta();
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
                    SetBitacora(BitacoraActionCode.Eliminacion, idPadre, "Se eliminó un(os) detalle(s) de terminal");
                    return RedirectToAction("View", "CondEspeCliDetalle", new { id = idPadre });
                }
                else
                {
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimCondEspeCliDetalle(Convert.ToInt32(id)).SetRespuesta();
                    if (result.Id == 0)
                    {
                        SetBitacora(BitacoraActionCode.Eliminacion, idPadre, "Se eliminó un detalle de terminal");
                        return RedirectToAction("View", "CondEspeCliDetalle", new { id = idPadre });
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