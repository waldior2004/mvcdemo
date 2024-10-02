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
        public static OrdenCompraDTO GetOrdenCompraDTO(this OrdenCompra source)
        {
            var objR = source.CreateMap<OrdenCompra, OrdenCompraDTO>();

            if (source.Proveedor != null)
                objR.Proveedor = source.Proveedor.CreateMap<Proveedor, ProveedorDTO>();
            else
                objR.Proveedor = new ProveedorDTO { Id = source.IdProveedor };

            if (source.Estado != null)
                objR.Estado = source.Estado.CreateMap<Tabla, TablaDTO>();
            else
                objR.Estado = new TablaDTO { Id = source.IdEstado };

            if (source.Cotizacion != null)
                objR.Cotizacion = source.Cotizacion.CreateMap<Cotizacion, CotizacionDTO>();
            else
                objR.Cotizacion = new CotizacionDTO { Id = source.IdCotizacion };

            if (source.DetalleOrdenCompras.Count > 0)
                foreach (var item in source.DetalleOrdenCompras)
                {
                    var objDet = new DetalleOrdenCompraDTO
                    {
                        Id = item.Id,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Total = item.Total,
                        Producto = item.Producto.CreateMap<Producto, ProductoDTO>(),
                        Observacion = item.Observacion
                    };
                    objDet.Producto.UnidadMedida = item.Producto.UnidadMedida.CreateMap<Tabla, TablaDTO>();
                    objR.DetalleOrdenCompras.Add(objDet);
                }

            if (source.OrdenCompraDocs.Count > 0)
                foreach (var item in source.OrdenCompraDocs)
                {
                    objR.Documentos.Add(new DocumentoDTO
                    {
                        IdTarifaCEDoc = item.Id,
                        Id = item.IdDocumento,
                        Nombre = item.Documento.Nombre,
                        Extension = item.Documento.Extension,
                        RutaLocal = item.Documento.RutaLocal,
                        TamanoMB = item.Documento.TamanoMB
                    });
                }

            return objR;
        }

        public static OrdenCompra SetOrdenCompra(this OrdenCompraDTO source)
        {
            var objR = source.CreateMap<OrdenCompraDTO, OrdenCompra>();

            if (source.Proveedor != null)
            {
                objR.IdProveedor = source.Proveedor.Id;
                objR.Proveedor = source.Proveedor.CreateMap<ProveedorDTO, Proveedor>();
            }

            if (source.Estado != null)
            {
                objR.IdEstado = source.Estado.Id;
                objR.Estado = source.Estado.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Cotizacion != null)
            {
                objR.IdCotizacion = source.Cotizacion.Id;
                objR.Cotizacion = source.Cotizacion.CreateMap<CotizacionDTO, Cotizacion>();
            }

            if (source.DetalleOrdenCompras != null)
            {
                foreach (var item in source.DetalleOrdenCompras)
                {
                    var objI = new DetalleOrdenCompra
                    {
                        Id = item.Id,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Total = item.Total,
                        Producto = item.Producto.CreateMap<ProductoDTO, Producto>(),
                        Observacion = item.Observacion
                    };
                    objI.Producto.UnidadMedida = item.Producto.UnidadMedida.CreateMap<TablaDTO, Tabla>();
                    objR.DetalleOrdenCompras.Add(objI);
                }
            }

            if (source.Documentos != null)
            {
                foreach (var item in source.Documentos)
                {
                    var objI = new OrdenCompraDoc
                    {
                        Id = item.IdTarifaCEDoc,
                        IdOrdenCompra = source.Id,
                        IdDocumento = item.Id,
                        Documento = item.CreateMap<DocumentoDTO, Documento>()
                    };
                    objR.OrdenCompraDocs.Add(objI);
                }
            }

            return objR;
        }

    }
}
