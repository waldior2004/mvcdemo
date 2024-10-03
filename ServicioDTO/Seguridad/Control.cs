using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(PaginaDTO))]
    [KnownType(typeof(TipoControlDTO))]
    [KnownType(typeof(PerfilDTO))]
    public class ControlDTO
    {
        public ControlDTO()
        {
            Perfiles = new List<PerfilDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdPerfilControl { get; set; }
        [DataMember]
        public TipoControlDTO TipoControl { get; set; }
        [DataMember]
        public PaginaDTO Pagina { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int Estado { get; set; }
        [DataMember]
        public List<PerfilDTO> Perfiles { get; set; }
    }
}
