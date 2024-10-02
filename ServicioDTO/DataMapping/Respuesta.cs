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
        public static RespuestaDTO GetRespuestaDTO(this Respuesta source)
        {
            return source.CreateMap<Respuesta, RespuestaDTO>();
        }

        public static Respuesta SetRespuesta(this RespuestaDTO source)
        {
            return source.CreateMap<RespuestaDTO, Respuesta>();
        }
    }
}
