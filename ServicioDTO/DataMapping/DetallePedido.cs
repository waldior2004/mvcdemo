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
        public static DetallePedidoDTO GetDetallePedidoDTO(this DetallePedido source)
        {
            var objR = source.CreateMap<DetallePedido, DetallePedidoDTO>();

            if (source.Producto != null)
            {
                objR.Producto = source.Producto.CreateMap<Producto, ProductoDTO>();
                if (source.Producto.UnidadMedida != null)
                    objR.Producto.UnidadMedida = source.Producto.UnidadMedida.CreateMap<Tabla, TablaDTO>();
            }
            else
                objR.Producto = new ProductoDTO { Id = source.IdProducto };

            return objR;
        }

        public static DetallePedido SetDetallePedido(this DetallePedidoDTO source)
        {
            var objR = source.CreateMap<DetallePedidoDTO, DetallePedido>();

            if (source.Producto != null)
            {
                objR.IdProducto = source.Producto.Id;
                objR.Producto = source.Producto.CreateMap<ProductoDTO, Producto>();

                if (source.Producto.UnidadMedida != null)
                    objR.Producto.UnidadMedida = source.Producto.UnidadMedida.CreateMap<TablaDTO, Tabla>();
            }

            return objR;
        }
    }
}
