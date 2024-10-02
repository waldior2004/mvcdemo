using com.msc.infraestructure.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static CondEspeCliDiaDTO GetCondEspeCliDiaDTO(this CondEspeCliDia source)
        {
            var objR = source.CreateMap<CondEspeCliDia, CondEspeCliDiaDTO>();

            if (source.Transporte != null)
                objR.Transporte = source.Transporte.CreateMap<Tabla, TablaDTO>();
            else
                objR.Transporte = new TablaDTO { Id = source.IdTransporte };

            return objR;
        }

        public static CondEspeCliDia SetCondEspeCliDia(this CondEspeCliDiaDTO source)
        {
            var objR = source.CreateMap<CondEspeCliDiaDTO, CondEspeCliDia>();
            if (source.Transporte != null)
            {
                objR.IdTransporte = source.Transporte.Id;
                objR.Transporte = source.Transporte.CreateMap<TablaDTO, Tabla>();
            }
            return objR;
        }
    }
}
