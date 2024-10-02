
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(TablaDTO))]
    [KnownType(typeof(DatoComercialDTO))]
    [KnownType(typeof(ContactoProveedorDTO))]
    [KnownType(typeof(TarifarioDTO))]
    public class ProveedorDTO
    {
        public ProveedorDTO()
        {
            DatoComercial = new List<DatoComercialDTO>();
            ContactoProveedor = new List<ContactoProveedorDTO>();
            Tarifarios = new List<TarifarioDTO>();
            Impuestos = new List<ImpuestoProveedorDTO>();
        }

        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public virtual TablaDTO TipoContribuyente { get; set; }

        [DataMember]
        public virtual TablaDTO TipoPersona { get; set; }

        [DataMember]
        public string Ruc { get; set; }

        [DataMember]
        public string RazonSocial { get; set; }

        [DataMember]
        public string NombreComercial { get; set; }

        [DataMember]
        public string ApellidoPaterno { get; set; }

        [DataMember]
        public string ApellidoMaterno { get; set; }

        [DataMember]
        public string Nombres { get; set; }

        [DataMember]
        public TablaDTO Pais { get; set; }

        [DataMember]
        public UbigeoDTO Departamento { get; set; }

        [DataMember]
        public UbigeoDTO Provincia { get; set; }

        [DataMember]
        public UbigeoDTO Distrito { get; set; }

        [DataMember]
        public string DireccionPrincipal { get; set; }

        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string RepresentanteLegal { get; set; }

        [DataMember]
        public virtual TablaDTO GiroNegocio { get; set; }

        [DataMember]
        public string CodigoSAP { get; set; }

        [DataMember]
        public virtual TablaDTO TipoNIF { get; set; }

        [DataMember]
        public virtual TablaDTO FormaCobro { get; set; }

        [DataMember]
        public DateTime? AudInsert { get; set; }

        [DataMember]
        public DateTime? AudUpdate { get; set; }

        [DataMember]
        public Byte AudActivo { get; set; }

        [DataMember]
        public List<DatoComercialDTO> DatoComercial { get; set; }

        [DataMember]
        public List<ContactoProveedorDTO> ContactoProveedor { get; set; }

        [DataMember]
        public List<TarifarioDTO> Tarifarios { get; set; }

        [DataMember]
        public List<ImpuestoProveedorDTO> Impuestos { get; set; }
    }
}
