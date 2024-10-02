using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public List<CondEspeCliDia> ObtCondEspeCliDia(int Id)
        {
            List<CondEspeCliDia> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.CondEspeCliDias.Include("Transporte")
                           where p.IdCondEspeCliDetalle == Id && p.AudActivo == 1
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

        public Respuesta EditCondEspeCliDia(CondEspeCliDia obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {

                    var exists = (from p in context.CondEspeCliDias
                                    where p.IdCondEspeCliDetalle == obj.IdCondEspeCliDetalle && p.AudActivo == 1
                                    select p).ToList();

                    if (exists != null)
                    {
                        Range<short> rango = new Range<short>(obj.DiaI, obj.DiaF);
                        var subexists = (from p in exists
                                         where rango.IsOverlapped(new Range<short>(p.DiaI, p.DiaF))
                                         select p).FirstOrDefault();
                        if (subexists != null)
                            objResp = MessagesApp.BackAppMessage(MessageCode.RangeDiasDetalleCondEsp);
                        else
                        {
                            obj.CondEspeCliDetalle = null;
                            obj.Transporte = null;
                            obj.AudActivo = 1;
                            context.CondEspeCliDias.Add(obj);
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.SaveChanges();
                        }
                    }
                    else {
                        obj.CondEspeCliDetalle = null;
                        obj.Transporte = null;
                        obj.AudActivo = 1;
                        context.CondEspeCliDias.Add(obj);
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

        public Respuesta ElimCondEspeCliDia(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.CondEspeCliDias
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
