using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.msc.frontend.mvc.Controllers
{
    public class ProveedorController : Controller
    {
        List<Proveedor> lst = new List<Proveedor>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult Index()
        {
            try
            {
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtProveedor();
                foreach ( var item in result)
                {
                    lst.Add(item.SetProveedor());                    
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
                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtProveedor(id);
                return View(result.SetProveedor());
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
                //Grupo A01 Tipo Persona
                var lstTP = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A01");
                this.loadSelectTablas(lstTP, 0, "Tipo Persona", "A01");

                //Grupo A04 Tipo Impuesto
                var lstTI = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A04");
                this.loadSelectTablas(lstTI, 0, "Tipo Impuesto", "A04");

                //Grupo A02 Giro Negocio
                var lstGN = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A02");
                this.loadSelectTablas(lstGN, 0, "Giro Negocio", "A02");

                //Grupo A03 NIF
                var lstNIF = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A03");
                this.loadSelectTablas(lstNIF, 0, "Tipo de Identificación", "A03");

                //Grupo A05 Forma Cobro
                var lstFC = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A05");
                this.loadSelectTablas(lstFC, 0, "Condición de Pago", "A05");

                ////Grupo A07 Tipo de Cuenta
                var lstTC = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A07");
                this.loadSelectTablas(lstTC, 0, "Tipo de Cuenta", "A07");

                //Grupo A08 Cargo
                var lstCargo = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A08");
                this.loadSelectTablas(lstCargo, 0, "Cargo", "A08");

                //Grupo 019 paises
                var lstTPais = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("019");
                this.loadSelectTablas(lstTPais, 0, "País", "019");

                ViewBag.lstPaises2 = ViewBag.lstPaises;

                //Grupo 020 Tipo Contribuyente
                var lstTCont = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("020");
                this.loadSelectTablas(lstTCont, 0, "Tipo Contribuyente", "020");

                //Grupo 020 Tipo Interlocutor
                var lstTInt = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("021");
                this.loadSelectTablas(lstTInt, 0, "Tipo Banco Interlocutor", "021");

                //ViewBag.lstPaises = new List<SelectListItem> { new SelectListItem{ Value = "0", Text = "[Seleccione País]"} };
                ViewBag.lstDeps = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "[Seleccione Departamento]" } };
                ViewBag.lstProvs = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "[Seleccione Provincia]" } };
                ViewBag.lstDists = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "[Seleccione Distrito]" } };

                return View("Edit", new Proveedor { Id = 0, TipoPersona = new Tabla { Id = 0 } });
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
        public JsonResult New(Proveedor obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditProveedor(obj.GetProveedorDTO()).SetRespuesta();
                }

                //result.Metodo = "/Proveedor/Index";
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
                //Grupo A01 Tipo Persona
                var lstTP = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A01");
                this.loadSelectTablas(lstTP, 0, "Tipo Persona", "A01");

                //Grupo A04 Tipo Impuesto
                var lstTI = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A04");
                this.loadSelectTablas(lstTI, 0, "Tipo Impuesto", "A04");

                //Grupo A02 Giro Negocio
                var lstGN = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A02");
                this.loadSelectTablas(lstGN, 0, "Giro Negocio", "A02");

                //Grupo A03 NIF
                var lstNIF = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A03");
                this.loadSelectTablas(lstNIF, 0, "Tipo de Identificación", "A03");

                //Grupo A05 Forma Cobro
                var lstFC = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A05");
                this.loadSelectTablas(lstFC, 0, "Condición de Pago", "A05");

                ////Grupo A07 Tipo de Cuenta
                var lstTC = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A07");
                this.loadSelectTablas(lstTC, 0, "Tipo de Cuenta", "A07");

                ////Grupo A08 Cargo
                var lstCargo = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("A08");
                this.loadSelectTablas(lstCargo, 0, "Cargo", "A08");

                //Grupo 019 paises
                var lstTPais = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("019");
                this.loadSelectTablas(lstTPais, 0, "País", "019");

                //Grupo Paises 2
                List<SelectListItem> lstItem = new List<SelectListItem>();
                lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Pais]" });
                foreach (var item in lstTPais)
                {
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
                }

                ViewBag.lstPaises2 = lstItem;

                //Grupo 020 Tipo Contribuyente
                var lstTCont = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("020");
                this.loadSelectTablas(lstTCont, 0, "Tipo Contribuyente", "020");

                //Grupo 020 Tipo Interlocutor
                var lstTInt = (HttpContext.Application["proxySistema"] as ISistema).ObtTablaGrupo("021");
                this.loadSelectTablas(lstTInt, 0, "Tipo Banco Interlocutor", "021");

                var result = (HttpContext.Application["proxySistema"] as ISistema).ObtProveedor(id);

                var objres = result.SetProveedor();

                if (objres.IdPais != null)
                {
                    var lstDeps = (HttpContext.Application["proxySistema"] as ISistema).ObtDepartamento(Convert.ToInt32(objres.IdPais));
                    this.loadSelectUbigeo(lstDeps, 0, "Departamento");
                }
                else
                {
                    ViewBag.lstDeps = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "[Seleccione Departamento]" } };
                }
                if (objres.IdDepartamento != null)
                {
                    var lstProvs = (HttpContext.Application["proxySistema"] as ISistema).ObtProvincia(Convert.ToInt32(objres.IdDepartamento));
                    this.loadSelectUbigeo(lstProvs, 0, "Provincia");
                }
                else
                {
                    ViewBag.lstProvs = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "[Seleccione Provincia]" } };
                }
                if (objres.IdProvincia != null)
                {
                    var lstDists = (HttpContext.Application["proxySistema"] as ISistema).ObtDistrito(Convert.ToInt32(objres.IdProvincia));
                    this.loadSelectUbigeo(lstDists, 0, "Distrito");
                }
                else
                {
                    ViewBag.lstDists = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "[Seleccione Distrito]" } };
                }

                return View(objres);

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
        public JsonResult Edit(Proveedor obj)
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).EditProveedor(obj.GetProveedorDTO()).SetRespuesta();
                }

                result.Metodo = "/Proveedor/Index";
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
                            result = (HttpContext.Application["proxySistema"] as ISistema).ElimProveedor(Convert.ToInt32(item)).SetRespuesta();
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
                    result = (HttpContext.Application["proxySistema"] as ISistema).ElimProveedor(Convert.ToInt32(id)).SetRespuesta();

                result.Metodo = "/Proveedor/Index";
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