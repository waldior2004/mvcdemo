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
        public static TablaDTO GetTablaDTO(this Tabla source)
        {
            var objR = source.CreateMap<Tabla, TablaDTO>();

            if (source.GrupoTabla != null)
                objR.GrupoTabla = source.GrupoTabla.CreateMap<GrupoTabla, GrupoTablaDTO>();
            else
                objR.GrupoTabla = new GrupoTablaDTO { Id = source.IdGrupoTabla };


            return objR;
        }

        public static Tabla SetTabla(this TablaDTO source)
        {
            var objR = source.CreateMap<TablaDTO, Tabla>();
            if (source.GrupoTabla != null)
            {
                objR.IdGrupoTabla = source.GrupoTabla.Id;
                objR.GrupoTabla = source.GrupoTabla.CreateMap<GrupoTablaDTO, GrupoTabla>();
            }

            return objR;
        }
    }
}
