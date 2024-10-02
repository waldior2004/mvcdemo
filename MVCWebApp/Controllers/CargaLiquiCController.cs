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
    public class CargaLiquiCController : Controller
    {
        List<CargaLiquiC> lst = new List<CargaLiquiC>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult Index()
        {
            try
            {
                TempData["BackUrl"] = null;

                var user = (Session["usuario"] as ExternoDTO);

                if (user.Terminal != null)
                {
                    //Grupo 001 Terminales
                    var lstP = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("001");
                    this.loadSelectTablas(lstP, user.Terminal.Id, "Terminal", "001");
                    (ViewBag.lstTerminales as List<SelectListItem>).RemoveAll(p=>p.Value != "0" && p.Value != user.Terminal.Id.ToString());
                }
                else {
                    ViewBag.lstTerminales = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "[Seleccione Terminal]" } };
                }

                var lstPuer = (HttpContext.Application["proxySistema"] as ISistema).ObtPuerto();
                this.loadSelectPuerto(lstPuer, 0);

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCargaLiquiC();
                foreach (var item in result)
                {
                    lst.Add(item.SetCargaLiquiC());
                }
                return View(lst.OrderByDescending(p => p.FecRegistro).ToList());
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
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCargaLiqui(id);
                return View(result.SetCargaLiqui());
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
                if (TempData["BackUrl"] != null)
                    ViewBag.UrlBack = TempData["BackUrl"];
                else
                    ViewBag.UrlBack = "/CargaLiquiC/Index";

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCargaLiquiC(id);
                return View(result.SetCargaLiquiC());
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
        public JsonResult Enviar(string id)
        {
            try
            {
                var user = (Session["usuario"] as ExternoDTO);

                result = (HttpContext.Application["proxySistema"] as ISistema).EnviarCargaLiquiC(id, user.Email1).SetRespuesta();
                result.Metodo = "/CargaLiquiC/Index";
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimCargaLiquiC(Convert.ToInt32(item)).SetRespuesta();
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimCargaLiquiC(Convert.ToInt32(id)).SetRespuesta();

                result.Metodo = "/CargaLiquiC/Index";
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