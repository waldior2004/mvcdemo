using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        public Tabla ObtTabla(string desc)
        {
            Tabla obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Tablas
                           where p.Descripcion.ToUpper().Contains(desc.ToUpper()) && p.AudActivo == 1
                           select p).FirstOrDefault();
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Tabla> ObtTabla()
        {
            List<Tabla> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Tablas.Include("GrupoTabla")
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

        public Tabla ObtTabla(int Id)
        {
            Tabla obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Tablas.Include("GrupoTabla")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Tabla> ObtTablaGrupo(string Codigo)
        {
            List<Tabla> obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Tablas
                           join q in context.GrupoTablas on p.IdGrupoTabla equals q.Id
                           where q.Codigo == Codigo && p.AudActivo == 1 && q.AudActivo == 1
                           select p).OrderBy(p=>p.Orden).ToList();
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditTabla(Tabla obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        var codeex = (from p in context.Tablas
                                      where (p.Codigo == obj.Codigo || p.Descripcion.ToLower() == obj.Descripcion.ToLower()) 
                                      && p.IdGrupoTabla == obj.IdGrupoTabla && p.AudActivo == 1 && p.Id != obj.Id
                                      select p).FirstOrDefault();

                        if (codeex != null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                        else
                        {
                            obj.GrupoTabla = null;
                            obj.AudActivo = 1;
                            context.Tablas.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();
                        }
                    }
                    else
                    {
                        var exists = (from p in context.Tablas
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var codeex = (from p in context.Tablas
                                          where (p.Codigo == obj.Codigo || p.Descripcion.ToLower() == obj.Descripcion.ToLower()) 
                                          && p.IdGrupoTabla == obj.IdGrupoTabla && p.AudActivo == 1 && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (codeex != null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                            }
                            else
                            {
                                exists.IdGrupoTabla = obj.IdGrupoTabla;
                                exists.Codigo = obj.Codigo;
                                exists.Orden = obj.Orden;
                                exists.Descripcion = obj.Descripcion;
                                exists.Breve = obj.Breve;
                                exists.AudUpdate = DateTime.Now;
                                objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                            }
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

        public Respuesta ElimTabla(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Tablas
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
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
