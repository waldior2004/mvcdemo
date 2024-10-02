using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(TablaDTO))]
    [KnownType(typeof(CondEspeCliDiaDTO))]
    public class CondEspeCliDetalleDTO
    {
        public CondEspeCliDetalleDTO()
        {
            this.CondEspeDias = new List<CondEspeCliDiaDTO>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdCondEspeCli { get; set; }

        [DataMember]
        public TablaDTO Terminal { get; set; }

        [DataMember]
        public TablaDTO RetiraPor { get; set; }

        [DataMember]
        public Int16 Dias { get; set; }

        [DataMember]
        public List<CondEspeCliDiaDTO> CondEspeDias { get; set; }
    }
}
