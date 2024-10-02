using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        public CargaLiqui ObtCargaLiqui(int Id)
        {
            CargaLiqui obj;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.CargaLiquis.Include("Contenedor").Include("Booking").Include("Cliente").Include("TipoCarga").Include("Nave").Include("Viaje").Include("Tarifa").Include("CondEspeCli")
                           where p.IdCargaLiquiD == Id && p.AudActivo == 1
                            select p).FirstOrDefault();

                    var objTipoTar = context.Tablas.Where(p => p.Id == obj.Tarifa.IdPerTar).FirstOrDefault();
                    obj.Tarifa.PerTarifa = objTipoTar;

                    var objMonTar = context.Tablas.Where(p => p.Id == obj.Tarifa.IdMoneda).FirstOrDefault();
                    obj.Tarifa.Moneda = objMonTar;

                    var objTipoCond = context.Tablas.Where(p => p.Id == obj.CondEspeCli.IdTipoCond).FirstOrDefault();
                    obj.CondEspeCli.TipoCond = objTipoCond;
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditCargaLiqui(CargaLiqui obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    obj.AudActivo = 1;
                    obj.CargaLiquiD = null;
                    obj.Contenedor = null;
                    obj.Booking = null;
                    obj.TipoCarga = null;
                    obj.Nave = null;
                    obj.Viaje = null;
                    obj.Tarifa = null;
                    obj.CondEspeCli = null;
                    context.CargaLiquis.Add(obj);
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
        
    }
}
