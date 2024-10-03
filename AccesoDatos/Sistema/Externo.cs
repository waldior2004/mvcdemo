using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        public Externo ObtExterno(string userName)
        {
            Externo obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Externos.Include("Terminal")
                           where p.Usuario.Equals(userName.Trim()) && p.AudActivo == 1
                           select p).FirstOrDefault();
                    if (obj != null)
                    {
                        var lstPerfiles = (from p in context.ExternoPerfils.Include("Perfil")
                                           where p.IdExterno == obj.Id && p.AudActivo == 1
                                           select p).ToList();

                        obj.ExternoPerfils = lstPerfiles;
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


        public List<Externo> ObtExterno()
        {
            List<Externo> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Externos.Include("Terminal")
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
        public Externo ObtExterno(int Id)
        {
            Externo obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Externos.Include("Terminal")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                    if (obj != null)
                    {
                        var lstPerfiles = (from p in context.ExternoPerfils.Include("Perfil")
                                           where p.IdExterno == Id && p.AudActivo == 1
                                           select p).ToList();

                        obj.ExternoPerfils = lstPerfiles;
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
        public Respuesta ResetKeyExterno(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                        var exists = (from p in context.Externos
                                      where p.Id == Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            exists.Clave = Encrypt.GetPasswordGenerated();
                            exists.EsInicio = 1;
                            exists.AudUpdate = DateTime.Now;
                            objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                            context.SaveChanges();
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
        public Respuesta EditExterno(Externo obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        obj.EsInicio = 1;
                        obj.Clave = Encrypt.GetPasswordGenerated();
                        obj.AudActivo = 1;
                        context.Externos.Add(obj);
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.Entry(obj).GetDatabaseValues();
                        objResp.Metodo = obj.Id.ToString();
                    }
                    else
                    {
                        var exists = (from p in context.Externos
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            exists.DescTerminal = obj.DescTerminal;
                            exists.Contacto = obj.Contacto;
                            exists.Ruc = obj.Ruc;
                            exists.Usuario = obj.Usuario;
                            exists.Email1 = obj.Email1;
                            exists.Email2 = obj.Email2;
                            exists.Telefono1 = obj.Telefono1;
                            exists.Telefono2 = obj.Telefono2;
                            exists.AudUpdate = DateTime.Now;
                            objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                            context.SaveChanges();
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
        public Respuesta ElimExterno(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Externos
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {

                        var docs = (from p in context.ExternoPerfils
                                    where p.IdExterno == Id
                                    select p);

                        foreach (var item in docs)
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
