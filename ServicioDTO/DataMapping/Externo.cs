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
        public static ExternoDTO GetExternoDTO(this Externo source)
        {
            var objR = source.CreateMap<Externo, ExternoDTO>();

            if (source.ExternoPerfils.Count > 0)
                foreach (var item in source.ExternoPerfils)
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

        public static Externo SetExterno(this ExternoDTO source)
        {
            var objR = source.CreateMap<ExternoDTO, Externo>();

            if (source.Perfiles != null)
            {
                foreach (var item in source.Perfiles)
                {
                    var objI = new ExternoPerfil
                    {
                        Id = item.IdUsuarioPerfil,
                        IdExterno = source.Id,
                        IdPerfil = item.Id,
                        Perfil = item.CreateMap<PerfilDTO, Perfil>()
                    };
                    objR.ExternoPerfils.Add(objI);
                }
            }

            return objR;
        }
    }
}
