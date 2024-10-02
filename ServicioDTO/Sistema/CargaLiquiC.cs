using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(CargaLiquiDDTO))]
    [KnownType(typeof(DocumentoDTO))]
    [KnownType(typeof(NaveDTO))]
    [KnownType(typeof(ViajeDTO))]
    public class CargaLiquiCDTO
    {
        public CargaLiquiCDTO()
        {
            CargaLiquiDetalles = new List<CargaLiquiDDTO>();
        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public NaveDTO Nave { get; set; }

        [DataMember]
        public ViajeDTO Viaje { get; set; }

        [DataMember]
        public PuertoDTO Puerto { get; set; }

        [DataMember]
        public short TipoEnvio { get; set; }

        [DataMember]
        public DocumentoDTO Documento { get; set; }

        [DataMember]
        public DocumentoDTO Documento2 { get; set; }

        [DataMember]
        public TablaDTO Terminal { get; set; }

        [DataMember]
        public short Procesados { get; set; }

        [DataMember]
        public short Correctos { get; set; }

        [DataMember]
        public short Errados { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public DateTime FecRegistro { get; set; }

        [DataMember]
        public string Comentario { get; set; }

        [DataMember]
        public string Provision { get; set; }

        [DataMember]
        public DateTime? FecValida { get; set; }

        [DataMember]
        public List<CargaLiquiDDTO> CargaLiquiDetalles { get; set; }
    }
}
