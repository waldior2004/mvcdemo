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
        public static UbigeoDTO GetUbigeoDTO(this Ubigeo source)
        {
            var objR = source.CreateMap<Ubigeo, UbigeoDTO>();
            return objR;
        }
        public static Ubigeo SetUbigeo(this UbigeoDTO source)
        {
            var objR = source.CreateMap<UbigeoDTO, Ubigeo>();
            return objR;
        }
    }
}
