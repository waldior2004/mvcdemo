using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(ControlDTO))]
    public class TipoControlDTO
    {
        public TipoControlDTO()
        {
            Controles = new List<ControlDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<ControlDTO> Controles { get; set; }
    }
}
