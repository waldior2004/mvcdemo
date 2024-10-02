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
    public class CondEspeCliController : Controller
    {
        List<CondEspeCli> lst = new List<CondEspeCli>();
        Respuesta result = new Respuesta();
        int IdTipoCli = 0;
        int IdTipoCar = 0;
        int IdTipoNave = 0;
        int IdTipoViaje = 0;
        private void setTipoForm()
        {
            //Grupo 006 Tipo Condición
            var lstTipoCond = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("006");
            IdTipoCli = (from p in lstTipoCond
                             where p.Codigo == "001"
                             select p.Id).FirstOrDefault();

            IdTipoCar = (from p in lstTipoCond
                             where p.Codigo == "002"
                             select p.Id).FirstOrDefault();

            IdTipoNave = (from p in lstTipoCond
                         where p.Codigo == "003"
                         select p.Id).FirstOrDefault();

            IdTipoViaje = (from p in lstTipoCond
                          where p.Codigo == "004"
                          select p.Id).FirstOrDefault();
        }

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
        public ActionResult Index()
        {
            try
            {
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCli();
                foreach (var item in result)
                {
                    lst.Add(item.SetCondEspeCli());
                }

                setTipoForm();

                ViewBag.lstCondClientes = (from p in lst
                                           where p.IdTipoCond == IdTipoCli
                                           select p).ToList();

                ViewBag.lstCondTipoC = (from p in lst
                                        where p.IdTipoCond == IdTipoCar
                                        select p).ToList();

                ViewBag.lstExcepcional = (from p in lst
                                          where p.IdTipoCond == IdTipoNave || p.IdTipoCond == IdTipoViaje
                                          select p).ToList();

                return View();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        public ActionResult Historial(string Tipo)
        {
            try
            {
                setTipoForm();

                if (Tipo == "BackCliente" || Tipo == "BackTipoCarga" || Tipo == "BackExcepcional")
                {
                    var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCli();

                    foreach (var item in result)
                    {
                        lst.Add(item.SetCondEspeCli());
                    }

                    if (Tipo == "BackCliente")
                    {
                        lst = (from p in lst
                               where p.IdTipoCond == IdTipoCli
                               select p).ToList();

                        return View("Cliente", lst);
                    }
                    else if (Tipo == "BackTipoCarga")
                    {
                        lst = (from p in lst
                               where p.IdTipoCond == IdTipoCar
                               select p).ToList();

                        return View("TipoCarga", lst);
                    }
                    else {
                        lst = (from p in lst
                               where p.IdTipoCond == IdTipoNave || p.IdTipoCond == IdTipoViaje
                               select p).ToList();

                        return View("Excepcional", lst);
                    }
                }
                else
                {
                    var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliHistorico();

                    foreach (var item in result)
                    {
                        lst.Add(item.SetCondEspeCli());
                    }

                    if (Tipo == "Cliente")
                        lst = (from p in lst
                               where p.IdTipoCond == IdTipoCli
                               select p).ToList();
                    else if(Tipo == "TipoCarga")
                        lst = (from p in lst
                               where p.IdTipoCond == IdTipoCar
                               select p).ToList();
                    else
                        lst = (from p in lst
                               where p.IdTipoCond == IdTipoNave || p.IdTipoCond == IdTipoViaje
                               select p).ToList();

                    ViewBag.TipoCondicion = Tipo;

                    return View(lst);
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
        public ActionResult View(int id)
        {
            try
            {
                setTipoForm();

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCli(id);

                if (result.TipoCond.Id == IdTipoCli)
                {
                    ViewBag.LabelRef = "Cliente";
                }
                else if (result.TipoCond.Id == IdTipoCar)
                {
                    ViewBag.LabelRef = "Tipo de Carga";
                }
                else
                {
                    ViewBag.LabelRef = "Excepcional";
                }

                var lstBitacora = (HttpContext.Application["proxySistema"] as ISistema).ObtBitacora(id, "CondEspeCli");
                ViewBag.lstBitacora = lstBitacora;
                return View(result.SetCondEspeCli());
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        public ActionResult New(string Tipo)
        {
            try
            {
                //Grupo 001 Terminales
                var lstP = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("001");
                this.loadSelectTablas(lstP, 0, "Terminal", "001");

                setTipoForm();

                ViewBag.TipoCondicion = Tipo;

                var IdTipo = 0;

                if (Tipo == "Cliente")
                {
                    IdTipo = IdTipoCli;
                }
                else if (Tipo == "TipoCarga")
                {
                    //Grupo 007 Tipo Carga
                    var lstTipoCarga = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("007");
                    this.loadSelectTablas(lstTipoCarga, 0, "Tipo de Carga", "007");

                    IdTipo = IdTipoCar;
                }
                else
                {
                    //Grupo 006 Tipo Condiciion
                    var lstExcepcional = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("006");
                    this.loadSelectTablas(lstExcepcional, 0, "Tipo de Excepción", "006");
                    (ViewBag.lstTipoCondicion as List<SelectListItem>).RemoveAll(p => p.Value == IdTipoCli.ToString() || p.Value == IdTipoCar.ToString() || p.Value == "0");

                    IdTipo = IdTipoNave;
                }

                return View("EditCliente", new CondEspeCli { Id = 0, IdTipoCond = IdTipo });
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
        public JsonResult New(CondEspeCli obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditCondEspeCli(obj.GetCondEspeCliDTO()).SetRespuesta();

                    if (result.Id == 0)
                    {
                        SetBitacora(BitacoraActionCode.Insercion, result.Metodo, "Se registró la condición especial");
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
                this.loadSelectTablas(lstP, 0, "Terminal", "001");

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCli(id);
                var objR = result.SetCondEspeCli();

                setTipoForm();

                if (objR.IdTipoCond == IdTipoCli)
                {
                    ViewBag.TipoCondicion = "Cliente";
                    return View("EditCliente", objR);
                }
                else if (objR.IdTipoCond == IdTipoCar)
                {
                    //Grupo 007 Tipo Carga
                    var lstTipoCarga = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("007");
                    this.loadSelectTablas(lstTipoCarga, 0, "Tipo de Carga", "007");

                    ViewBag.TipoCondicion = "TipoCarga";
                    return View("EditCliente", objR);
                }
                else
                {
                    //Grupo 006 Tipo Condiciion
                    var lstExcepcional = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("006");
                    this.loadSelectTablas(lstExcepcional, 0, "Tipo de Excepción", "006");
                    (ViewBag.lstTipoCondicion as List<SelectListItem>).RemoveAll(p => p.Value == IdTipoCli.ToString() || p.Value == IdTipoCar.ToString() || p.Value == "0");

                    ViewBag.TipoCondicion = "Excepcional";
                    return View("EditCliente", objR);
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
        [ValidateAntiForgeryToken]
        public JsonResult Edit(CondEspeCli obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditCondEspeCli(obj.GetCondEspeCliDTO()).SetRespuesta();

                    if (result.Id == 0)
                    {
                        SetBitacora(BitacoraActionCode.Actualizacion, obj.Id.ToString(), "Se modificó la condición especial");
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
        public ActionResult Delete(string id, string Tipo)
        {
            try
            {
                setTipoForm();

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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimCondEspeCli(Convert.ToInt32(item)).SetRespuesta();
                            if (result.Id == 0)
                            {
                                OK++;
                                Message += string.Format("OK({0})", item);
                                SetBitacora(BitacoraActionCode.Eliminacion, item, "Se eliminó la condición especial");
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
                    SetBitacora(BitacoraActionCode.Eliminacion, id, "Se eliminó la(s) condicion(es) especial(es)");
                    ViewBag.Message = result.Message;
                }
                else
                {
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimCondEspeCli(Convert.ToInt32(id)).SetRespuesta();

                    if (result.Id == 0)
                    {
                        SetBitacora(BitacoraActionCode.Eliminacion, id, "Se eliminó la condición especial");
                    }
                }

                var lstResult = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCli();
                foreach (var item in lstResult)
                {
                    lst.Add(item.SetCondEspeCli());
                }

                if (Tipo == "Cliente")
                {
                    lst = (from p in lst
                           where p.IdTipoCond == IdTipoCli
                           select p).ToList();
                    return View("Cliente", lst);
                }
                else if (Tipo == "TipoCarga")
                {
                    lst = (from p in lst
                           where p.IdTipoCond == IdTipoCar
                           select p).ToList();
                    return View("TipoCarga", lst);
                }
                else
                {
                    lst = (from p in lst
                           where p.IdTipoCond == IdTipoNave || p.IdTipoCond == IdTipoViaje
                           select p).ToList();
                    return View("Excepcional", lst);
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