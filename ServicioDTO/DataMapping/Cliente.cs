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
        public static ClienteDTO GetClienteDTO(this Cliente source)
        {
            var objR = source.CreateMap<Cliente, ClienteDTO>();
            return objR;
        }


    }
}
