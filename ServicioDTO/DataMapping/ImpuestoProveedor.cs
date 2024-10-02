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
        public static ImpuestoProveedorDTO GetImpuestoProveedorDTO(this ImpuestoProveedor source)
        {
            var objR = source.CreateMap<ImpuestoProveedor, ImpuestoProveedorDTO>();


            if (source.Impuesto != null)
            {
                objR.Impuesto = source.Impuesto.CreateMap<Impuesto, ImpuestoDTO>();
                if (source.Impuesto.TipoImpuesto != null)
                    objR.Impuesto.TipoImpuesto = source.Impuesto.TipoImpuesto.CreateMap<Tabla, TablaDTO>();
            }
            else
                objR.Impuesto = new ImpuestoDTO { Id = source.IdImpuesto };


            return objR;
        }

        public static ImpuestoProveedor SetImpuestoProveedor(this ImpuestoProveedorDTO source)
        {
            var objR = source.CreateMap<ImpuestoProveedorDTO, ImpuestoProveedor>();
            if (source.Impuesto != null)
            {
                objR.Impuesto = source.Impuesto.CreateMap<ImpuestoDTO, Impuesto>();
                if (source.Impuesto.TipoImpuesto != null)
                {
                    objR.Impuesto.IdTipoImpuesto = source.Impuesto.TipoImpuesto.Id;
                    objR.Impuesto.TipoImpuesto = source.Impuesto.TipoImpuesto.CreateMap<TablaDTO, Tabla>();
                }
            }
            return objR;
        }
    }
}
