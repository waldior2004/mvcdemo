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
        public List<Familia> ObtFamilia()
        {
            List<Familia> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Familias
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

        public Familia ObtFamilia(int Id)
        {
            Familia obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Familias
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

        public Respuesta EditFamilia(Familia obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        var codeex = (from p in context.Familias
                                      where p.Descripcion.ToLower() == obj.Descripcion.ToLower() && p.AudActivo == 1 && p.Id != obj.Id
                                      select p).FirstOrDefault();

                        if (codeex != null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                        else
                        {
                            obj.AudActivo = 1;
                            context.Familias.Add(obj);
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.Familias
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var codeex = (from p in context.Familias
                                          where p.Descripcion.ToLower() == obj.Descripcion.ToLower() && p.AudActivo == 1 && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (codeex != null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                            }
                            else
                            {
                                exists.Abreviatura = obj.Abreviatura;
                                exists.Descripcion = obj.Descripcion;
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

        public Respuesta ElimFamilia(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Familias
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        var fam = (from p in context.SubFamilias
                                      where p.IdFamilia == Id
                                      select p);

                        foreach (var item in fam)
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