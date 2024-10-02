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
        public List<Pagina> ObtPagina()
        {
            List<Pagina> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Paginas
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

        public Pagina ObtPagina(int Id)
        {
            Pagina obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Paginas
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                    obj.PaginaPadre = context.Paginas.Where(p => p.Id == obj.IdPagina && p.AudActivo == 1).FirstOrDefault();
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditPagina(Pagina obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        obj.PaginaPadre = null;
                        obj.IdPagina = (obj.IdPagina == 0 ? null : obj.IdPagina);
                        obj.AudActivo = 1;
                        context.Paginas.Add(obj);
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                    }
                    else
                    {
                        var exists = (from p in context.Paginas
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            exists.Titulo = obj.Titulo;
                            exists.Url = obj.Url;
                            exists.Orden = obj.Orden;
                            exists.IdPagina = (obj.IdPagina == 0 ? null : obj.IdPagina);
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

        public Respuesta ElimPagina(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Paginas
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        var pagH = (from p in context.Paginas
                                        where p.IdPagina == Id
                                        select p);
                        foreach (var item in pagH)
                        {
                            item.AudActivo = 0;
                        }
                        var contH = (from p in context.Controls
                                    where p.IdPagina == Id
                                    select p);
                        foreach (var item in contH)
                        {
                            item.AudActivo = 0;
                        }
                        var pconH = (from p in context.PerfilControls
                                     join q in context.Controls on p.IdControl equals q.Id
                                     where q.IdPagina == Id
                                     select p);
                        foreach (var item in pconH)
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
