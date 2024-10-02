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
    public class PerfilControlController : Controller
    {
        List<PerfilControl> lst = new List<PerfilControl>();
        Respuesta result = new Respuesta();
        

        [Authorization]
        public ActionResult View(int id)
        {
            try
            {
                if(TempData["Message"] != null)
                    ViewBag.Message = "Resultado Ultima Ejecución: " + TempData["Message"];

                var res = (HttpContext.Application["proxySeguridad"] as ISeguridad).ObtPerfil(id);
                var objR = res.SetPerfil();
                return View(objR.PerfilControls);
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
        public ActionResult Edit(PerfilControl obj)
        {
            try
            {
                result = (HttpContext.Application["proxySeguridad"] as ISeguridad).EditPerfilControl(obj.GetPerfilControlDTO()).SetRespuesta();
                if (result.Id == 0)
                {
                    return RedirectToAction("View", "PerfilControl", new { id = obj.IdPerfil });
                }
                else {
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
        public ActionResult EditGroup(string Ids, int IdPerfil, int Estado)
        {
            try
            {
                if (Ids.IndexOf(",") >= 0)
                {
                    var OK = 0;
                    var Fail = 0;
                    var Message = "";
                    var codes = Ids.Split(',');
                    foreach (var item in codes)
                    {
                        var obj = new PerfilControlDTO {
                            Id = Convert.ToInt32(item),
                            IdPerfil = IdPerfil,
                            IdControl = 0,
                            Estado = Estado
                        };
                        if (item != "")
                        {
                            result = (HttpContext.Application["proxySeguridad"] as ISeguridad).EditPerfilControl(obj).SetRespuesta();
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
                    return RedirectToAction("View", "PerfilControl", new { id = IdPerfil });
                }
                else
                {
                    var obj = new PerfilControlDTO
                    {
                        Id = Convert.ToInt32(Ids),
                        IdPerfil = IdPerfil,
                        IdControl = 0,
                        Estado = Estado
                    };
                    result = (HttpContext.Application["proxySeguridad"] as ISeguridad).EditPerfilControl(obj).SetRespuesta();
                    if (result.Id == 0)
                    {
                        return RedirectToAction("View", "PerfilControl", new { id = IdPerfil });
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
                            result = (HttpContext.Application["proxySeguridad"] as ISeguridad).ElimPerfilControl(Convert.ToInt32(item)).SetRespuesta();
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
                    return RedirectToAction("View", "PerfilControl", new { id = idPadre });
                }
                else
                {
                    result = (HttpContext.Application["proxySeguridad"] as ISeguridad).ElimPerfilControl(Convert.ToInt32(id)).SetRespuesta();
                    if (result.Id == 0)
                    {
                        return RedirectToAction("View", "PerfilControl", new { id = idPadre });
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