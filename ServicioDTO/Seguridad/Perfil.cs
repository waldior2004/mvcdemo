using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(UsuarioDTO))]
    [KnownType(typeof(ControlDTO))]
    public class PerfilDTO
    {
        public PerfilDTO()
        {
            Usuarios = new List<UsuarioDTO>();
            Controles = new List<ControlDTO>();
            Externos = new List<ExternoDTO>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdUsuarioPerfil { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string MenuSup { get; set; }
        [DataMember]
        public string Permisos { get; set; }
        [DataMember]
        public List<UsuarioDTO> Usuarios { get; set; }
        [DataMember]
        public List<ControlDTO> Controles { get; set; }
        [DataMember]
        public List<ExternoDTO> Externos { get; set; }
    }
}
