using com.msc.infraestructure.entities;
using System;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static ControlDTO GetControlDTO(this Control source)
        {
            var objR = source.CreateMap<Control, ControlDTO>();

            if (source.Pagina != null)
                objR.Pagina = source.Pagina.CreateMap<Pagina, PaginaDTO>();
            else
                objR.Pagina = new PaginaDTO { Id = Convert.ToInt32(source.IdPagina) };

            if (source.TipoControl != null)
                objR.TipoControl = source.TipoControl.CreateMap<TipoControl, TipoControlDTO>();
            else
                objR.TipoControl = new TipoControlDTO { Id = Convert.ToInt32(source.IdTipoControl) };

            return objR;
        }

        public static Control SetControl(this ControlDTO source)
        {
            var objR = source.CreateMap<ControlDTO, Control>();
            if (source.Pagina != null)
            {
                objR.IdPagina = source.Pagina.Id;
                objR.Pagina = source.Pagina.CreateMap<PaginaDTO, Pagina>();
            }

            if (source.TipoControl != null)
            {
                objR.IdTipoControl = source.TipoControl.Id;
                objR.TipoControl = source.TipoControl.CreateMap<TipoControlDTO, TipoControl>();
            }

            return objR;
        }
    }
}
