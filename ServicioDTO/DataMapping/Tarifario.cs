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
        public static TarifarioDTO GetTarifarioDTO(this Tarifario source)
        {
            var objR = source.CreateMap<Tarifario, TarifarioDTO>();

            if (source.Proveedor != null)
                objR.Proveedor = source.Proveedor.CreateMap<Proveedor, ProveedorDTO>();
            else
                objR.Proveedor = new ProveedorDTO { Id = source.IdProveedor };

            if (source.Producto != null)
            {
                objR.Producto = source.Producto.CreateMap<Producto, ProductoDTO>();
                if (source.Producto.UnidadMedida != null)
                    objR.Producto.UnidadMedida = source.Producto.UnidadMedida.CreateMap<Tabla, TablaDTO>();
            }
            else
                objR.Producto = new ProductoDTO { Id = source.IdProducto };

            if (source.Moneda != null)
                objR.Moneda = source.Moneda.CreateMap<Tabla, TablaDTO>();
            else
                objR.Moneda = new TablaDTO { Id = source.IdMoneda };

            if (source.Estado != null)
                objR.Estado = source.Estado.CreateMap<Tabla, TablaDTO>();
            else
                objR.Estado = new TablaDTO { Id = source.IdEstado };

            if (source.TarifarioDocs.Count > 0)
                foreach (var item in source.TarifarioDocs)
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

            if (source.V_Tarifarios.Count > 0)
                foreach (var item in source.V_Tarifarios)
                {
                    objR.Vtarifarios.Add(new V_TarifarioDTO
                    {
                        Descripcion = item.Descripcion,
                        Producto = item.Producto.CreateMap<Producto, ProductoDTO>(),
                        Proveedor = item.Proveedor.CreateMap<Proveedor, ProveedorDTO>(),
                        Moneda = item.Moneda.CreateMap<Tabla, TablaDTO>(),
                        Estado = item.Estado.CreateMap<Tabla, TablaDTO>(),
                        Precio = item.Precio,
                        InicioVigencia = item.InicioVigencia,
                        FinVigencia = item.FinVigencia
                    });
                }


            return objR;
        }

        public static Tarifario SetTarifario(this TarifarioDTO source)
        {
            var objR = source.CreateMap<TarifarioDTO, Tarifario>();

            if (source.Proveedor != null)
            {
                objR.IdProveedor = source.Proveedor.Id;
                objR.Proveedor = source.Proveedor.CreateMap<ProveedorDTO, Proveedor>();
            }

            if (source.Producto != null)
            {
                objR.IdProducto = source.Producto.Id;
                objR.Producto = source.Producto.CreateMap<ProductoDTO, Producto>();
                if (source.Producto.UnidadMedida != null)
                    objR.Producto.UnidadMedida = source.Producto.UnidadMedida.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Estado != null)
            {
                objR.IdEstado = source.Estado.Id;
                objR.Estado = source.Estado.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Moneda != null)
            {
                objR.IdMoneda = source.Moneda.Id;
                objR.Moneda = source.Moneda.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Documentos != null)
            {
                foreach (var item in source.Documentos)
                {
                    var objI = new TarifarioDoc
                    {
                        Id = item.IdTarifaCEDoc,
                        IdTarifario = source.Id,
                        IdDocumento = item.Id,
                        Documento = item.CreateMap<DocumentoDTO, Documento>()
                    };
                    objR.TarifarioDocs.Add(objI);
                }
            }

            if (source.Vtarifarios.Count > 0)
                foreach (var item in source.Vtarifarios)
                {
                    objR.V_Tarifarios.Add(new V_Tarifario
                    {
                        Descripcion = item.Descripcion,
                        IdProducto = item.Producto.Id,
                        Producto = item.Producto.CreateMap<ProductoDTO, Producto>(),
                        IdProveedor = item.Proveedor.Id,
                        Proveedor = item.Proveedor.CreateMap<ProveedorDTO, Proveedor>(),
                        IdMoneda = item.Moneda.Id,
                        Moneda = item.Moneda.CreateMap<TablaDTO, Tabla>(),
                        IdEstado = item.Estado.Id,
                        Estado = item.Estado.CreateMap<TablaDTO, Tabla>(),
                        Precio = item.Precio,
                        InicioVigencia = item.InicioVigencia,
                        FinVigencia = item.FinVigencia
                    });
                }

            return objR;
        }
    }
}
