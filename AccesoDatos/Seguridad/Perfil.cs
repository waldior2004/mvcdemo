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
        public List<Perfil> ObtPerfil()
        {
            List<Perfil> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Perfils
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

        public Perfil ObtPerfil(int Id)
        {
            Perfil obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Perfils
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                    if (obj != null)
                    {
                        var lstControles = (from p in context.PerfilControls.Include("Control")
                                            where p.IdPerfil == Id && p.AudActivo == 1
                                            select p).ToList();

                        lstControles.ForEach(p => p.Control.Pagina = context.Paginas.Where(q => q.Id == p.Control.IdPagina && q.AudActivo == 1).FirstOrDefault());

                        obj.PerfilControls = lstControles;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditPerfil(Perfil obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        obj.AudActivo = 1;
                        context.Perfils.Add(obj);
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.Entry(obj).GetDatabaseValues();
                        objResp.Metodo = obj.Id.ToString();
                    }
                    else
                    {
                        var exists = (from p in context.Perfils
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            exists.Descripcion = obj.Descripcion;
                            exists.AudUpdate = DateTime.Now;
                            objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                        }
                        context.SaveChanges();
                    }
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return MyException.OnException(ex);
            }
        }

        public Respuesta ElimPerfil(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Perfils
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        exists.AudActivo = 0;
                        var perfilCo = (from p in context.PerfilControls
                                        where p.IdPerfil == Id
                                        select p);
                        foreach (var item in perfilCo)
                        {
                            item.AudActivo = 0;
                        }
                        var perfilUsu = (from p in context.UsuarioPerfils
                                         where p.IdPerfil == Id
                                         select p);
                        foreach (var item in perfilUsu)
                        {
                            item.AudActivo = 0;
                        }
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
