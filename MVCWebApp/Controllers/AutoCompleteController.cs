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
    public class AutoCompleteController : Controller
    {

        public JsonResult Naves(string desc)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllNave(desc);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value = item.Descripcion, data = item.Id.ToString() });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult Viajes(string desc)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllViaje(desc);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value = item.Descripcion, data = item.Id });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult Clientes(string desc)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllCliente(desc);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value= item.Descripcion, data = item.Id.ToString() });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult Proveedores(string desc)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllProveedor(desc);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value = item.RazonSocial, data = item.Id.ToString() });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult Pedidos(string desc)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllPedido(desc);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value = item.Codigo, data = item.Id.ToString() });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult Cotizaciones(string desc)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllCotizacion(desc);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value = item.Codigo, data = item.Id.ToString() });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult ProveedoresTarifaProducto(string desc, int id)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllProveedorTarifaProducto(desc, id);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value = item.RazonSocial, data = item.Id.ToString(), unimed = item.TipoNIF.Id.ToString(), precio = item.Telefono });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult Productos(string desc)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllProducto(desc);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value = item.Descripcion, data = item.Id.ToString(), unimed = item.UnidadMedida.Descripcion, precio = "0" });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult ProductosxProveedor(string desc, int id)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllProductoxProveedor(desc, id);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete
                    {
                        value = item.Descripcion,
                        data = item.Id.ToString(),
                        unimed = item.UnidadMedida.Descripcion,
                        precio = item.Observaciones,
                        idtar = item.Abreviatura
                    });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public JsonResult OrdenesCompra(string desc)
        {
            try
            {
                var lstJson = new List<AutoComplete>();
                var lst = (HttpContext.Application["proxySistema"] as ISistema).ObtAllOrdenCompra(desc);
                foreach (var item in lst)
                {
                    lstJson.Add(new AutoComplete { value = item.Codigo, data = item.Id.ToString() });
                }
                return Json(lstJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }


    }
}