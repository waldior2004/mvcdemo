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
        public static ImpuestoDTO GetImpuestoDTO(this Impuesto source)
        {
            var objR = source.CreateMap<Impuesto, ImpuestoDTO>();

            if (source.TipoImpuesto != null)
                objR.TipoImpuesto = source.TipoImpuesto.CreateMap<Tabla, TablaDTO>();
            else
                objR.TipoImpuesto = new TablaDTO { Id = source.IdTipoImpuesto };

            return objR;
        }

        public static Impuesto SetImpuesto(this ImpuestoDTO source)
        {
            var objR = source.CreateMap<ImpuestoDTO, Impuesto>();
            if (source.TipoImpuesto != null)
            {
                objR.IdTipoImpuesto = source.TipoImpuesto.Id;
                objR.TipoImpuesto = source.TipoImpuesto.CreateMap<TablaDTO, Tabla>();
            }

            return objR;
        }
    }
}
