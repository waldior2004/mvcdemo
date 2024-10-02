using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class ExternoPerfilDTO
    {
        public ExternoPerfilDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdExterno { get; set; }
        [DataMember]
        public int IdPerfil { get; set; }
    }
}
