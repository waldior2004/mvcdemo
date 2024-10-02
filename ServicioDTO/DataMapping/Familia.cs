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
        public static FamiliaDTO GetFamiliaDTO(this Familia source)
        {
            var objR = source.CreateMap<Familia, FamiliaDTO>();
            return objR;
        }

        public static Familia SetFamilia(this FamiliaDTO source)
        {
            return source.CreateMap<FamiliaDTO, Familia>();
        }
    }
}
