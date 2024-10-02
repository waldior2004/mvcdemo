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
        public static ExternoPerfilDTO GetExternoPerfilDTO(this ExternoPerfil source)
        {
            var objR = source.CreateMap<ExternoPerfil, ExternoPerfilDTO>();
            return objR;
        }

        public static ExternoPerfil SetExternoPerfil(this ExternoPerfilDTO source)
        {
            var objR = source.CreateMap<ExternoPerfilDTO, ExternoPerfil>();
            return objR;
        }
    }
}
