using com.msc.infraestructure.entities.mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
