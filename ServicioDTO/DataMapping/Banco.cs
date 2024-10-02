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
        public static BancoDTO GetBancoDTO(this Banco source)
        {
            var objR = source.CreateMap<Banco, BancoDTO>();

            if (source.Pais != null)
                objR.Pais = source.Pais.CreateMap<Tabla, TablaDTO>();
            else
                objR.Pais = new TablaDTO { Id = source.IdPais };

            return objR;
        }

        public static Banco SetBanco(this BancoDTO source)
        {
            var objR = source.CreateMap<BancoDTO, Banco>();
            if (source.Pais != null)
            {
                objR.IdPais = source.Pais.Id;
                objR.Pais = source.Pais.CreateMap<TablaDTO, Tabla>();
            }

            return objR;
        }
    }
}
