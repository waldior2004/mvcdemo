using com.msc.services.dto;
using System.Collections.Generic;
using System.Web.Mvc;

namespace com.msc.infraestructure.utils
{
    public static class UtilityController
    {

        public static void loadSelectEmpresa(this Controller source, List<EmpresaDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Empresa]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstEmpresas = lstItem;
        }

        public static void loadSelectUbigeo(this Controller source, List<UbigeoDTO> lst, int selected, string mensaje)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione " + mensaje + "]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            switch (mensaje)
            {
                case "Departamento":
                    source.ViewBag.lstDeps = lstItem;
                    break;
                case "Provincia":
                    source.ViewBag.lstProvs = lstItem;
                    break;
                case "Distrito":
                    source.ViewBag.lstDists = lstItem;
                    break;
            }
        }

        public static void loadSelectTablas(this Controller source, List<TablaDTO> lst, int selected, string mensaje, string grupo)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione " + mensaje + "]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            switch (grupo)
            {
                case "A01":
                    source.ViewBag.lstTipoPersona = lstItem;
                    break;
                case "A02":
                    source.ViewBag.lstGiroNegocio = lstItem;
                    break;
                case "A03":
                    source.ViewBag.lstNIF = lstItem;
                    break;
                case "A04":
                    source.ViewBag.lstClaseImpuesto = lstItem;
                    break;
                case "A05":
                    source.ViewBag.lstFormaCobro = lstItem;
                    break;
                case "A07":
                    source.ViewBag.lstTipoCuenta = lstItem;
                    break;
                case "A08":
                    source.ViewBag.lstCargo = lstItem;
                    break;
                case "B01":
                    source.ViewBag.lstTipoProducto = lstItem;
                    break;
                case "001":
                    source.ViewBag.lstTerminales = lstItem;
                    break;
                case "002":
                    source.ViewBag.lstPeriodoTars = lstItem;
                    break;
                case "003":
                    source.ViewBag.lstMonedas = lstItem;
                    break;
                case "007":
                    source.ViewBag.lstTipoCarga = lstItem;
                    break;
                case "006":
                    source.ViewBag.lstTipoCondicion = lstItem;
                    break;
                case "008":
                    source.ViewBag.lstTransporte = lstItem;
                    break;
                case "014":
                    source.ViewBag.lstTipoPeticion = lstItem;
                    break;
                case "015":
                    source.ViewBag.lstEstados = lstItem;
                    break;
                case "016":
                    source.ViewBag.lstUniMedidas = lstItem;
                    break;
                case "017":
                    source.ViewBag.lstGrupoProd = lstItem;
                    break;
                case "019":
                    source.ViewBag.lstPaises = lstItem;
                    break;
                case "020":
                    source.ViewBag.lstTipoContribu = lstItem;
                    break;
                case "021":
                    source.ViewBag.lstTipoInterlocutor = lstItem;
                    break;
                case "022":
                    source.ViewBag.lstTipoProvision = lstItem;
                    break;
                case "023":
                    source.ViewBag.lstCuentaContable = lstItem;
                    break;
                case "024":
                    source.ViewBag.lstEstadoProv = lstItem;
                    break;
            }
        }

        public static void loadSelectAreaSolicitante(this Controller source, List<AreaSolicitanteDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Area Solicitante]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstAreaSolicitante = lstItem;
        }

        public static void loadSelectBanco(this Controller source, List<BancoDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Banco]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Codigo + " - " + item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Codigo + " - " + item.Descripcion, Selected = false });
            }
            source.ViewBag.lstBanco = lstItem;
        }

        public static void loadSelectPuerto(this Controller source, List<PuertoDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Puerto]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstPuerto = lstItem;
        }

        public static void loadSelectSucursales(this Controller source, List<SucursalDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Sucursal]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstSucursales = lstItem;
        }

        public static void loadSelectCentroCosto(this Controller source, List<CentroCostoDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Centro de Costo]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstCentroCosto = lstItem;
        }

        public static void loadSelectGrupoTablas(this Controller source, List<GrupoTablaDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Grupo de Tabla]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstGrupoTablas = lstItem;
        }

        public static void loadSelectPerfiles(this Controller source, List<PerfilDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Perfil]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstPerfiles = lstItem;
        }

        public static void loadSelectRoles(this Controller source, List<RolDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Rol]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstRoles = lstItem;
        }

        public static void loadSelectPaginas(this Controller source, List<PaginaDTO> lst, int id, int? selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Padre]" });
            foreach (var item in lst)
            {
                if (item.Id != id)
                {
                    if (item.Id == selected)
                        lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Titulo, Selected = true });
                    else
                        lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Titulo, Selected = false });
                }
            }
            source.ViewBag.lstPaginas = lstItem;
        }

        public static void loadSelectPaginas(this Controller source, List<PaginaDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Página]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Titulo, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Titulo, Selected = false });
            }
            source.ViewBag.lstPaginas = lstItem;
        }

        public static void loadSelectTipoControl(this Controller source, List<TipoControlDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Tipo de Control]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstTipoControl = lstItem;
        }

        public static void loadSelectFamilias(this Controller source, List<FamiliaDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Familia]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstFamilias = lstItem;
        }

        public static void loadSelectSubFamilias(this Controller source, List<SubFamiliaDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Sub Familia]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstSubFamilias = lstItem;
        }

        public static void loadSelectProductos(this Controller source, List<ProductoDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Producto]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstProductos = lstItem;
        }

        public static void loadSelectProveedores(this Controller source, List<ProveedorDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Proveedor]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.NombreComercial, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.NombreComercial, Selected = false });
            }
            source.ViewBag.lstProveedores = lstItem;
        }

        public static void loadSelectImpuesto(this Controller source, List<ImpuestoDTO> lst, int selected)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Value = "0", Text = "[Seleccione Impuesto]" });
            foreach (var item in lst)
            {
                if (item.Id == selected)
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = true });
                else
                    lstItem.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descripcion, Selected = false });
            }
            source.ViewBag.lstImpuesto = lstItem;
        }
    }
}
