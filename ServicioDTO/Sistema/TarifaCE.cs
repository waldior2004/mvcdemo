using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(TablaDTO))]
    [KnownType(typeof(ClienteDTO))]
    [KnownType(typeof(DocumentoDTO))]
    public class TarifaCEDTO
    {
        public TarifaCEDTO()
        {
            this.Documentos = new List<DocumentoDTO>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public TablaDTO Terminal { get; set; }

        [DataMember]
        public TablaDTO PerTarifa { get; set; }

        [DataMember]
        public TablaDTO Moneda { get; set; }

        [DataMember]
        public TablaDTO Estado { get; set; }

        [DataMember]
        public decimal Importe { get; set; }

        [DataMember]
        public DateTime FechaInicio { get; set; }

        [DataMember]
        public DateTime FechaFin { get; set; }

        [DataMember]
        public string Comentarios { get; set; }

        [DataMember]
        public List<DocumentoDTO> Documentos { get; set; }
    }
}
