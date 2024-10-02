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

        public List<ImpuestoProveedor> ObtGetImpuestosByProveedor(int Id)
        {
            List<ImpuestoProveedor> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.ImpuestoProveedors.Include("Impuesto")
                           where p.IdProveedor == Id && p.AudActivo == 1
                           select p).ToList();

                    foreach (var item in lst)
                    {
                        var tipo = (from p in context.Tablas
                                    where p.Id == item.Impuesto.IdTipoImpuesto && p.AudActivo == 1
                                    select p).FirstOrDefault();
                        item.Impuesto.TipoImpuesto = tipo;
                    }

                }

                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                LogError.PostErrorMessage(ex.InnerException, null);
                return null;
            }
        }

        public ImpuestoProveedor ObtImpuestoProveedor(int Id)
        {
            ImpuestoProveedor obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.ImpuestoProveedors.Include("Impuesto")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    var tipo = (from p in context.Tablas
                                where p.Id == obj.Impuesto.IdTipoImpuesto && p.AudActivo == 1
                                select p).FirstOrDefault();

                    obj.Impuesto.TipoImpuesto = tipo;


                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditImpuestoProveedor(ImpuestoProveedor obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        var exists = (from p in context.ImpuestoProveedors
                                      where p.IdProveedor == obj.IdProveedor &&
                                          p.IdImpuesto == obj.IdImpuesto && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            obj.Proveedor = null;
                            obj.Impuesto = null;
                            obj.AudActivo = 1;
                            context.ImpuestoProveedors.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.ImpuestoProveedors
                                      where p.IdProveedor == obj.IdProveedor
                                        && p.IdImpuesto == obj.IdImpuesto
                                        && p.AudActivo == 1 && p.Id != obj.Id
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            var objUpd = (from p in context.ImpuestoProveedors
                                          where p.Id == obj.Id && p.AudActivo == 1
                                          select p).FirstOrDefault();

                            if (objUpd == null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                            }
                            else
                            {
                                objUpd.IdImpuesto = obj.IdImpuesto;
                                objUpd.AudUpdate = obj.AudUpdate;
                                objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
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

        public Respuesta ElimImpuestoProveedor(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.ImpuestoProveedors
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();

                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        exists.AudActivo = 0;
                        exists.AudUpdate = DateTime.Now;
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
