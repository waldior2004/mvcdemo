using com.msc.services.dto;
using System.Collections.Generic;
using System.Web.Mvc;

namespace com.msc.infraestructure.utils
{
    public static class UtilityController
    {
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
    }
}
