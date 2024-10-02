using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Data;
using com.msc.infraestructure.entities.reportes;

namespace com.msc.infraestructure.biz
{
    public partial class ReportesBL
    {
        private Repository _repositorio;

        public ReportesBL()
        {
            _repositorio = new Repository();
        }

        public List<EnergiaConten> ObtEnergiaConten(pEnergiaConten param)
        {
            return _repositorio.ObtEnergiaConten(param);
        }

    }
}
