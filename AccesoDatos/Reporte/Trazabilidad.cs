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
        public List<Trazabilidad> ObtTrazabilidad()
        {
            List<Trazabilidad> lstReporte;
            try
            {
                using (var context = new CompanyContext())
                {
                    lstReporte = context.Database.SqlQuery<Trazabilidad>("[REPORTE].[PROC_TRAZABILIDAD]").ToList();
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
