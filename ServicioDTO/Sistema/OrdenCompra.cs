using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{

    [DataContract]
    [KnownType(typeof(CotizacionDTO))]
    [KnownType(typeof(ProveedorDTO))]
    [KnownType(typeof(TablaDTO))]
    public class OrdenCompraDTO
    {
        public OrdenCompraDTO()
        {
            DetalleOrdenCompras = new List<DetalleOrdenCompraDTO>();
            this.Documentos = new List<DocumentoDTO>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public int IdCotizacion { get; set; }
        [DataMember]
        public CotizacionDTO Cotizacion { get; set; }
        [DataMember]
        public int IdProveedor { get; set; }
        [DataMember]
        public ProveedorDTO Proveedor { get; set; }
        [DataMember]
        public int IdEstado { get; set; }
        [DataMember]
        public TablaDTO Estado { get; set; }
        [DataMember]
        public DateTime FechaRegistro { get; set; }
        [DataMember]
        public string ComentAprobador { get; set; }
        [DataMember]
        public string Observacion { get; set; }
        [DataMember]
        public Byte FlagAprobacion { get; set; }
        [DataMember]
        public string NumFactura { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string UserAprob { get; set; }
        [DataMember]
        public decimal SubTotal { get; set; }
        [DataMember]
        public decimal Igv { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public virtual List<DetalleOrdenCompraDTO> DetalleOrdenCompras { get; set; }
        [DataMember]
        public virtual List<DocumentoDTO> Documentos { get; set; }
    }
}
