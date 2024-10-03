using com.msc.infraestructure.entities.mvc;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static LoginDTO GetLoginDTO(this Login source)
        {
            return source.CreateMap<Login, LoginDTO>();
        }

        public static Login SetLogin(this LoginDTO source)
        {
            return source.CreateMap<LoginDTO, Login>();
        }

    }
}
