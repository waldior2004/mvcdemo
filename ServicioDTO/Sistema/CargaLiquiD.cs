using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(CargaLiquiCDTO))]
    public class CargaLiquiDDTO
    {
        public CargaLiquiDDTO()
        {

        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public CargaLiquiCDTO CargaLiquiC { get; set; }

        [DataMember]
        public string NumContenedores { get; set; }

        [DataMember]
        public string Booking { get; set; }

        [DataMember]
        public short Item { get; set; }

        [DataMember]
        public string Linea { get; set; }

        [DataMember]
        public string InDate { get; set; }

        [DataMember]
        public string OutDate { get; set; }

        [DataMember]
        public string Shipper { get; set; }

        [DataMember]
        public string Commodity { get; set; }

        [DataMember]
        public string Nave { get; set; }

        [DataMember]
        public string Viaje { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public decimal Total { get; set; }
    }
}
