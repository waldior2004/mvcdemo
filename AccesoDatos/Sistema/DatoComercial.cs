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

        public DatoComercial ObtDatoComercial(int Id)
        {
            DatoComercial obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.DatoComerciales.Include("Banco").Include("TipoCuenta").Include("Pais").Include("TipoInterlocutor")
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

        public Respuesta EditDatoComercial(DatoComercial obj)
        {
            var objResp = new Respuesta();
            try
            {
                //LogError.PostInfoMessage("DAL - Banco: " + obj.IdBanco.ToString() + ", TipoCuenta: " + obj.IdTipoCuenta.ToString());
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        var exists = (from p in context.DatoComerciales
                                      where p.IdBanco == obj.IdBanco && p.IdTipoCuenta == obj.IdTipoCuenta && p.NroCuenta == obj.NroCuenta && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            obj.TipoCuenta = null;
                            obj.Banco = null;
                            obj.TipoInterlocutor = null;
                            obj.Pais = null;
                            obj.AudActivo = 1;
                            context.DatoComerciales.Add(obj);
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
                        var exists = (from p in context.DatoComerciales
                                      where p.IdBanco == obj.IdBanco && p.IdTipoCuenta == obj.IdTipoCuenta && p.NroCuenta == obj.NroCuenta && p.AudActivo == 1 && p.Id != obj.Id
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {

                            var objUpd = (from p in context.DatoComerciales
                                          where p.Id == obj.Id && p.AudActivo == 1
                                          select p).FirstOrDefault();

                            if (objUpd == null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                            }
                            else
                            {
                                objUpd.IdPais2 = obj.IdPais2;
                                objUpd.IdBanco = obj.IdBanco;
                                objUpd.IdTipoCuenta = obj.IdTipoCuenta;
                                objUpd.IdTipoInterlocutor = obj.IdTipoInterlocutor;
                                objUpd.NroCuenta = obj.NroCuenta;
                                objUpd.NroCCI = obj.NroCCI;
                                objUpd.Swift = obj.Swift;
                                objUpd.AudUpdate = DateTime.Now;
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

        public Respuesta ElimDatoComercial(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.DatoComerciales
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
