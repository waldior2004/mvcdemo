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
        public List<Banco> ObtBanco()
        {
            List<Banco> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Bancos.Include("Pais")
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

        public Banco ObtBanco(int Id)
        {
            Banco obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Bancos.Include("Pais")
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

        public Respuesta EditBanco(Banco obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        var codeex = (from p in context.Bancos
                                      where p.Codigo == obj.Codigo && p.AudActivo == 1 
                                      select p).FirstOrDefault();

                        if (codeex != null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                        else
                        {
                            obj.Pais = null;
                            obj.AudActivo = 1;
                            context.Bancos.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();
                        }
                    }
                    else
                    {
                        var exists = (from p in context.Bancos
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var codeex = (from p in context.Bancos
                                          where p.Codigo == obj.Codigo && p.AudActivo == 1 && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (codeex != null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                            }
                            else
                            {
                                exists.IdPais = obj.IdPais;
                                exists.Codigo = obj.Codigo;
                                exists.Descripcion = obj.Descripcion;
                                exists.Direccion = obj.Direccion;
                                exists.Swift = obj.Swift;
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

        public Respuesta ElimBanco(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Bancos
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {

                        var dcoms = (from p in context.DatoComerciales
                                      where p.IdBanco == Id && p.AudActivo == 1
                                      select p);

                        foreach (var item in dcoms)
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