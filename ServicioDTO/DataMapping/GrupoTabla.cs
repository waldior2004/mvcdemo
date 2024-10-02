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
        public static GrupoTablaDTO GetGrupoTablaDTO(this GrupoTabla source)
        {
            var objR = source.CreateMap<GrupoTabla, GrupoTablaDTO>();
            return objR;
        }

        public static GrupoTabla SetGrupoTabla(this GrupoTablaDTO source)
        {
            return source.CreateMap<GrupoTablaDTO, GrupoTabla>();
        }
    }
}
