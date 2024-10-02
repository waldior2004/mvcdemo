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
        public static SucursalDTO GetSucursalDTO(this Sucursal source)
        {
            var objR = source.CreateMap<Sucursal, SucursalDTO>();

            if (source.Empresa != null)
                objR.Empresa = source.Empresa.CreateMap<Empresa, EmpresaDTO>();
            else
                objR.Empresa = new EmpresaDTO { Id = source.IdEmpresa };

            return objR;
        }

        public static Sucursal SetSucursal(this SucursalDTO source)
        {
            var objR = source.CreateMap<SucursalDTO, Sucursal>();
            if (source.Empresa != null)
            {
                objR.IdEmpresa = source.Empresa.Id;
                objR.Empresa = source.Empresa.CreateMap<EmpresaDTO, Empresa>();
            }
            return objR;
        }
    }
}
