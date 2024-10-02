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

        public Respuesta EditCargaLiquiD(CargaLiquiD obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        obj.AudActivo = 1;
                        obj.CargaLiquiC = null;
                        context.CargaLiquiDs.Add(obj);
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.SaveChanges();
                    }
                    else
                    {
                        var exists = (from p in context.CargaLiquiDs
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists != null)
                        {
                            exists.InDate = obj.InDate;
                            exists.OutDate = obj.OutDate;
                            exists.Estado = obj.Estado;
                            exists.Total = obj.Total;
                            exists.AudUpdate = DateTime.Now;
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.SaveChanges();
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
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

        public CargaLiquiD ObtCargaLiquiD(CargaLiquiD obj)
        {
            var objResp = new CargaLiquiD();
            try
            {
                using (var context = new CompanyContext())
                {
                    objResp = (from p in context.CargaLiquiDs
                               where obj.Booking == p.Booking && obj.NumContenedores == p.NumContenedores &&
                                  obj.Linea == p.Linea && obj.Shipper == p.Shipper && obj.Commodity == p.Commodity && 
                                  obj.Nave == p.Nave && obj.Viaje == p.Viaje && obj.InDate != p.InDate && obj.OutDate != p.OutDate && p.AudActivo == 1
                               select p).FirstOrDefault();

                    return objResp;
                }
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return null;
            }
        }

    }
}
