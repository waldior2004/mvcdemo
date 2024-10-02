using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class PerfilControlDTO
    {
        public PerfilControlDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdPerfil { get; set; }
        [DataMember]
        public int IdControl { get; set; }
        [DataMember]
        public int Estado { get; set; }
    }
}
