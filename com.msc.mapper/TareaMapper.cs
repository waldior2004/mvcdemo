using AutoMapper;
using com.msc.domain.entities.Sistema;
using com.msc.sqlserver.entities.Sistema;
using com.msc.wcf.entities.Sistema;

namespace com.msc.mapper
{
    public class TareaMapper : Profile
    {
        public TareaMapper()
        {
            CreateMap<Tarea, TareaADO>();
            CreateMap<TareaADO, Tarea>()
                .ConvertUsing(x => new Tarea(x.Id, x.Titulo, x.Descripcion, x.Completado));
            CreateMap<Tarea, TareaDTO>();
            CreateMap<TareaDTO, Tarea>()
                .ConvertUsing(x => new Tarea(x.Id, x.Titulo, x.Descripcion, x.Completado));
            CreateMap<TareaADO, TareaDTO>();
        }
    }
}
