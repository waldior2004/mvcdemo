using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(UsuarioDTO))]
    public class RolDTO
    {
        public RolDTO()
        {
            Usuarios = new List<UsuarioDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<UsuarioDTO> Usuarios { get; set; }
    }
}
