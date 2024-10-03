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
        public List<Rol> ObtRol()
        {
            List<Rol> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Rols
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

        public Rol ObtRol(int Id)
        {
            Rol obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Rols
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

        public Respuesta EditRol(Rol obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        var exists = (from p in context.Rols
                                      where p.Descripcion.ToLower() == obj.Descripcion.ToLower() && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            obj.AudActivo = 1;
                            context.Rols.Add(obj);
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.Rols
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var exists2 = (from p in context.Rols
                                           where p.Descripcion.ToLower() == obj.Descripcion.ToLower() && p.AudActivo == 1 && p.Id != obj.Id
                                           select p).FirstOrDefault();

                            if (exists2 == null)
                            {
                                exists.Descripcion = obj.Descripcion;
                                exists.AudUpdate = DateTime.Now;
                                objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                            }
                            else
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
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

        public Respuesta ElimRol(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Rols
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
