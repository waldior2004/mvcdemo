using com.msc.infraestructure.entities;
using com.msc.services.interfaces;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.msc.infraestructure.utils;

namespace com.msc.frontend.mvc.Controllers
{
    public class DatoComercialController : Controller
    {
        List<DatoComercial> lst = new List<DatoComercial>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult View(int id)
        {
            try
            {
                if (TempData["Message"] != null)
                    ViewBag.Message = "Resultado Ultima Ejecución: " + TempData["Message"];

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtProveedor(id);
                var objR = result.SetProveedor();
                return View(objR.DatoComercial);
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
                ////Grupo 019 Pais
                var lstPais = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("019");
                this.loadSelectTablas(lstPais, 0, "Pais", "019");

                ////Grupo A07 Tipo de Cuenta
                var lstTC = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A07");
                this.loadSelectTablas(lstTC, 0, "Tipo de Cuenta", "A07");

                ////Grupo 021 Tipo Banco Interlocutor
                var lstTI = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("021");
                this.loadSelectTablas(lstTI, 0, "Tipo Banco Interlocutor", "021");

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtDatoComercial(id);
                ////Grupo A06 Banco
                var lstBanco = (HttpContext.Application["proxySistema"] as ISistema).ObtBanco().FindAll(p => p.Pais.Id == result.IdPais2);
                this.loadSelectBanco(lstBanco, 0);

                var objR = result.SetDatoComercial();

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
        public ActionResult Edit(DatoComercial obj)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).EditDatoComercial(obj.GetDatoComercialDTO()).SetRespuesta();
                //LogError.PostInfoMessage("MVC - Banco: " + obj.IdBanco.ToString() + ", TipoCuenta: " + obj.IdTipoCuenta.ToString() );
                if (result.Id == 0)
                {
                    return RedirectToAction("View", "DatoComercial", new { id = obj.IdProveedor });
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimDatoComercial(Convert.ToInt32(item)).SetRespuesta();
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
                    return RedirectToAction("View", "DatoComercial", new { id = idPadre });
                }
                else
                {
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimDatoComercial(Convert.ToInt32(id)).SetRespuesta();
                    if (result.Id == 0)
                    {
                        return RedirectToAction("View", "DatoComercial", new { id = idPadre });
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