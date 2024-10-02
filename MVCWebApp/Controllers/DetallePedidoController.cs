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
    public class DetallePedidoController : Controller
    {
        List<DetallePedido> lst = new List<DetallePedido>();
        Respuesta result = new Respuesta();
        

        [Authorization]
        public ActionResult View(int id)
        {
            try
            {
                if(TempData["Message"] != null)
                    ViewBag.Message = "Resultado Ultima Ejecución: " + TempData["Message"];

                var res = (HttpContext.Application["proxySistema"] as ISistema).ObtPedido(id);
                var objR = res.SetPedido();
                return View(objR.DetallePedidos);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        public ActionResult Detail(int id)
        {
            try
            {
                var res = (HttpContext.Application["proxySistema"] as ISistema).ObtDetallePedido(id);
                var objR = res.SetDetallePedido();
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
        public ActionResult Edit(int id)
        {
            try
            {
                var res = (HttpContext.Application["proxySistema"] as ISistema).ObtDetallePedido(id);
                var objR = res.SetDetallePedido();
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
        public ActionResult Edit(DetallePedido obj)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).EditDetallePedido(obj.GetDetallePedidoDTO()).SetRespuesta();
                if (result.Id == 0)
                {
                    return RedirectToAction("View", "DetallePedido", new { id = obj.IdPedido });
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
        public ActionResult New(DetallePedido obj)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).EditDetallePedido(obj.GetDetallePedidoDTO()).SetRespuesta();
                if (result.Id == 0)
                {
                    return RedirectToAction("View", "DetallePedido", new { id = obj.IdPedido });
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimDetallePedido(Convert.ToInt32(item)).SetRespuesta();
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
                    return RedirectToAction("View", "DetallePedido", new { id = idPadre });
                }
                else
                {
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimDetallePedido(Convert.ToInt32(id)).SetRespuesta();
                    if (result.Id == 0)
                    {
                        return RedirectToAction("View", "DetallePedido", new { id = idPadre });
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