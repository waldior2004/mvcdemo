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
    public class ContactoProveedorController : Controller
    {
        List<ContactoProveedor> lst = new List<ContactoProveedor>();
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
                return View(objR.ContactoProveedor);
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
                //Grupo A08 Cargo
                var lstTC = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A08");
                this.loadSelectTablas(lstTC, 0, "Cargo", "A08");

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtContactoProveedor(id);
                var objR = result.SetContactoProveedor();

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
        public ActionResult Edit(ContactoProveedor obj)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).EditContactoProveedor(obj.GetContactoProveedorDTO()).SetRespuesta();
                if (result.Id == 0)
                {
                    return RedirectToAction("View", "ContactoProveedor", new { id = obj.IdProveedor });
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimContactoProveedor(Convert.ToInt32(item)).SetRespuesta();
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
                    return RedirectToAction("View", "ContactoProveedor", new { id = idPadre });
                }
                else
                {
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimContactoProveedor(Convert.ToInt32(id)).SetRespuesta();
                    if (result.Id == 0)
                    {
                        return RedirectToAction("View", "ContactoProveedor", new { id = idPadre });
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