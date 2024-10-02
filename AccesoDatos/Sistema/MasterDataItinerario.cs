using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public List<MasterDataItinerario> ObtMasterDataItinerario(string codNave, string codViaje, int idPuerto, string movimiento, string idItin)
        {
            List<MasterDataItinerario> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = context.Database.SqlQuery<MasterDataItinerario>("SISTEMA.PROC_OBTENER_CONTENEDORES @codNave,@codViaje,@idPuerto,@Movimiento, @iditin",
                         new SqlParameter("codNave", codNave),
                       new SqlParameter("codViaje", codViaje),
                      new SqlParameter("idPuerto", idPuerto),
                       new SqlParameter("Movimiento", movimiento),
                      new SqlParameter("iditin", idItin)
                      ).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }
    }
}
