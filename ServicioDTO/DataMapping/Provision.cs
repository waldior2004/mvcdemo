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
        public static ProvisionDTO GetProvisionDTO(this Provision source)
        {
            var objR = source.CreateMap<Provision, ProvisionDTO>();

            if (source.Proveedor != null)
                objR.Proveedor = source.Proveedor.CreateMap<Proveedor, ProveedorDTO>();
            else
                objR.Proveedor = new ProveedorDTO { Id = source.IdProveedor };

            if (source.Estado != null)
                objR.Estado = source.Estado.CreateMap<Tabla, TablaDTO>();
            else
                objR.Estado = new TablaDTO { Id = source.IdEstado };

            if (source.Empresa != null)
                objR.Empresa = source.Empresa.CreateMap<Empresa, EmpresaDTO>();
            else
                objR.Empresa = new EmpresaDTO { Id = source.IdEmpresa };

            if (source.Sucursal != null)
                objR.Sucursal = source.Sucursal.CreateMap<Sucursal, SucursalDTO>();
            else
                objR.Sucursal = new SucursalDTO { Id = source.IdSucursal };

            if (source.TipoProvision != null)
                objR.TipoProvision = source.TipoProvision.CreateMap<Tabla, TablaDTO>();
            else
                objR.TipoProvision = new TablaDTO { Id = source.IdTipoProvision };

            if (source.CuentaContable != null)
                objR.CuentaContable = source.CuentaContable.CreateMap<Tabla, TablaDTO>();
            else
                objR.CuentaContable = new TablaDTO { Id = source.IdCuentaContable };

            if (source.Moneda != null)
                objR.Moneda = source.Moneda.CreateMap<Tabla, TablaDTO>();
            else
                objR.Moneda = new TablaDTO { Id = source.IdMoneda };

            if (source.OrdenCompra != null)
                objR.OrdenCompra = source.OrdenCompra.CreateMap<OrdenCompra, OrdenCompraDTO>();
            else
                objR.OrdenCompra = new OrdenCompraDTO { Id = Convert.ToInt32(source.IdOrdenCompra) };

            return objR;
        }

        public static Provision SetProvision(this ProvisionDTO source)
        {
            var objR = source.CreateMap<ProvisionDTO, Provision>();

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

            if (source.Empresa != null)
            {
                objR.IdEmpresa = source.Empresa.Id;
                objR.Empresa = source.Empresa.CreateMap<EmpresaDTO, Empresa>();
            }

            if (source.Sucursal != null)
            {
                objR.IdSucursal = source.Sucursal.Id;
                objR.Sucursal = source.Sucursal.CreateMap<SucursalDTO, Sucursal>();
            }

            if (source.TipoProvision != null)
            {
                objR.IdTipoProvision = source.TipoProvision.Id;
                objR.TipoProvision = source.TipoProvision.CreateMap<TablaDTO, Tabla>();
            }

            if (source.CuentaContable != null)
            {
                objR.IdCuentaContable = source.CuentaContable.Id;
                objR.CuentaContable = source.CuentaContable.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Moneda != null)
            {
                objR.IdMoneda = source.Moneda.Id;
                objR.Moneda = source.Moneda.CreateMap<TablaDTO, Tabla>();
            }

            if (source.OrdenCompra != null)
            {
                objR.IdOrdenCompra = source.OrdenCompra.Id;
                objR.OrdenCompra = source.OrdenCompra.CreateMap<OrdenCompraDTO, OrdenCompra>();
            }

            return objR;
        }
    }
}
