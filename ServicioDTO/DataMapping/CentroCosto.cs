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
        public static CentroCostoDTO GetCentroCostoDTO(this CentroCosto source)
        {
            var objR = source.CreateMap<CentroCosto, CentroCostoDTO>();
            return objR;
        }

        public static CentroCosto SetCentroCosto(this CentroCostoDTO source)
        {
            var objR = source.CreateMap<CentroCostoDTO, CentroCosto>();
            return objR;
        }
    }
}
