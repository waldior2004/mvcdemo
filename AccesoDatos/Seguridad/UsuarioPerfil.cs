using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public Respuesta EditUsuarioPerfil(UsuarioPerfil obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.UsuarioPerfils
                                  where p.IdUsuario == obj.IdUsuario && p.IdPerfil == obj.IdPerfil && p.AudActivo == 1
                                  select p).FirstOrDefault();

                    if (exists == null)
                    {
                        obj.AudActivo = 1;
                        context.UsuarioPerfils.Add(obj);
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.SaveChanges();
                    }
                    else
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
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

        public Respuesta ElimUsuarioPerfil(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.UsuarioPerfils
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
