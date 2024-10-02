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
        public static DetalleOrdenCompraDTO GetDetalleOrdenCompraDTO(this DetalleOrdenCompra source)
        {
            var objR = source.CreateMap<DetalleOrdenCompra, DetalleOrdenCompraDTO>();

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

        public static DetalleOrdenCompra SetDetalleOrdenCompra(this DetalleOrdenCompraDTO source)
        {
            var objR = source.CreateMap<DetalleOrdenCompraDTO, DetalleOrdenCompra>();

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
