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
        public static TareaDTO GetTareaDTO(this Tarea source)
        {
            var objR = source.CreateMap<Tarea, TareaDTO>();

            return objR;
        }

        public static Tarea SetTarea(this TareaDTO source)
        {
            var objR = source.CreateMap<TareaDTO, Tarea>();

            return objR;
        }
    }
}
