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
        public static PerfilControlDTO GetPerfilControlDTO(this PerfilControl source)
        {
            var objR = source.CreateMap<PerfilControl, PerfilControlDTO>();
            return objR;
        }
        public static PerfilControl SetPerfilControl(this PerfilControlDTO source)
        {
            var objR = source.CreateMap<PerfilControlDTO, PerfilControl>();
            return objR;
        }
    }
}
