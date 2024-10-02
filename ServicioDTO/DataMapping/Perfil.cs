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
        public static PerfilDTO GetPerfilDTO(this Perfil source)
        {
            var objR = source.CreateMap<Perfil, PerfilDTO>();
            if (source.PerfilControls.Count > 0)
                foreach (var item in source.PerfilControls)
                {
                    objR.Controles.Add(new ControlDTO
                    {
                        IdPerfilControl = item.Id,
                        Id = item.IdControl,
                        Descripcion = item.Control.Descripcion,
                        Estado = item.Estado,
                        Url = item.Control.Url,
                        Pagina = item.Control.Pagina.CreateMap<Pagina, PaginaDTO>()
                    });
                }

            return objR;
        }

        public static Perfil SetPerfil(this PerfilDTO source)
        {
            var objR = source.CreateMap<PerfilDTO, Perfil>();
            if (source.Controles != null)
            {
                foreach (var item in source.Controles)
                {
                    var objI = new PerfilControl
                    {
                        Id = item.IdPerfilControl,
                        IdPerfil = source.Id,
                        IdControl = item.Id,
                        Estado = item.Estado,
                        Control = item.CreateMap<ControlDTO, Control>()
                    };
                    objI.Control.Pagina = item.Pagina.CreateMap<PaginaDTO, Pagina>();
                    objR.PerfilControls.Add(objI);
                }
            }
            return objR;
        }


    }
}
