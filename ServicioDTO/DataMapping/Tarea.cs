using com.msc.infraestructure.entities;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static TareaDTO GetTareaDTO(this Tarea source)
        {
            var objR = source.CreateMap<Tarea, TareaDTO>();

            return objR;
        }

        public static Tarea SetTarea(this TareaDTO source)
        {
            var objR = source.CreateMap<TareaDTO, Tarea>();

            return objR;
        }
    }
}
