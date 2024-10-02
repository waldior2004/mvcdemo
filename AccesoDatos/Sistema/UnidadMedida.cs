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
        public List<UnidadMedida> ObtUnidadMedida()
        {
            List<UnidadMedida> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.UnidadesMedidas
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
    }
}
