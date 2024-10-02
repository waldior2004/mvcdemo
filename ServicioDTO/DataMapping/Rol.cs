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
