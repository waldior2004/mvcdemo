using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public KeyValuePair<int, int> ObtCondExcepcional(Range<DateTime> rango, string nave, string viaje, int terminal)
        {
            int horas = 0;
            int idcondicion = 0;
            try
            {
                using (var context = new CompanyContext())
                {
                    //Obtengo distibucion dias terminal
                    var objTransporte = (from p in ObtTablaGrupo("008")
                                         where p.Codigo == "001"
                                         select p).FirstOrDefault();


                    //Obtengo Tipo Nave
                    var objTipoNave = (from p in ObtTablaGrupo("006")
                                       where p.Codigo == "003"
                                       select p).FirstOrDefault();

                    var existsNav = (from p in context.CondEspeClis
                                     where p.IdReferencia == nave && p.AudActivo == 1 && p.IdTipoCond == objTipoNave.Id && p.UsuAprobacion != "" && p.FechaAprobacion != null
                                     select p).ToList();

                    if (existsNav.Count() > 0)
                    {
                        foreach (var item in existsNav)
                        {
                            var lstDetalles = (from p in context.CondEspeCliDetalles
                                               where p.IdCondEspeCli == item.Id && p.AudActivo == 1 && p.IdTerminal == terminal && p.IdTerminal == p.IdRetiraPor
                                               select p).ToList();

                            item.CondEspeCliDetalles = lstDetalles;
                        }

                        var subexistsNav = (from p in existsNav
                                            where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                            select p).ToList();

                        foreach (var item in subexistsNav)
                        {
                            var detaObj = item.CondEspeCliDetalles.FirstOrDefault();
                            if (detaObj != null)
                            {
                                var horasCli = (from p in context.CondEspeCliDias
                                                where p.IdCondEspeCliDetalle == detaObj.Id && p.IdTransporte == objTransporte.Id && p.AudActivo == 1
                                                select p).ToList().Sum(q => q.DiaF - q.DiaI + 1) * 24;
                                if (horasCli > horas)
                                {
                                    horas = horasCli;
                                    idcondicion = detaObj.IdCondEspeCli;
                                }
                            }
                        }
                    }

                    //Obtengo Tipo Condicion VIAJE
                    var objTipoVia = (from p in ObtTablaGrupo("006")
                                      where p.Codigo == "004"
                                      select p).FirstOrDefault();

                    var existsVia = (from p in context.CondEspeClis
                                     where p.IdReferencia == viaje && p.AudActivo == 1 && p.IdTipoCond == objTipoVia.Id && p.UsuAprobacion != "" && p.FechaAprobacion != null
                                     select p).ToList();

                    if (existsVia.Count() > 0)
                    {
                        foreach (var item in existsVia)
                        {
                            var lstDetalles = (from p in context.CondEspeCliDetalles
                                               where p.IdCondEspeCli == item.Id && p.AudActivo == 1 && p.IdTerminal == terminal && p.IdTerminal == p.IdRetiraPor
                                               select p).ToList();

                            item.CondEspeCliDetalles = lstDetalles;
                        }

                        var subexistsVia = (from p in existsVia
                                            where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                            select p).ToList();

                        foreach (var item in subexistsVia)
                        {
                            var detaObj = item.CondEspeCliDetalles.FirstOrDefault();
                            if (detaObj != null)
                            {
                                var horasCli = (from p in context.CondEspeCliDias
                                                where p.IdCondEspeCliDetalle == detaObj.Id && p.IdTransporte == objTransporte.Id && p.AudActivo == 1
                                                select p).ToList().Sum(q => q.DiaF - q.DiaI + 1) * 24;
                                if (horasCli > horas)
                                {
                                    horas = horasCli;
                                    idcondicion = detaObj.IdCondEspeCli;
                                }
                            }
                        }
                    }
                }
                return new KeyValuePair<int, int>(idcondicion, horas);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return new KeyValuePair<int, int>(0, 0); ;
            }
        }


        public KeyValuePair<int, int> ObtCondEspeCli(Range<DateTime> rango, int cliente, int tipocarga, int terminal, int idCargaLiqD)
        {
            var htot = Convert.ToInt16((rango.Max - rango.Min).TotalHours);

            List<CargaLiquiDistribucion> distribucion = new List<CargaLiquiDistribucion>();
            List<CargaLiquiDistribucion> distribucionCarga = new List<CargaLiquiDistribucion>();
            
            int horas = 0;
            int idcondicion = 0;
            string _cliente = cliente.ToString();
            string _tipocarga = tipocarga.ToString();
            try
            {
                using (var context = new CompanyContext())
                {
                    //Obtengo distibucion dias terminal
                    var objTransporte = (from p in ObtTablaGrupo("008")
                                         where p.Codigo == "001"
                                         select p).FirstOrDefault();


                    //Obtengo Tipo Condicion Cliente
                    var objTipoCli = (from p in ObtTablaGrupo("006")
                                      where p.Codigo == "001"
                                      select p).FirstOrDefault();

                    var existsCli = (from p in context.CondEspeClis
                                     where p.IdReferencia == _cliente && p.AudActivo == 1 && p.IdTipoCond == objTipoCli.Id && p.UsuAprobacion != "" && p.FechaAprobacion != null
                                     select p).ToList();

                    if (existsCli.Count() > 0)
                    {
                        foreach (var item in existsCli)
                        {
                            var lstDetalles = (from p in context.CondEspeCliDetalles
                                               where p.IdCondEspeCli == item.Id && p.AudActivo == 1 && p.IdTerminal == terminal && p.IdTerminal == p.IdRetiraPor
                                               select p).ToList();

                            item.CondEspeCliDetalles = lstDetalles;
                        }

                        var subexistsCli = (from p in existsCli
                                            where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                            select p).ToList();

                        foreach (var item in subexistsCli)
                        {
                            var detaObj = item.CondEspeCliDetalles.FirstOrDefault();
                            if (detaObj != null)
                            {
                                //horaCli son solamente de la terminal

                                var detalleDistribucion = (from p in context.CondEspeCliDias
                                                           where p.IdCondEspeCliDetalle == detaObj.Id && p.AudActivo == 1
                                                           select p).ToList();




                                //var horasCli = (from p in detalleDistribucion where p.IdTransporte == objTransporte.Id select p).ToList().Sum(q => q.DiaF - q.DiaI + 1) * 24;

                                /*
                                var horasCli = (from p in context.CondEspeCliDias
                                                where p.IdCondEspeCliDetalle == detaObj.Id && p.IdTransporte == objTransporte.Id && p.AudActivo == 1
                                                select p).ToList().Sum(q => q.DiaF - q.DiaI + 1) * 24;
                                                */


                                //REPLICAR
                                int remanente = htot;
                                int horasA = 0;
                                foreach (CondEspeCliDia cond in detalleDistribucion)
                                {

                                    if (remanente > 0)
                                        for (int dia = cond.DiaI; dia <= cond.DiaF; dia++)
                                        {
                                            if (remanente > 0)
                                            {
                                               

                                                    if (24*dia <= htot)
                                                    {
                                                        horasA = 24;
                                                    }
                                                    else
                                                    {
                                                        horasA = remanente;
                                                    }

                                                    remanente = remanente - horasA;

                                                    distribucion.Add(new CargaLiquiDistribucion()
                                                    {
                                                        Dia = dia,
                                                        Horas = horasA,
                                                        IdCargaLiquiD = idCargaLiqD,
                                                        IdTransporte = cond.IdTransporte
                                                    });
                                                }
                                            
                                        }

                                }

                                var horasCli = (from p in distribucion where p.IdTransporte == objTransporte.Id select p).ToList().Sum(n => n.Horas);
                                if (horasCli > horas)
                                {
                                    horas = horasCli;
                                    idcondicion = detaObj.IdCondEspeCli;
                                }
                            }
                        }
                    }

                    //Obtengo Tipo Condicion Tipo Carga
                    var objTipoCar = (from p in ObtTablaGrupo("006")
                                      where p.Codigo == "002"
                                      select p).FirstOrDefault();

                    var existsCar = (from p in context.CondEspeClis
                                     where p.IdReferencia == _tipocarga && p.AudActivo == 1 && p.IdTipoCond == objTipoCar.Id && p.UsuAprobacion != "" && p.FechaAprobacion != null
                                     select p).ToList();

                    if (existsCar.Count() > 0)
                    {
                       
                        foreach (var item in existsCar)
                        {
                            var lstDetalles = (from p in context.CondEspeCliDetalles
                                               where p.IdCondEspeCli == item.Id && p.AudActivo == 1 && p.IdTerminal == terminal && p.IdTerminal == p.IdRetiraPor
                                               select p).ToList();

                            item.CondEspeCliDetalles = lstDetalles;
                        }

                        var subexistsCar = (from p in existsCar
                                            where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                            select p).ToList();

                        foreach (var item in subexistsCar)
                        {
                            var detaObj = item.CondEspeCliDetalles.FirstOrDefault();
                            if (detaObj != null)
                            {
                                //horaCli son solamente de la terminal

                                var detalleDistribucion = (from p in context.CondEspeCliDias
                                                           where p.IdCondEspeCliDetalle == detaObj.Id && p.AudActivo == 1
                                                           select p).ToList();


                                /*var horasCli = (from p in context.CondEspeCliDias
                                                where p.IdCondEspeCliDetalle == detaObj.Id && p.IdTransporte == objTransporte.Id && p.AudActivo == 1
                                                select p).ToList().Sum(q => q.DiaF - q.DiaI + 1) * 24;*/
                                //REPLICAR
                                int remanente = htot;
                                int horasA = 0;
                                foreach (CondEspeCliDia cond in detalleDistribucion)
                                {

                                    if (remanente > 0)
                                        for (int dia = cond.DiaI; dia <= cond.DiaF; dia++)
                                        {
                                            if (remanente > 0)
                                            {


                                                if (24 * dia <= htot)
                                                {
                                                    horasA = 24;
                                                }
                                                else
                                                {
                                                    horasA = remanente;
                                                }

                                                remanente = remanente - horasA;

                                                distribucionCarga.Add(new CargaLiquiDistribucion()
                                                {
                                                    Dia = dia,
                                                    Horas = horasA,
                                                    IdCargaLiquiD = idCargaLiqD,
                                                    IdTransporte = cond.IdTransporte
                                                });
                                            }

                                        }

                                }

                                var horasCli = (from p in distribucionCarga where p.IdTransporte == objTransporte.Id select p).ToList().Sum(n => n.Horas);


                                if (horasCli > horas)
                                {
                                    distribucion.Clear();
                                    distribucion.AddRange(distribucionCarga);
                                    horas = horasCli;
                                    idcondicion = detaObj.IdCondEspeCli;
                                }
                            }
                        }
                    }
                }

                //ENRIQUE
                //REGISTRO DE DISTRIBUCION
                EditCargaLiquiDistribucionAll(distribucion);


                //

                return new KeyValuePair<int, int>(idcondicion, horas);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return new KeyValuePair<int, int>(0, 0); ;
            }
        }

        public List<CondEspeCli> ObtCondEspeCli()
        {
            List<CondEspeCli> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.CondEspeClis.Include("TipoCond")
                           where p.AudActivo == 1 /*&& !(p.FechaFin < DateTime.Now)*/
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
        public List<CondEspeCli> ObtCondEspeCliHistorico()
        {
            List<CondEspeCli> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.CondEspeClis.Include("TipoCond")
                           where p.AudActivo == 1 && p.FechaFin < DateTime.Now
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
        public CondEspeCli ObtCondEspeCli(int Id)
        {
            CondEspeCli obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.CondEspeClis.Include("TipoCond")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                    if (obj != null)
                    {
                        var lstDocumentos = (from p in context.CondEspeCliDocs.Include("Documento")
                                             where p.IdCondEspeCli == Id && p.AudActivo == 1
                                             select p).ToList();

                        obj.CondEspeCliDocs = lstDocumentos;

                        var lstDetalles = (from p in context.CondEspeCliDetalles.Include("Terminal").Include("RetiraPor").Include("CondEspeCli")
                                           where p.IdCondEspeCli == Id && p.AudActivo == 1
                                           select p).ToList();

                        obj.CondEspeCliDetalles = lstDetalles;
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
        public Respuesta EditCondEspeCli(CondEspeCli obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        Range<DateTime> rango = new Range<DateTime>(obj.FechaInicio, obj.FechaFin);
                        var exists = (from p in context.CondEspeClis
                                      where p.IdTipoCond == obj.IdTipoCond && p.IdReferencia == obj.IdReferencia
                                      && p.AudActivo == 1
                                      select p).ToList();

                        if (exists != null)
                        {
                            var subexists = (from p in exists
                                             where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                             select p).FirstOrDefault();
                            if (subexists != null)
                                objResp = MessagesApp.BackAppMessage(MessageCode.RangeDatesCondEspeCli);
                            else
                            {
                                obj.TipoCond = null;
                                obj.AudActivo = 1;
                                context.CondEspeClis.Add(obj);
                                context.SaveChanges();
                                objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                                context.Entry(obj).GetDatabaseValues();
                                objResp.Metodo = obj.Id.ToString();
                            }
                        }
                        else
                        {
                            obj.TipoCond = null;
                            obj.AudActivo = 1;
                            context.CondEspeClis.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();
                        }
                    }
                    else
                    {
                        var exists = (from p in context.CondEspeClis
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {

                            Range<DateTime> rango = new Range<DateTime>(obj.FechaInicio, obj.FechaFin);

                            var exists2 = (from p in context.CondEspeClis
                                           where p.IdTipoCond == obj.IdTipoCond && p.IdReferencia == obj.IdReferencia
                                          && p.Id != obj.Id && p.AudActivo == 1
                                           select p).ToList();

                            if (exists2 != null)
                            {
                                var subexists2 = (from p in exists2
                                                  where rango.IsOverlapped(new Range<DateTime>(p.FechaInicio, p.FechaFin))
                                                  select p).FirstOrDefault();

                                if (subexists2 != null)
                                    objResp = MessagesApp.BackAppMessage(MessageCode.RangeDatesCondEspeCli);
                                else
                                {
                                    var maxDeta = 0;
                                    var lstdeta = (from p in context.CondEspeCliDetalles
                                                   where p.IdCondEspeCli == obj.Id && p.AudActivo == 1
                                                   select p).ToList();

                                    if (lstdeta != null)
                                        maxDeta = lstdeta.Max(p => p.Dias);

                                    if (maxDeta > obj.DiasLibres)
                                    {
                                        objResp = MessagesApp.BackAppMessage(MessageCode.NumberDaysExceedCondition);
                                    }
                                    else
                                    {
                                        exists.IdTipoCond = obj.IdTipoCond;
                                        exists.IdReferencia = obj.IdReferencia;
                                        exists.DescReferencia = obj.DescReferencia;
                                        exists.FechaInicio = obj.FechaInicio;
                                        exists.FechaFin = obj.FechaFin;
                                        exists.FechaAprobacion = obj.FechaAprobacion;
                                        exists.Descripcion = obj.Descripcion;
                                        exists.UsuAprobacion = obj.UsuAprobacion;
                                        exists.Nombre = obj.Nombre;
                                        exists.DiasLibres = obj.DiasLibres;
                                        exists.AudUpdate = DateTime.Now;
                                        objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                exists.IdTipoCond = obj.IdTipoCond;
                                exists.IdReferencia = obj.IdReferencia;
                                exists.DescReferencia = obj.DescReferencia;
                                exists.FechaInicio = obj.FechaInicio;
                                exists.FechaFin = obj.FechaFin;
                                exists.FechaAprobacion = obj.FechaAprobacion;
                                exists.Descripcion = obj.Descripcion;
                                exists.UsuAprobacion = obj.UsuAprobacion;
                                exists.Nombre = obj.Nombre;
                                exists.DiasLibres = obj.DiasLibres;
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
        public Respuesta ElimCondEspeCli(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.CondEspeClis
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        var Docs = (from p in context.Documentos
                                    join q in context.CondEspeCliDocs on p.Id equals q.IdDocumento
                                    where q.IdCondEspeCli == Id
                                    select p);
                        foreach (var item in Docs)
                        {
                            item.AudActivo = 0;
                        }

                        var condespeDocs = (from p in context.CondEspeCliDocs
                                            where p.IdCondEspeCli == Id
                                            select p);
                        foreach (var item in condespeDocs)
                        {
                            item.AudActivo = 0;
                        }


                        var condespeDias = (from p in context.CondEspeCliDias
                                            join q in context.CondEspeCliDetalles on p.IdCondEspeCliDetalle equals q.Id
                                            where q.IdCondEspeCli == Id
                                            select p);

                        foreach (var item in condespeDias)
                        {
                            item.AudActivo = 0;
                        }

                        var condespeDet = (from p in context.CondEspeCliDetalles
                                           where p.IdCondEspeCli == Id
                                           select p);
                        foreach (var item in condespeDet)
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
