using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace com.msc.frontend.mvc.Controllers
{
    public class GenCotizaController : Controller
    {
        List<Pedido> lst = new List<Pedido>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult Index()
        {
            try
            {

                TempData["BackUrl"] = "/GenCotiza/Index";

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtPedidoPorCotizar();
                foreach (var item in result)
                {
                    lst.Add(item.SetPedido());
                }
                return View(lst.OrderByDescending(p => p.FechaPeticion).ToList());
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
                List<Tarifario> lst = new List<Tarifario>();
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtPedido(id);
                var lstT = (HttpContext.Application["proxySistema"] as ISistema).ObtPedidoMapProductos(id);
                foreach (var item in lstT)
                {
                    lst.Add(item.SetTarifario());
                }
                ViewBag.Tarifas = lst;
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
        public ActionResult Delete(string id, int idPadre)
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimCotizacion(Convert.ToInt32(item)).SetRespuesta();
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimCotizacion(Convert.ToInt32(id)).SetRespuesta();

                if (result.Id == 0)
                {
                    var objP = (HttpContext.Application["proxySistema"] as ISistema).ObtPedido(idPadre);
                    return View("View", objP.SetPedido().Cotizaciones);
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
        public ActionResult Cotizar(int id, int idPadre)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).GenerarCotizacion(id, idPadre).SetRespuesta();

                if (result.Id == 0)
                {
                    var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtPedido(idPadre);
                    return View("View", lst.SetPedido().Cotizaciones);
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
        public ActionResult Compare(int id)
        {
            try
            {
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtPedido(id);

                CotizacionDTO objBest = new CotizacionDTO();
                decimal total = 1000000000;

                foreach (var item in result.Cotizaciones)
                {
                    if(total > item.DetalleCotizaciones.Sum(p => p.Total))
                    {
                        total = item.DetalleCotizaciones.Sum(p => p.Total);
                        objBest = item;
                    }
                }

                ViewBag.BestCot = objBest.SetCotizacion();
                ViewBag.BestPrice = total;

                var conta = 1;
                List<SelectListItem> lstProductos = new List<SelectListItem>();
                List<Producto> lstProd = new List<Producto>();

                lstProductos.Add(new SelectListItem { Value = "0", Text = "[Seleccione Producto]" });
                foreach (var item in result.DetallePedidos)
                {
                    lstProductos.Add(new SelectListItem { Value = conta.ToString(), Text = item.Producto.Descripcion });
                    conta++;
                    lstProd.Add(new Producto { Id = item.Producto.Id });
                }

                ViewBag.lstProd = lstProd;
                ViewBag.lstProducto = lstProductos;

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
        public JsonResult GenerarOC(int id)
        { 
            try
            {
                string usuario = (Session["Usuario"] as ExternoDTO).Usuario;
                result = (HttpContext.Application["proxySistema"] as ISistema).GenerarOC(id, usuario).SetRespuesta();
                result.Metodo = "/GenCotiza/Index";
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