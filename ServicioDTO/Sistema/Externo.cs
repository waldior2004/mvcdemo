using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    public class ExternoDTO
    {
        public ExternoDTO()
        {
            Perfiles = new List<PerfilDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string DescTerminal { get; set; }
        [DataMember]
        public string Contacto { get; set; }
        [DataMember]
        public string Ruc { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string Email1 { get; set; }
        [DataMember]
        public string Email2 { get; set; }
        [DataMember]
        public string Telefono1 { get; set; }
        [DataMember]
        public string Telefono2 { get; set; }
        [DataMember]
        public Byte EsInicio { get; set; }
        [DataMember]
        public List<PerfilDTO> Perfiles { get; set; }

    }
}
