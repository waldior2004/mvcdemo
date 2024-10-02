using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public CondEspeCliDetalle ObtCondEspeCliDetalle(int Id)
        {
            CondEspeCliDetalle obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.CondEspeCliDetalles.Include("Terminal").Include("RetiraPor")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    if (obj != null)
                    {
                        var lstDias = (from p in context.CondEspeCliDias.Include("Transporte")
                                             where p.IdCondEspeCliDetalle == Id && p.AudActivo == 1
                                             select p).ToList();

                        obj.CondEspeCliDias = lstDias;
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

        public Respuesta EditCondEspeCliDetalle(CondEspeCliDetalle obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        var exists = (from p in context.CondEspeCliDetalles
                                      where p.IdTerminal == obj.IdTerminal && p.IdCondEspeCli == obj.IdCondEspeCli && p.IdRetiraPor == obj.IdRetiraPor && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            var sumdet = 0;
                            var dias = (from p in context.CondEspeClis
                                        where p.Id == obj.IdCondEspeCli && p.AudActivo == 1
                                        select p.DiasLibres).FirstOrDefault();

                            var lstdet = (from p in context.CondEspeCliDetalles
                                          where p.IdCondEspeCli == obj.IdCondEspeCli && p.AudActivo == 1
                                          select p).ToList();

                            if (lstdet != null)
                                sumdet = lstdet.Sum(p => p.Dias);

                            if (dias >= obj.Dias)
                            {
                                obj.Terminal = null;
                                obj.RetiraPor = null;
                                obj.AudActivo = 1;
                                context.CondEspeCliDetalles.Add(obj);
                                objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                                context.SaveChanges();
                            }
                            else
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.NumberDaysExceedCondition);
                            }
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.TerminalYaExisteCondicion);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.CondEspeCliDetalles
                                      where p.IdTerminal == obj.IdTerminal && p.IdCondEspeCli == obj.IdCondEspeCli && p.IdRetiraPor == obj.IdRetiraPor && p.AudActivo == 1
                                      && p.Id != obj.Id
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {

                            var objUpd = (from p in context.CondEspeCliDetalles
                                          where p.Id == obj.Id && p.AudActivo == 1
                                          select p).FirstOrDefault();

                            if (objUpd == null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                            }
                            else
                            {
                                var sumdet = 0;
                                var dias = (from p in context.CondEspeClis
                                            where p.Id == obj.IdCondEspeCli && p.AudActivo == 1
                                            select p.DiasLibres).FirstOrDefault();

                                var lstdet = (from p in context.CondEspeCliDetalles
                                              where p.IdCondEspeCli == obj.IdCondEspeCli && p.AudActivo == 1 && p.Id != obj.Id
                                              select p).ToList();

                                if (lstdet != null)
                                    sumdet = lstdet.Sum(p=>p.Dias);

                                if (dias >= obj.Dias)
                                {
                                    var sumhij = 0;
                                    var hijos = (from p in context.CondEspeCliDias
                                                 where p.IdCondEspeCliDetalle == obj.Id && p.AudActivo == 1
                                                 select p).ToList();
                                    if (hijos != null)
                                        if(hijos.Count > 0)
                                            sumhij = hijos.Max(p=>p.DiaF);

                                    if (obj.Dias < sumhij)
                                        objResp = MessagesApp.BackAppMessage(MessageCode.NumberDaysAsignacionTrans);
                                    else
                                    {
                                        obj.Terminal = null;
                                        obj.RetiraPor = null;
                                        objUpd.IdRetiraPor = obj.IdRetiraPor;
                                        objUpd.IdTerminal = obj.IdTerminal;
                                        objUpd.Dias = obj.Dias;
                                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                                        context.SaveChanges();
                                    }
                                }
                                else
                                {
                                    objResp = MessagesApp.BackAppMessage(MessageCode.NumberDaysExceedCondition);
                                }
                            }
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.TerminalYaExisteCondicion);
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

        public Respuesta ElimCondEspeCliDetalle(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.CondEspeCliDetalles
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        var condespeDias = (from p in context.CondEspeCliDias
                                            where p.Id == Id
                                            select p);

                        foreach (var item in condespeDias)
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
