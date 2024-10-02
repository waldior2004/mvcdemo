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
        public static PedidoDTO GetPedidoDTO(this Pedido source)
        {
            var objR = source.CreateMap<Pedido, PedidoDTO>();

            if (source.Empresa != null)
                objR.Empresa = source.Empresa.CreateMap<Empresa, EmpresaDTO>();
            else
                objR.Empresa = new EmpresaDTO { Id = source.IdEmpresa };

            if (source.OrdenCompra != null)
                objR.OrdenCompra = source.OrdenCompra.CreateMap<OrdenCompra, OrdenCompraDTO>();
            else
                objR.OrdenCompra = new OrdenCompraDTO { Id = source.IdOrdenCompra };

            if (source.Sucursal != null)
                objR.Sucursal = source.Sucursal.CreateMap<Sucursal, SucursalDTO>();
            else
                objR.Sucursal = new SucursalDTO { Id = source.IdSucursal };

            if (source.AreaSolicitante != null)
                objR.AreaSolicitante = source.AreaSolicitante.CreateMap<AreaSolicitante, AreaSolicitanteDTO>();
            else
                objR.AreaSolicitante = new AreaSolicitanteDTO { Id = source.IdAreaSolicitante };

            if (source.TipoPeticion != null)
                objR.TipoPeticion = source.TipoPeticion.CreateMap<Tabla, TablaDTO>();
            else
                objR.TipoPeticion = new TablaDTO { Id = source.IdTipoPeticion };

            if (source.Estado != null)
                objR.Estado = source.Estado.CreateMap<Tabla, TablaDTO>();
            else
                objR.Estado = new TablaDTO { Id = source.IdEstado };

            if (source.CentroCosto != null)
                objR.CentroCosto = source.CentroCosto.CreateMap<CentroCosto, CentroCostoDTO>();
            else
                objR.CentroCosto = new CentroCostoDTO { Id = source.IdCentroCosto };

            if (source.DetallePedidos.Count > 0)
                foreach (var item in source.DetallePedidos)
                {
                    var objDet = new DetallePedidoDTO
                    {
                        Id = item.Id,
                        IdPedido = item.IdPedido,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Total = item.Total,
                        Observaciones = item.Observaciones,
                        Producto = item.Producto.CreateMap<Producto, ProductoDTO>()
                    };
                    objDet.Producto.UnidadMedida = item.Producto.UnidadMedida.CreateMap<Tabla, TablaDTO>();
                    objR.DetallePedidos.Add(objDet);
                }

            if (source.Cotizaciones.Count > 0)
                foreach (var item in source.Cotizaciones)
                {
                    var objDet = new CotizacionDTO
                    {
                        Id = item.Id,
                        IdPedido = (item.IdPedido == null ? 0 : Convert.ToInt32(item.IdPedido)),
                        IdProveedor = item.IdProveedor,
                        IdEstado = item.IdEstado,
                        Proveedor = item.Proveedor.CreateMap<Proveedor, ProveedorDTO>(),
                        Estado = item.Estado.CreateMap<Tabla, TablaDTO>(),
                        FechaCotizacion = item.FechaCotizacion,
                        FechaEntrega = item.FechaEntrega,
                        Observacion = item.Observacion,
                        Codigo = item.Codigo
                    };

                    if (item.Proveedor.ContactoProveedor.Count > 0)
                    {
                        var contactos = new List<ContactoProveedorDTO>();

                        foreach (var subitem in item.Proveedor.ContactoProveedor)
                        {
                            contactos.Add(subitem.CreateMap<ContactoProveedor, ContactoProveedorDTO>());
                        }

                        objDet.Proveedor.ContactoProveedor = contactos;
                    }

                    if (item.DetalleCotizaciones.Count > 0)
                    {
                        foreach (var subitem in item.DetalleCotizaciones)
                        {
                            var objDetaC = new DetalleCotizacionDTO
                            {
                                Id = subitem.Id,
                                IdCotizacion = item.Id,
                                IdProducto = subitem.IdProducto,
                                IdTarifario = subitem.IdTarifario,
                                Cantidad = subitem.Cantidad,
                                Precio = subitem.Precio,
                                Observacion = subitem.Observacion,
                                Producto = subitem.Producto.CreateMap<Producto, ProductoDTO>(),
                                Tarifario = (subitem.Tarifario == null ? null : subitem.Tarifario.CreateMap<Tarifario, TarifarioDTO>()),
                                Total = subitem.Total
                            };
                            objDetaC.Producto.UnidadMedida = subitem.Producto.UnidadMedida.CreateMap<Tabla, TablaDTO>();
                            objDet.DetalleCotizaciones.Add(objDetaC);
                        }
                    }

                    objR.Cotizaciones.Add(objDet);
                }

            return objR;
        }

        public static Pedido SetPedido(this PedidoDTO source)
        {
            var objR = source.CreateMap<PedidoDTO, Pedido>();

            if (source.Empresa != null)
            {
                objR.IdEmpresa = source.Empresa.Id;
                objR.Empresa = source.Empresa.CreateMap<EmpresaDTO, Empresa>();
            }

            if (source.OrdenCompra != null)
            {
                objR.IdOrdenCompra = source.OrdenCompra.Id;
                objR.OrdenCompra = source.OrdenCompra.CreateMap<OrdenCompraDTO, OrdenCompra>();
            }

            if (source.Sucursal != null)
            {
                objR.IdSucursal = source.Sucursal.Id;
                objR.Sucursal = source.Sucursal.CreateMap<SucursalDTO, Sucursal>();
            }

            if (source.AreaSolicitante != null)
            {
                objR.IdAreaSolicitante = source.AreaSolicitante.Id;
                objR.AreaSolicitante = source.AreaSolicitante.CreateMap<AreaSolicitanteDTO, AreaSolicitante>();
            }

            if (source.TipoPeticion != null)
            {
                objR.IdTipoPeticion = source.TipoPeticion.Id;
                objR.TipoPeticion = source.TipoPeticion.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Estado != null)
            {
                objR.IdEstado = source.Estado.Id;
                objR.Estado = source.Estado.CreateMap<TablaDTO, Tabla>();
            }

            if (source.CentroCosto != null)
            {
                objR.IdCentroCosto = source.CentroCosto.Id;
                objR.CentroCosto = source.CentroCosto.CreateMap<CentroCostoDTO, CentroCosto>();
            }

            if (source.DetallePedidos != null)
            {
                foreach (var item in source.DetallePedidos)
                {
                    var objI = new DetallePedido
                    {
                        Id = item.Id,
                        IdPedido = source.Id,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        Observaciones = item.Observaciones,
                        Precio = item.Precio,
                        Total = item.Total,
                        Producto = item.Producto.CreateMap<ProductoDTO, Producto>()
                    };
                    objI.Producto.UnidadMedida = item.Producto.UnidadMedida.CreateMap<TablaDTO, Tabla>();
                    objR.DetallePedidos.Add(objI);
                }
            }

            if (source.Cotizaciones != null)
            {
                foreach (var item in source.Cotizaciones)
                {
                    var objI = new Cotizacion
                    {
                        Id = item.Id,
                        IdPedido = source.Id,
                        IdProveedor = item.IdProveedor,
                        Proveedor = item.Proveedor.CreateMap<ProveedorDTO, Proveedor>(),
                        IdEstado = item.IdEstado,
                        Estado = item.Estado.CreateMap<TablaDTO, Tabla>(),
                        FechaCotizacion = item.FechaCotizacion,
                        FechaEntrega = item.FechaEntrega,
                        Observacion = item.Observacion,
                        Codigo = item.Codigo
                    };

                    if (item.Proveedor.ContactoProveedor.Count > 0)
                    {
                        var contactos = new List<ContactoProveedor>();

                        foreach (var subitem in item.Proveedor.ContactoProveedor)
                        {
                            contactos.Add(subitem.CreateMap<ContactoProveedorDTO, ContactoProveedor>());
                        }

                        objI.Proveedor.ContactoProveedor = contactos;
                    }

                    if (item.DetalleCotizaciones.Count > 0)
                    {
                        foreach (var subitem in item.DetalleCotizaciones)
                        {
                            var objDetaC = new DetalleCotizacion
                            {
                                Id = subitem.Id,
                                IdProducto = subitem.IdProducto,
                                IdTarifario = subitem.IdTarifario,
                                IdCotizacion = subitem.IdCotizacion,
                                Precio = subitem.Precio,
                                Total = subitem.Total,
                                Observacion = subitem.Observacion,
                                Cantidad = subitem.Cantidad,
                                Producto = subitem.Producto.CreateMap<ProductoDTO, Producto>(),
                                Tarifario = (subitem.Tarifario == null ? null : subitem.Tarifario.CreateMap<TarifarioDTO, Tarifario>())
                            };
                            objDetaC.Producto.UnidadMedida = subitem.Producto.UnidadMedida.CreateMap<TablaDTO, Tabla>();
                            objI.DetalleCotizaciones.Add(objDetaC);
                        }
                    }

                    objR.Cotizaciones.Add(objI);
                }
            }

            return objR;
        }

    }
}
