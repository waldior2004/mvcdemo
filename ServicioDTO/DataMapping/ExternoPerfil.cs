using com.msc.infraestructure.entities;

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
