using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        public Documento ObtOrdenCompraDocNombre(int Id)
        {
            Documento name;
            try
            {
                using (var context = new CompanyContext())
                {
                    name = (from p in context.OrdenCompraDocs
                           join q in context.Documentos on p.IdDocumento equals q.Id
                           where p.Id == Id && p.AudActivo == 1
                           select q).FirstOrDefault();
                }
                return name;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditOrdenCompraDoc(OrdenCompraDoc obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    obj.AudActivo = 1;
                    context.OrdenCompraDocs.Add(obj);
                    objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                    context.SaveChanges();
                    context.Entry(obj).GetDatabaseValues();
                    objResp.Metodo = obj.Id.ToString();
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return MyException.OnException(ex);
            }
        }

        public Respuesta ElimOrdenCompraDoc(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.OrdenCompraDocs
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        var existsDoc = (from p in context.Documentos
                                         where p.Id == exists.IdDocumento
                                         select p).FirstOrDefault();
                        if (existsDoc != null)
                        {
                            existsDoc.AudActivo = 0;
                        }
                        exists.AudActivo = 0;
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.DeleteOK);
                        objResp.Message = existsDoc.Nombre;
                        objResp.Metodo = exists.IdOrdenCompra.ToString();
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
