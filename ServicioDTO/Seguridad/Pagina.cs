using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(ControlDTO))]
    public class PaginaDTO
    {
        public PaginaDTO()
        {
            Controles = new List<ControlDTO>();
            Hijas = new List<PaginaDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public PaginaDTO PaginaPadre { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Int16? Orden { get; set; }
        [DataMember]
        public List<ControlDTO> Controles { get; set; }
        [DataMember]
        public List<PaginaDTO> Hijas { get; set; }
    }
}
