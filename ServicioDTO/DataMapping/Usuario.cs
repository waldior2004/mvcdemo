using com.msc.infraestructure.entities;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static UsuarioDTO GetUsuarioDTO(this Usuario source)
        {
            var objR = source.CreateMap<Usuario, UsuarioDTO>();

            if (source.Rol != null)
                objR.Rol = source.Rol.CreateMap<Rol, RolDTO>();
            else
                objR.Rol = new RolDTO { Id = source.IdRol };

            if (source.UsuarioPerfils.Count > 0)
                foreach (var item in source.UsuarioPerfils)
                {
                    objR.Perfiles.Add(new PerfilDTO
                    {
                        IdUsuarioPerfil = item.Id,
                        Id = item.IdPerfil,
                        Descripcion = item.Perfil.Descripcion,
                        MenuSup = item.Perfil.MenuSup,
                        Permisos = item.Perfil.Permisos
                    });
                }

            return objR;
        }

        public static Usuario SetUsuario(this UsuarioDTO source)
        {
            var objR = source.CreateMap<UsuarioDTO, Usuario>();
            if (source.Rol != null)
            {
                objR.IdRol = source.Rol.Id;
                objR.Rol = source.Rol.CreateMap<RolDTO, Rol>();
            }
            if (source.Perfiles != null)
            {
                foreach (var item in source.Perfiles)
                {
                    var objI = new UsuarioPerfil
                    {
                        Id = item.IdUsuarioPerfil,
                        IdUsuario = source.Id,
                        IdPerfil = item.Id,
                        Perfil = item.CreateMap<PerfilDTO, Perfil>()
                    };
                    objR.UsuarioPerfils.Add(objI);
                }
            }
            return objR;
        }
    }
}
