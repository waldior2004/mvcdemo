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

        public Respuesta EditCargaLiquiDistribucionAll(List<CargaLiquiDistribucion> obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {


                    context.Configuration.AutoDetectChangesEnabled = false;

                    foreach(CargaLiquiDistribucion item in obj)
                    {
                        context.CargaLiquiDistribuciones.Add(item);
                    }

                    objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                    context.SaveChanges();


                    context.Configuration.AutoDetectChangesEnabled = true;
                    //   LogError.PostInfoMessage("Id interno: " + obj.Id);

                    /*if (obj.Id == 0)
                    {
                        //obj.AudActivo = 1;
                        //obj.FecRegistro = DateTime.Now;



                        context.CargaLiquiDistribuciones.Add(obj);
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.SaveChanges();
                        context.Entry(obj).GetDatabaseValues();
                        objResp.Metodo = obj.Id.ToString();
                    }
                    else
                    {*/
                    /*
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
                    */
                    //}
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return MyException.OnException(ex);
            }
        }


        public Respuesta EditCargaLiquiDistribucion(CargaLiquiDistribucion obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {

                    LogError.PostInfoMessage("Id interno: " + obj.Id);

                    if (obj.Id == 0)
                    {
                        //obj.AudActivo = 1;
                        //obj.FecRegistro = DateTime.Now;

                      

                        context.CargaLiquiDistribuciones.Add(obj);
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.SaveChanges();
                        context.Entry(obj).GetDatabaseValues();
                        objResp.Metodo = obj.Id.ToString();
                    }
                    else
                    {
                        /*
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
                        */
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
        /*
        public Respuesta ElimCargaLiquiDistribucion(int Id)
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

    */
    }
}
