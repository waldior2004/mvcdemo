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
        public static BitacoraDTO GetBitacoraDTO(this Bitacora source)
        {
            var objR = source.CreateMap<Bitacora, BitacoraDTO>();
            return objR;
        }
        public static Bitacora SetBitacora(this BitacoraDTO source)
        {
            return source.CreateMap<BitacoraDTO, Bitacora>();
        }
    }
}
