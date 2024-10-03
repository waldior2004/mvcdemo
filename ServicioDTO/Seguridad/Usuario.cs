using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(RolDTO))]
    [KnownType(typeof(PerfilDTO))]
    public class UsuarioDTO
    {
        public UsuarioDTO()
        {
            Perfiles = new List<PerfilDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public RolDTO Rol { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string Compare { get; set; }
        [DataMember]
        public DateTime? UltimoAcceso { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<PerfilDTO> Perfiles { get; set; }
    }
}
