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
        public static NaveDTO GetNaveDTO(this Nave source)
        {
            var objR = source.CreateMap<Nave, NaveDTO>();
            return objR;
        }
    }
}
