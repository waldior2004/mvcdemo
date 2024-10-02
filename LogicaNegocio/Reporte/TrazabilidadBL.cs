using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Data;
using com.msc.infraestructure.entities.reportes;

namespace com.msc.infraestructure.biz
{
    public partial class ReportesBL
    {
        public List<Trazabilidad> ObtTrazabilidad()
        {
            return _repositorio.ObtTrazabilidad();
        }
    }
}
