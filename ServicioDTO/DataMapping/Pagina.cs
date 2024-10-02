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
        public static PaginaDTO GetPaginaDTO(this Pagina source)
        {
            var objR = source.CreateMap<Pagina, PaginaDTO>();
            if (source.PaginaPadre != null)
                objR.PaginaPadre = source.PaginaPadre.CreateMap<Pagina, PaginaDTO>();
            else if (source.IdPagina != null)
                objR.PaginaPadre = new PaginaDTO { Id = Convert.ToInt32(source.IdPagina) };
            foreach (var item in source.Hijas)
            {
                objR.Hijas.Add(item.CreateMap<Pagina, PaginaDTO>());
            }
            return objR;
        }

        public static Pagina SetPagina(this PaginaDTO source)
        {
            var objR = source.CreateMap<PaginaDTO, Pagina>();
            if (source.PaginaPadre != null)
            {
                objR.IdPagina = source.PaginaPadre.Id;
                objR.PaginaPadre = source.PaginaPadre.CreateMap<PaginaDTO, Pagina>();
            }

            foreach (var item in source.Hijas)
            {
                objR.Hijas.Add(item.CreateMap<PaginaDTO, Pagina>());
            }
            return objR;
        }
    }
}
