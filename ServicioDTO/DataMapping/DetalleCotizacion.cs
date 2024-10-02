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
        public static DetalleCotizacionDTO GetDetalleCotizacionDTO(this DetalleCotizacion source)
        {
            var objR = source.CreateMap<DetalleCotizacion, DetalleCotizacionDTO>();

            if (source.Producto != null)
            {
                objR.Producto = source.Producto.CreateMap<Producto, ProductoDTO>();
                if (source.Producto.UnidadMedida != null)
                    objR.Producto.UnidadMedida = source.Producto.UnidadMedida.CreateMap<Tabla, TablaDTO>();
            }
            else
                objR.Producto = new ProductoDTO { Id = source.IdProducto };

            if (source.Tarifario != null)
                objR.Tarifario = source.Tarifario.CreateMap<Tarifario, TarifarioDTO>();
            else
                objR.Tarifario = new TarifarioDTO { Id = (source.IdTarifario == null ? 0 : Convert.ToInt32(source.IdTarifario)) };

            return objR;
        }

        public static DetalleCotizacion SetDetalleCotizacion(this DetalleCotizacionDTO source)
        {
            var objR = source.CreateMap<DetalleCotizacionDTO, DetalleCotizacion>();

            if (source.Producto != null)
            {
                objR.IdProducto = source.Producto.Id;
                objR.Producto = source.Producto.CreateMap<ProductoDTO, Producto>();

                if (source.Producto.UnidadMedida != null)
                    objR.Producto.UnidadMedida = source.Producto.UnidadMedida.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Tarifario != null)
            {
                objR.IdTarifario = source.Tarifario.Id;
                objR.Tarifario = source.Tarifario.CreateMap<TarifarioDTO, Tarifario>();
            }

            return objR;
        }
    }
}
