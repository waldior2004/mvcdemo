using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class UsuarioPerfilDTO
    {
        public UsuarioPerfilDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public int IdPerfil { get; set; }
    }
}
