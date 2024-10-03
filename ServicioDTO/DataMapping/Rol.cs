using com.msc.infraestructure.entities;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static RolDTO GetRolDTO(this Rol source)
        {
            var objR = source.CreateMap<Rol, RolDTO>();
            return objR;
        }

        public static Rol SetRol(this RolDTO source)
        {
            return source.CreateMap<RolDTO, Rol>();
        }
    }
}
