using com.msc.infraestructure.biz;
using com.msc.infraestructure.entities.reportes;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.interfaces;
using System;
using System.Collections.Generic;

namespace com.msc.services.implementations
{
    public partial class ReporteService : IReporte
    {
        private ReportesBL _reporteLogic;


        public ReporteService()
        {
            _reporteLogic = new ReportesBL();

        }

        public List<EnergiaConten> ObtEnergiaConten(pEnergiaConten param)
        {
            return _reporteLogic.ObtEnergiaConten(param);
        }

        public List<Trazabilidad> ObtTrazabilidad()
        {
            return _reporteLogic.ObtTrazabilidad();
        }

    }
}
