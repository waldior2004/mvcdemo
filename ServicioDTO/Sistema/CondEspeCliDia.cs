using System;
using System.Runtime.Serialization;
namespace com.msc.services.dto
{

    [DataContract]
    [KnownType(typeof(TablaDTO))]
    public class CondEspeCliDiaDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdCondEspeCliDetalle { get; set; }

        [DataMember]
        public TablaDTO Transporte { get; set; }

        [DataMember]
        public Int16 DiaI { get; set; }

        [DataMember]
        public Int16 DiaF { get; set; }

    }
}
