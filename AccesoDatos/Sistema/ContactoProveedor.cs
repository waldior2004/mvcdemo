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

        public List<ContactoProveedor> ObtGetContactosByProveedor(int Id)
        {
            List<ContactoProveedor> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.ContactoProveedores
                           where p.IdProveedor == Id && p.AudActivo == 1
                           select p).ToList();
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

        public ContactoProveedor ObtContactoProveedor(int Id)
        {
            ContactoProveedor obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.ContactoProveedores.Include("Cargo")
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

        public Respuesta EditContactoProveedor(ContactoProveedor obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        var exists = (from p in context.ContactoProveedores
                                      where p.IdProveedor == obj.IdProveedor &&
                                          p.IdCargo == obj.IdCargo &&
                                          p.Correo.ToLower() == obj.Correo.ToLower() && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            obj.Cargo = null;
                            obj.AudActivo = 1;
                            obj.AudInsert = DateTime.Now;
                            context.ContactoProveedores.Add(obj);
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
                        var exists = (from p in context.ContactoProveedores
                                      where p.IdProveedor == obj.IdProveedor
                                        && p.IdCargo == obj.IdCargo
                                        && p.Correo.ToLower() == obj.Correo.ToLower()
                                        && p.AudActivo == 1
                                        && p.Id != obj.Id
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {

                            var objUpd = (from p in context.ContactoProveedores
                                          where p.Id == obj.Id && p.AudActivo == 1
                                          select p).FirstOrDefault();

                            if (objUpd == null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                            }
                            else
                            {
                                objUpd.IdCargo = obj.IdCargo;
                                objUpd.NombreCompleto = obj.NombreCompleto;
                                objUpd.Correo = obj.Correo;
                                objUpd.IndContacto = obj.IndContacto;
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

        public Respuesta ElimContactoProveedor(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.ContactoProveedores
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
