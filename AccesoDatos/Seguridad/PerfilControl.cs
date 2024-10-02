using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        private void UpdateXMLPerfil(int id)
        {
            using (var context = new CompanyContext())
            {
                var obj = (from p in context.Perfils
                       where p.Id == id && p.AudActivo == 1
                       select p).FirstOrDefault();
                if (obj != null)
                {
                    var lstControles = (from p in context.PerfilControls.Include("Control")
                                        where p.IdPerfil == id && p.AudActivo == 1 && p.Estado == 1
                                        select p).ToList();
                    obj.Permisos = MyHtmlHelpers.BuildPermisos(lstControles);
                    context.SaveChanges();
                }
            }
        }

        private void UpdateXMLMenu(int id)
        {
            using (var context = new CompanyContext())
            {
                var obj = (from p in context.Perfils
                           where p.Id == id && p.AudActivo == 1
                           select p).FirstOrDefault();
                if (obj != null)
                {
                    StringBuilder strList = new StringBuilder();
                    strList.Append("<div id='sidebar-menu' class='main_menu_side hidden-print main_menu'><div class='menu_section'><h3>General</h3>");
                    strList.Append("<ul class='nav side-menu'><li><a><i class='fa fa-home'></i> Inicio <span class='fa fa-chevron-down'></span></a>");
                    strList.Append("<ul class='nav child_menu'><li><a href='/Home/Index'>Dashboard</a></li></ul></li>");
                    var lst1 = (from p in context.Paginas
                                where p.Url == "#" && p.AudActivo == 1 
                                select p).OrderBy(q => q.Orden).ToList();

                    foreach (var item in lst1)
                    {
                        var subitems = (from p in context.Paginas
                                        join r in context.Controls on p.Id equals r.IdPagina
                                        join q in context.PerfilControls on r.Id equals q.IdControl
                                        where p.IdPagina == item.Id && p.AudActivo == 1 && q.IdPerfil == id && q.AudActivo == 1 && q.Estado == 1 && r.AudActivo == 1
                                        && (r.Url.Contains("/Index") || r.Url.Contains("/Reporte/"))
                                        select p).DistinctBy(p=>p.Id).OrderBy(q=>q.Orden).ToList();
                        if (subitems.Count > 0)
                        {
                            strList.Append("<li>");
                            strList.Append("<a><i class='fa fa-edit'></i> " + item.Titulo + " <span class='fa fa-chevron-down'></span></a>");
                            strList.Append("<ul class='nav child_menu'>");
                            foreach (var subi in subitems)
                            {
                                strList.Append("<li><a href='" + subi.Url + "'>" + subi.Titulo + "</a></li>");
                            }
                            strList.Append("</ul>");
                            strList.Append("</li>");
                        }
                    }
                    strList.Append("</ul></div></div>");
                    obj.MenuSup = strList.ToString();
                    context.SaveChanges();
                }
            }
        }

        public Respuesta EditPerfilControl(PerfilControl obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        var exists = (from p in context.PerfilControls
                                      where p.IdControl == obj.IdControl && p.IdPerfil == obj.IdPerfil && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            obj.AudActivo = 1;
                            context.PerfilControls.Add(obj);
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.PerfilControls
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            exists.Estado = obj.Estado;
                            exists.AudUpdate = DateTime.Now;
                            objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                        }
                    }
                    context.SaveChanges();
                }
                UpdateXMLPerfil(obj.IdPerfil);
                UpdateXMLMenu(obj.IdPerfil);
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return MyException.OnException(ex);
            }
        }

        public Respuesta ElimPerfilControl(int Id)
        {
            var objResp = new Respuesta();
            var idPerf = 0;
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.PerfilControls
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        idPerf = exists.IdPerfil;
                        exists.AudActivo = 0;
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.DeleteOK);
                    }
                }
                if (idPerf > 0)
                {
                    UpdateXMLPerfil(idPerf);
                    UpdateXMLMenu(idPerf);
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, new Respuesta { Id = Id });
                return MyException.OnException(ex);
            }
        }
    }
}