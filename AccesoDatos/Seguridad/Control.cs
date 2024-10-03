using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        public List<Control> ObtControl()
        {
            List<Control> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Controls.Include("Pagina").Include("TipoControl")
                           where p.AudActivo == 1
                           select p).ToList();
                   
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Control ObtControl(int Id)
        {
            Control obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Controls
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                    obj.Pagina = context.Paginas.Where(p => p.Id == obj.IdPagina && p.AudActivo == 1).FirstOrDefault();
                    obj.TipoControl = context.TipoControls.Where(p => p.Id == obj.IdTipoControl && p.AudActivo == 1).FirstOrDefault();
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditControl(Control obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        obj.Pagina = null;
                        obj.TipoControl = null;
                        obj.AudActivo = 1;
                        context.Controls.Add(obj);
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                    }
                    else
                    {
                        var exists = (from p in context.Controls
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            exists.IdPagina = obj.IdPagina;
                            exists.IdTipoControl = obj.IdTipoControl;
                            exists.Url = obj.Url;
                            exists.Descripcion = obj.Descripcion;
                            exists.AudUpdate = DateTime.Now;
                            objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                        }
                    }
                    context.SaveChanges();
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return MyException.OnException(ex);
            }
        }

        public Respuesta ElimControl(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Controls
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        var perfilCo = (from p in context.PerfilControls
                                        where p.IdControl == Id
                                        select p);
                        foreach (var item in perfilCo)
                        {
                            item.AudActivo = 0;
                        }
                        exists.AudActivo = 0;
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.DeleteOK);
                    }
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
