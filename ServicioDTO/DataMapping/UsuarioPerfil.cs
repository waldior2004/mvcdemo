using com.msc.infraestructure.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static UsuarioPerfilDTO GetUsuarioPerfilDTO(this UsuarioPerfil source)
        {
            var objR = source.CreateMap<UsuarioPerfil, UsuarioPerfilDTO>();
            return objR;
        }
        public static UsuarioPerfil SetUsuarioPerfil(this UsuarioPerfilDTO source)
        {
            var objR = source.CreateMap<UsuarioPerfilDTO, UsuarioPerfil>();
            return objR;
        }
    }
}
