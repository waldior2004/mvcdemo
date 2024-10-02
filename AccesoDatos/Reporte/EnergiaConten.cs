using com.msc.infraestructure.entities.reportes;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        public List<EnergiaConten> ObtEnergiaConten(pEnergiaConten param)
        {
            List<EnergiaConten> lstReporte;
            try
            {
                using (var context = new CompanyContext())
                {
                    lstReporte = context.Database.SqlQuery<EnergiaConten>("[REPORTE].[PROC_ENERGIACONTEN] @IdNave, @IdViaje, @FecIniApro, @FecFinApro, @FecIniZarpe, @FecFinZarpe, @IdPuerto", 
                        new SqlParameter("IdNave", param.IdNave), 
                        new SqlParameter("IdViaje", param.IdViaje), 
                        new SqlParameter("FecIniApro", param.FecIniApro), 
                        new SqlParameter("FecFinApro", param.FecFinApro), 
                        new SqlParameter("FecIniZarpe", param.FecIniZarpe), 
                        new SqlParameter("FecFinZarpe", param.FecFinZarpe),
                        new SqlParameter("IdPuerto", param.IdPuerto)).ToList();
                }

                return lstReporte;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }
    }
}
