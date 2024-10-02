using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        public KeyValuePair<int,decimal> ObtTarifaCE(Range<DateTime> rango, int terminal)
        {
            decimal tarifa = 0;
            int idtarifa = 0;
            try
            {
                using (var context = new CompanyContext())
                {
                    //Obtengo estado aprobado
                    var objEstado = (from p in ObtTablaGrupo("004")
                                     where p.Codigo == "002"
                                     select p).FirstOrDefault();

                    //Obtengo periodo dia
                    var objPeriodo = (from p in ObtTablaGrupo("002")
                                     where p.Codigo == "001"
                                     select p).FirstOrDefault();

                    var exists = (from p in context.TarifaCEs
                                  where p.IdTerminal == terminal && p.AudActivo == 1 && p.IdEstado == objEstado.Id
                                  select p).ToList();

                    var subexists = (from p in exists
                                     where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                     select p).ToList();

                    foreach (var item in subexists)
                    {
                        if (item.IdPerTar == objPeriodo.Id)
                        {
                            item.Importe = item.Importe / 24;
                        }

                        if (item.Importe > tarifa)
                        {
                            tarifa = item.Importe;
                            idtarifa = item.Id;
                        }
                    }

                }
                return new KeyValuePair<int, decimal>(idtarifa,tarifa);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return new KeyValuePair<int, decimal>(0, 0);
            }
        }

        public List<TarifaCE> ObtTarifaCE()
        {
            List<TarifaCE> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    //Obtengo estado aprobado
                    var objEstado = (from p in ObtTablaGrupo("004")
                                     where p.Codigo == "002"
                                     select p).FirstOrDefault();

                    lst = (from p in context.TarifaCEs.Include("Terminal").Include("PerTarifa").Include("Moneda").Include("Estado")
                           where p.AudActivo == 1 /*&& !(objEstado.Id == p.IdEstado && p.FechaFin < DateTime.Now)*/
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
        public List<TarifaCE> ObtTarifaHistoricoCE()
        {
            List<TarifaCE> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    //Obtengo estado aprobado
                    var objEstado = (from p in ObtTablaGrupo("004")
                                     where p.Codigo == "002"
                                     select p).FirstOrDefault();

                    lst = (from p in context.TarifaCEs.Include("Terminal").Include("PerTarifa").Include("Moneda").Include("Estado")
                           where p.AudActivo == 1 && objEstado.Id == p.IdEstado && p.FechaFin < DateTime.Now
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
        public TarifaCE ObtTarifaCE(int Id)
        {
            TarifaCE obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.TarifaCEs.Include("Terminal").Include("PerTarifa").Include("Moneda").Include("Estado")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                    if (obj != null)
                    {
                        var lstDocumentos = (from p in context.TarifaCEDocs.Include("Documento")
                                           where p.IdTarifaCE == Id && p.AudActivo == 1
                                           select p).ToList();

                        obj.TarifaCEDocs = lstDocumentos;
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
        public Respuesta EditTarifaCE(TarifaCE obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        Range<DateTime> rango = new Range<DateTime>(obj.FechaInicio, obj.FechaFin);
                        var exists = (from p in context.TarifaCEs
                                      where p.IdTerminal == obj.IdTerminal
                                      && p.IdPerTar == obj.IdPerTar
                                      && p.AudActivo == 1
                                      select p).ToList();

                        if (exists != null)
                        {
                            var subexists = (from p in exists
                                             where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                             select p).FirstOrDefault();
                            if (subexists != null)
                                objResp = MessagesApp.BackAppMessage(MessageCode.RangeDatesTarifaCE);
                            else
                            {
                                obj.Terminal = null;
                                obj.PerTarifa = null;
                                obj.Moneda = null;
                                obj.Estado = null;
                                obj.AudActivo = 1;
                                context.TarifaCEs.Add(obj);
                                context.SaveChanges();
                                objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                                context.Entry(obj).GetDatabaseValues();
                                objResp.Metodo = obj.Id.ToString();
                            }
                        }
                        else
                        {
                            obj.Terminal = null;
                            obj.PerTarifa = null;
                            obj.Moneda = null;
                            obj.Estado = null;
                            obj.AudActivo = 1;
                            context.TarifaCEs.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();
                        }
                    }
                    else
                    {
                        var exists = (from p in context.TarifaCEs
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {

                            Range<DateTime> rango = new Range<DateTime>(obj.FechaInicio, obj.FechaFin);

                            var exists2 = (from p in context.TarifaCEs
                                          where p.IdTerminal == obj.IdTerminal
                                          && p.IdPerTar == obj.IdPerTar
                                          && p.Id != obj.Id && p.AudActivo == 1
                                          select p).ToList();

                            if (exists2 != null)
                            {
                                var subexists2 = (from p in exists2
                                                 where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                                 select p).FirstOrDefault();

                                if (subexists2 != null)
                                    objResp = MessagesApp.BackAppMessage(MessageCode.RangeDatesTarifaCE);
                                else
                                {
                                    exists.IdTerminal = obj.IdTerminal;
                                    exists.IdPerTar = obj.IdPerTar;
                                    exists.IdMoneda = obj.IdMoneda;
                                    exists.Importe = obj.Importe;
                                    exists.FechaInicio = obj.FechaInicio;
                                    exists.FechaFin = obj.FechaFin;
                                    exists.Comentarios = obj.Comentarios;
                                    exists.IdEstado = obj.IdEstado;
                                    exists.AudUpdate = DateTime.Now;
                                    objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                                    context.SaveChanges();
                                }
                            }
                            else
                            {
                                exists.IdTerminal = obj.IdTerminal;
                                exists.IdPerTar = obj.IdPerTar;
                                exists.IdMoneda = obj.IdMoneda;
                                exists.Importe = obj.Importe;
                                exists.FechaInicio = obj.FechaInicio;
                                exists.FechaFin = obj.FechaFin;
                                exists.Comentarios = obj.Comentarios;
                                exists.IdEstado = obj.IdEstado;
                                exists.AudUpdate = DateTime.Now;
                                objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                                context.SaveChanges();
                            }
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
        public Respuesta ElimTarifaCE(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.TarifaCEs
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        var Docs = (from p in context.Documentos
                                    join q in context.TarifaCEDocs on p.Id equals q.IdDocumento
                                          where q.IdTarifaCE == Id
                                          select p);
                        foreach (var item in Docs)
                        {
                            item.AudActivo = 0;
                        }

                        var tarifaDocs = (from p in context.TarifaCEDocs
                                      where p.IdTarifaCE == Id
                                      select p);
                        foreach (var item in tarifaDocs)
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
