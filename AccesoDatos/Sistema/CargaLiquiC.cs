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
        public string GeneraCodigoProvision()
        {
            var correlativo = "";
            try
            {
                using (var context = new CompanyContext())
                {
                    correlativo = context.Database.SqlQuery<string>("SISTEMA.PROC_GENERAPROVISION").SingleOrDefault();
                }
                return correlativo;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }
        public Documento ObtCargaLiquiCDocNombre(int Id)
        {
            Documento name;
            try
            {
                using (var context = new CompanyContext())
                {
                    name = (from p in context.Documentos
                            where p.Id == Id && p.AudActivo == 1
                            select p).FirstOrDefault();
                }
                return name;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<CargaLiquiC> ObtCargaLiquiC()
        {
            List<CargaLiquiC> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.CargaLiquiCs.Include("Documento").Include("Terminal")
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

        public List<CargaLiquiC> ObtEnviadosC()
        {
            List<CargaLiquiC> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.CargaLiquiCs.Include("Documento").Include("Terminal").Include("Nave")
                           where p.AudActivo == 1 && (p.Estado == "Enviado" || p.Estado == "Aprobado" || p.Estado == "Rechazado")
                           select p).DistinctBy(p=>p.Id).ToList();

                    foreach (var item in lst)
                    {
                        var objViaje = (from p in context.Viajes
                                        where p.Id == item.IdViaje && p.IdNave == item.IdNave
                                        select p).FirstOrDefault();
                        item.Viaje = objViaje;
                    }

                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public CargaLiquiC ObtCargaLiquiC(int Id)
        {
            CargaLiquiC obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.CargaLiquiCs.Include("Documento").Include("Terminal").Include("Nave").Include("Viaje").Include("Puerto")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    if (obj != null)
                    {
                        var lstDetalles = (from p in context.CargaLiquiDs
                                             where p.IdCargaLiquiC == Id && p.AudActivo == 1
                                             select p).ToList();

                        obj.CargaLiquiDs = lstDetalles;
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

        public Respuesta EditStatusCargaLiquiC(CargaLiquiC obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var objGet = (from p in context.CargaLiquiCs
                                    where p.Id == obj.Id && p.AudActivo == 1
                                    select p).FirstOrDefault();

                    objGet.Procesados = obj.Procesados;
                    objGet.Errados = obj.Errados;
                    objGet.Correctos = obj.Correctos;
                    objGet.Estado = obj.Estado;
                    objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
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

        public Respuesta EditCargaLiquiC(CargaLiquiC obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {

                    LogError.PostInfoMessage("Id interno: " + obj.Id);

                    if (obj.Id == 0)
                    {
                        obj.AudActivo = 1;
                        obj.FecRegistro = DateTime.Now;
                        obj.Documento = null;
                        obj.Terminal = null;
                        obj.Documento2 = null;
                        obj.Nave = null;
                        obj.Viaje = null;
                        obj.Puerto = null;
                        context.CargaLiquiCs.Add(obj);
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.SaveChanges();
                        context.Entry(obj).GetDatabaseValues();
                        objResp.Metodo = obj.Id.ToString();
                    }
                    else
                    {
                        var objGet = (from p in context.CargaLiquiCs
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        objGet.IdDocumento2 = obj.IdDocumento2;
                        objGet.Procesados = obj.Procesados;
                        objGet.Errados = obj.Errados;
                        objGet.Correctos = obj.Correctos;
                        objGet.Estado = obj.Estado;
                        objGet.Total = obj.Total;
                        objGet.Comentario = obj.Comentario;
                        objGet.Provision = obj.Provision;
                        objGet.FecValida = obj.FecValida;
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.SaveChanges();
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

        public Respuesta ElimCargaLiquiC(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.CargaLiquiCs
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {

                        var liquis = (from p in context.CargaLiquis
                                      join q in context.CargaLiquiDs on p.IdCargaLiquiD equals q.Id
                                      where q.IdCargaLiquiC == Id
                                      select p);
                        foreach (var item in liquis)
                        {
                            item.AudActivo = 0;
                        }

                        var detalles = (from p in context.CargaLiquiDs
                                         where p.IdCargaLiquiC == Id
                                         select p);
                        foreach (var item in detalles)
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
