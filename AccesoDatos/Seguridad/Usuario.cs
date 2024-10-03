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
        public List<Usuario> ObtUsuario()
        {
            List<Usuario> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Usuarios.Include("Rol")
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

        public Usuario ObtUsuario(int Id)
        {
            Usuario obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Usuarios.Include("Rol")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                    if (obj != null)
                    {
                        var lstPerfiles = (from p in context.UsuarioPerfils.Include("Perfil")
                                            where p.IdUsuario == Id && p.AudActivo == 1
                                            select p).ToList();

                        obj.UsuarioPerfils = lstPerfiles;
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

        public Respuesta EditUsuario(Usuario obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        if (obj.Clave != "" && obj.Clave != null)
                        {
                            using (MD5 md5Hash = MD5.Create())
                            {
                                obj.Clave = Encrypt.GetMd5Hash(md5Hash, obj.Clave);
                                obj.Compare = Encrypt.GetMd5Hash(md5Hash, obj.Compare);
                            }
                        }
                        obj.Rol = null;
                        obj.AudActivo = 1;
                        context.Usuarios.Add(obj);
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.Entry(obj).GetDatabaseValues();
                        objResp.Metodo = obj.Id.ToString();
                    }
                    else
                    {
                        var exists = (from p in context.Usuarios
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            if (obj.Clave != "" && obj.Clave != null)
                            {
                                using (MD5 md5Hash = MD5.Create())
                                {
                                    obj.Clave = Encrypt.GetMd5Hash(md5Hash, obj.Clave);
                                    obj.Compare = Encrypt.GetMd5Hash(md5Hash, obj.Compare);
                                }
                                exists.Clave = obj.Clave;
                                exists.Compare = obj.Compare;
                            }
                            else
                            {
                                exists.Compare = exists.Clave;
                            }
                            exists.IdRol = obj.IdRol;
                            exists.Login = obj.Login;
                            exists.Descripcion = obj.Descripcion;
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

        public Respuesta ElimUsuario(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Usuarios
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        exists.Compare = exists.Clave;
                        exists.AudActivo = 0;
                        var perfilUsu = (from p in context.UsuarioPerfils
                                         where p.IdUsuario == Id
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
