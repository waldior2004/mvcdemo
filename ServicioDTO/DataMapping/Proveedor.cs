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
        public static ProveedorDTO GetProveedorDTO(this Proveedor source)
        {
            var objR = source.CreateMap<Proveedor, ProveedorDTO>();

            if (source.Pais != null)
                objR.Pais = source.Pais.CreateMap<Tabla, TablaDTO>();
            else if (source.IdPais != null)
                objR.Pais = new TablaDTO { Id = Convert.ToInt32(source.IdPais) };

            if (source.Departamento != null)
                objR.Departamento = source.Departamento.CreateMap<Ubigeo, UbigeoDTO>();
            else if (source.IdDepartamento != null)
                objR.Departamento = new UbigeoDTO { Id = Convert.ToInt32(source.IdDepartamento) };

            if (source.Provincia != null)
                objR.Provincia = source.Provincia.CreateMap<Ubigeo, UbigeoDTO>();
            else if (source.IdProvincia != null)
                objR.Provincia = new UbigeoDTO { Id = Convert.ToInt32(source.IdProvincia) };

            if (source.Distrito != null)
                objR.Distrito = source.Distrito.CreateMap<Ubigeo, UbigeoDTO>();
            else if (source.IdDistrito != null)
                objR.Distrito = new UbigeoDTO { Id = Convert.ToInt32(source.IdDistrito) };

            if (source.TipoContribuyente != null)
                objR.TipoContribuyente = source.TipoContribuyente.CreateMap<Tabla, TablaDTO>();
            else if (source.IdTipoContribuyente != null)
                objR.TipoContribuyente = new TablaDTO { Id = Convert.ToInt32(source.IdTipoContribuyente) };

            if (source.TipoPersona != null)
                objR.TipoPersona = source.TipoPersona.CreateMap<Tabla, TablaDTO>();
            else if (source.IdTipoPersona != null)
                objR.TipoPersona = new TablaDTO { Id = Convert.ToInt32(source.IdTipoPersona) };

            if (source.GiroNegocio != null)
                objR.GiroNegocio = source.GiroNegocio.CreateMap<Tabla, TablaDTO>();
            else if (source.IdGiroNegocio != null)
                objR.GiroNegocio = new TablaDTO { Id = Convert.ToInt32(source.IdGiroNegocio) };

            if (source.TipoNIF != null)
                objR.TipoNIF = source.TipoNIF.CreateMap<Tabla, TablaDTO>();
            else if (source.IdTipoNIF != null)
                objR.TipoNIF = new TablaDTO { Id = Convert.ToInt32(source.IdTipoNIF) };

            if (source.FormaCobro != null)
                objR.FormaCobro = source.FormaCobro.CreateMap<Tabla, TablaDTO>();
            else if (source.IdFormaCobro != null)
                objR.FormaCobro = new TablaDTO { Id = Convert.ToInt32(source.IdFormaCobro) };

            foreach (var item in source.DatoComercial)
            {
                var objDC = item.CreateMap<DatoComercial, DatoComercialDTO>();

                if (item.Pais != null)
                    objDC.Pais = item.Pais.CreateMap<Tabla, TablaDTO>();

                if (item.Banco != null)
                    objDC.Banco = item.Banco.CreateMap<Banco, BancoDTO>();

                if (item.TipoCuenta != null)
                    objDC.TipoCuenta = item.TipoCuenta.CreateMap<Tabla, TablaDTO>();

                if (item.TipoInterlocutor != null)
                    objDC.TipoInterlocutor = item.TipoInterlocutor.CreateMap<Tabla, TablaDTO>();

                objR.DatoComercial.Add(objDC);
            }
            foreach (var item in source.ContactoProveedor)
            {
                var objCP = item.CreateMap<ContactoProveedor, ContactoProveedorDTO>();

                if (item.Cargo != null)
                    objCP.Cargo = item.Cargo.CreateMap<Tabla, TablaDTO>();

                objR.ContactoProveedor.Add(objCP);
            }

            foreach (var item in source.Impuestos)
            {
                var objImp = item.CreateMap<ImpuestoProveedor, ImpuestoProveedorDTO>();
                if (item.Impuesto != null)
                {
                    objImp.Impuesto = item.Impuesto.CreateMap<Impuesto, ImpuestoDTO>();
                    objImp.Impuesto.TipoImpuesto = item.Impuesto.TipoImpuesto.CreateMap<Tabla, TablaDTO>();
                }
                objR.Impuestos.Add(objImp);
            }

            return objR;
        }

        public static Proveedor SetProveedor(this ProveedorDTO source)
        {
            var objR = source.CreateMap<ProveedorDTO, Proveedor>();

            if (source.Pais != null)
            {
                objR.Pais = source.Pais.CreateMap<TablaDTO, Tabla>();
                objR.IdPais = source.Pais.Id;
            }

            if (source.Departamento != null)
            {
                objR.Departamento = source.Departamento.CreateMap<UbigeoDTO, Ubigeo>();
                objR.IdDepartamento = source.Departamento.Id;
            }

            if (source.Provincia != null)
            {
                objR.Provincia = source.Provincia.CreateMap<UbigeoDTO, Ubigeo>();
                objR.IdProvincia = source.Provincia.Id;
            }

            if (source.Distrito != null)
            {
                objR.Distrito = source.Distrito.CreateMap<UbigeoDTO, Ubigeo>();
                objR.IdDistrito = source.Distrito.Id;
            }

            if (source.TipoPersona != null)
            {
                objR.TipoPersona = source.TipoPersona.CreateMap<TablaDTO, Tabla>();
                objR.IdTipoPersona = source.TipoPersona.Id;
            }

            if (source.TipoContribuyente != null)
            {
                objR.TipoContribuyente = source.TipoContribuyente.CreateMap<TablaDTO, Tabla>();
                objR.IdTipoContribuyente = source.TipoContribuyente.Id;
            }

            if (source.GiroNegocio != null)
            {
                objR.GiroNegocio = source.GiroNegocio.CreateMap<TablaDTO, Tabla>();
                objR.IdGiroNegocio = source.GiroNegocio.Id;
            }

            if (source.TipoNIF != null)
            {
                objR.TipoNIF = source.TipoNIF.CreateMap<TablaDTO, Tabla>();
                objR.IdTipoNIF = source.TipoNIF.Id;
            }

            if (source.FormaCobro != null)
            {
                objR.FormaCobro = source.FormaCobro.CreateMap<TablaDTO, Tabla>();
                objR.IdFormaCobro = source.FormaCobro.Id;
            }
            foreach (var item in source.DatoComercial)
            {
                var objDC = item.CreateMap<DatoComercialDTO, DatoComercial>();

                if (item.Pais != null)
                    objDC.Pais = item.Pais.CreateMap<TablaDTO, Tabla>();

                if (item.Banco != null)
                    objDC.Banco = item.Banco.CreateMap<BancoDTO, Banco>();

                if (item.TipoCuenta != null)
                    objDC.TipoCuenta = item.TipoCuenta.CreateMap<TablaDTO, Tabla>();

                if (item.TipoInterlocutor != null)
                    objDC.TipoInterlocutor = item.TipoInterlocutor.CreateMap<TablaDTO, Tabla>();

                objR.DatoComercial.Add(objDC);

            }
            foreach (var item in source.ContactoProveedor)
            {
                var objCP = item.CreateMap<ContactoProveedorDTO, ContactoProveedor>();

                if (item.Cargo != null)
                {
                    objCP.Cargo = item.Cargo.CreateMap<TablaDTO, Tabla>();
                    objCP.IdCargo = item.Id;
                }

                objR.ContactoProveedor.Add(objCP);

            }

            foreach (var item in source.Impuestos)
            {
                var objCP = item.CreateMap<ImpuestoProveedorDTO, ImpuestoProveedor>();
                if (item.Impuesto != null)
                {
                    objCP.Impuesto = item.Impuesto.CreateMap<ImpuestoDTO, Impuesto>();
                    objCP.Impuesto.TipoImpuesto = item.Impuesto.TipoImpuesto.CreateMap<TablaDTO, Tabla>();
                }
                objR.Impuestos.Add(objCP);
            }

            return objR;
        }

    }
}
