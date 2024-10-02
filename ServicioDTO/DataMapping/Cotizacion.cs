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
        public static CotizacionDTO GetCotizacionDTO(this Cotizacion source)
        {
            var objR = source.CreateMap<Cotizacion, CotizacionDTO>();

            if (source.Proveedor != null)
                objR.Proveedor = source.Proveedor.CreateMap<Proveedor, ProveedorDTO>();
            else
                objR.Proveedor = new ProveedorDTO { Id = source.IdProveedor };

            if (source.OrdenCompra != null)
                objR.OrdenCompra = source.OrdenCompra.CreateMap<OrdenCompra, OrdenCompraDTO>();
            else
                objR.OrdenCompra = new OrdenCompraDTO { Id = source.IdOrdenCompra };

            if (source.Estado != null)
                objR.Estado = source.Estado.CreateMap<Tabla, TablaDTO>();
            else
                objR.Estado = new TablaDTO { Id = source.IdEstado };

            if (source.Pedido != null)
                objR.Pedido = source.Pedido.CreateMap<Pedido, PedidoDTO>();
            else
                objR.Pedido = new PedidoDTO { Id = (source.IdPedido == null ? 0 : Convert.ToInt32(source.IdPedido)) };

            if (source.DetalleCotizaciones.Count > 0)
                foreach (var item in source.DetalleCotizaciones)
                {
                    var objDet = new DetalleCotizacionDTO
                    {
                        Id = item.Id,
                        IdTarifario = item.IdTarifario,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Total = item.Total,
                        Producto = item.Producto.CreateMap<Producto, ProductoDTO>(),
                        Tarifario = (item.Tarifario == null ? null : item.Tarifario.CreateMap<Tarifario, TarifarioDTO>()),
                        Observacion = item.Observacion
                    };
                    objDet.Producto.UnidadMedida = item.Producto.UnidadMedida.CreateMap<Tabla, TablaDTO>();
                    objR.DetalleCotizaciones.Add(objDet);
                }
            return objR;
        }

        public static Cotizacion SetCotizacion(this CotizacionDTO source)
        {
            var objR = source.CreateMap<CotizacionDTO, Cotizacion>();

            if (source.Proveedor != null)
            {
                objR.IdProveedor = source.Proveedor.Id;
                objR.Proveedor = source.Proveedor.CreateMap<ProveedorDTO, Proveedor>();
            }

            if (source.OrdenCompra != null)
            {
                objR.IdOrdenCompra = source.OrdenCompra.Id;
                objR.OrdenCompra = source.OrdenCompra.CreateMap<OrdenCompraDTO, OrdenCompra>();
            }

            if (source.Estado != null)
            {
                objR.IdEstado = source.Estado.Id;
                objR.Estado = source.Estado.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Pedido != null)
            {
                objR.IdPedido = source.Pedido.Id;
                objR.Pedido = source.Pedido.CreateMap<PedidoDTO, Pedido>();
            }

            if (source.DetalleCotizaciones != null)
            {
                foreach (var item in source.DetalleCotizaciones)
                {
                    var objI = new DetalleCotizacion
                    {
                        Id = item.Id,
                        IdProducto = item.IdProducto,
                        IdTarifario = item.IdTarifario,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Total = item.Total,
                        Producto = item.Producto.CreateMap<ProductoDTO, Producto>(),
                        Tarifario = (item.Tarifario == null ? null : item.Tarifario.CreateMap<TarifarioDTO, Tarifario>()),
                        Observacion = item.Observacion
                    };
                    objI.Producto.UnidadMedida = item.Producto.UnidadMedida.CreateMap<TablaDTO, Tabla>();
                    objR.DetalleCotizaciones.Add(objI);
                }
            }

            return objR;
        }
    }
}
