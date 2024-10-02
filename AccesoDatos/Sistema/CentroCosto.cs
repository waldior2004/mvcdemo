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

        public List<CentroCosto> ObtCentroCosto()
        {
            List<CentroCosto> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.CentroCostos
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

        public CentroCosto ObtCentroCosto(int Id)
        {
            CentroCosto obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.CentroCostos
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

        public Respuesta EditCentroCosto(CentroCosto obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        var codeex = (from p in context.CentroCostos
                                      where p.Descripcion.ToUpper() == obj.Descripcion.ToUpper() && p.AudActivo == 1 
                                      select p).FirstOrDefault();

                        if (codeex != null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                        else
                        {
                            obj.AudActivo = 1;
                            context.CentroCostos.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();
                        }
                    }
                    else
                    {
                        var exists = (from p in context.CentroCostos
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var codeex = (from p in context.CentroCostos
                                          where p.Descripcion.ToUpper() == obj.Descripcion.ToUpper() && p.AudActivo == 1 && p.Id != obj.Id
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

        public Respuesta ElimCentroCosto(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.CentroCostos
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
