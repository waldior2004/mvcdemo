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
        public static SubFamiliaDTO GetSubFamiliaDTO(this SubFamilia source)
        {
            var objR = source.CreateMap<SubFamilia, SubFamiliaDTO>();

            if (source.Familia != null)
                objR.Familia = source.Familia.CreateMap<Familia, FamiliaDTO>();
            else
                objR.Familia = new FamiliaDTO { Id = source.IdFamilia };

            return objR;
        }

        public static SubFamilia SetSubFamilia(this SubFamiliaDTO source)
        {
            var objR = source.CreateMap<SubFamiliaDTO, SubFamilia>();

            if (source.Familia != null)
            {
                objR.IdFamilia = source.Familia.Id;
                objR.Familia = source.Familia.CreateMap<FamiliaDTO, Familia>();
            }

            return objR;
        }
    }
}
