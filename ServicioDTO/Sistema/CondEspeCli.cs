using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(DocumentoDTO))]
    [KnownType(typeof(ClienteDTO))]
    [KnownType(typeof(CondEspeCliDetalleDTO))]
    public class CondEspeCliDTO
    {
        public CondEspeCliDTO()
        {
            this.Documentos = new List<DocumentoDTO>();
            this.Detalles = new List<CondEspeCliDetalleDTO>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public TablaDTO TipoCond { get; set; }

        [DataMember]
        public string IdReferencia { get; set; }

        [DataMember]
        public string DescReferencia { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public DateTime FechaInicio { get; set; }

        [DataMember]
        public DateTime FechaFin { get; set; }

        [DataMember]
        public DateTime FechaAprobacion { get; set; }

        [DataMember]
        public string UsuAprobacion { get; set; }

        [DataMember]
        public Int16 DiasLibres { get; set; }

        [DataMember]
        public virtual List<DocumentoDTO> Documentos { get; set; }

        [DataMember]
        public virtual List<CondEspeCliDetalleDTO> Detalles { get; set; }
    }
}
