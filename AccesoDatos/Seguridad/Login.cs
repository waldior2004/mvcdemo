using System;
using System.Linq;
using System.Security.Cryptography;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.infraestructure.entities.mvc;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public Respuesta ChangePassword(string Usuario, string Clave)
        {
            Respuesta objResp = null;
            Externo obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Externos
                           where p.Usuario == Usuario && p.AudActivo == 1
                           select p).FirstOrDefault();
                    obj.Clave = Clave;
                    context.SaveChanges();
                    objResp = MessagesApp.BackAppMessage(MessageCode.ChangePasswordOK);
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta AuthenticateExterno(Login log)
        {
            Externo obj = null;
            Respuesta objResp = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Externos
                           where p.Usuario == log.Usuario && p.AudActivo == 1
                           select p).FirstOrDefault();
                    if (obj == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.AuthenticateAccountError);
                    }
                    else
                    {
                            LogError.PostInfoMessage(string.Format("Autenticación Externo {1}({2}) {0}", obj.Clave, log.Usuario, log.Password));

                            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                            if (0 == comparer.Compare(obj.Clave, log.Password))
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.AuthenticateOK);
                                if (obj.EsInicio == 1)
                                {
                                    objResp.Message = "Change";
                                    obj.EsInicio = 0;
                                    context.SaveChanges();
                                }
                            }
                            else
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.AuthenticateBadPassword);
                            }
                    }
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Usuario GetUserAuthenticated(string user)
        {
            try
            {
                Usuario obj = null;
                using (var context = new CompanyContext())
                {
                    
                    obj = (from p in context.Usuarios
                           join q in context.UsuarioPerfils on p.Id equals q.IdUsuario
                           join r in context.Perfils on q.IdPerfil equals r.Id
                           where p.Login == user && p.AudActivo == 1 && q.AudActivo == 1 && r.AudActivo == 1
                           select p).FirstOrDefault();
                    obj.Rol = context.Rols.Where(p => p.Id == obj.IdRol && p.AudActivo == 1).FirstOrDefault();
                    obj.UsuarioPerfils = context.UsuarioPerfils.Include("Perfil").Where(p => p.IdUsuario == obj.Id && p.AudActivo == 1).ToList();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Externo GetUserExternalAuthenticated(string user)
        {
            try
            {
                Externo obj = null;
                using (var context = new CompanyContext())
                {

                    obj = (from p in context.Externos
                           join q in context.ExternoPerfils on p.Id equals q.IdExterno
                           join r in context.Perfils on q.IdPerfil equals r.Id
                           where p.Usuario == user && p.AudActivo == 1 && q.AudActivo == 1 && r.AudActivo == 1
                           select p).FirstOrDefault();
                    obj.Terminal = context.Tablas.Where(p => p.Id == obj.IdTerminal && p.AudActivo == 1).FirstOrDefault();
                    obj.ExternoPerfils = context.ExternoPerfils.Include("Perfil").Where(p => p.IdExterno == obj.Id && p.AudActivo == 1).ToList();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta Authenticate(Login log)
        {
            Usuario obj = null;
            Respuesta objResp = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Usuarios
                           where p.Login == log.Usuario && p.AudActivo == 1
                           select p).FirstOrDefault();
                    if (obj == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.AuthenticateAccountError);
                    }
                    else
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string hash = Encrypt.GetMd5Hash(md5Hash, log.Password);

                            LogError.PostInfoMessage(string.Format("Autenticación {2}({3}) {0} - {1}", hash, obj.Clave, log.Usuario, log.Password));

                            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                            if (0 == comparer.Compare(obj.Clave, hash))
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.AuthenticateOK);
                            }
                            else
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.AuthenticateBadPassword);
                            }
                        }
                    }
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }


    }
}
