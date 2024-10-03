using com.msc.infraestructure.entities;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static TipoControlDTO GetTipoControlDTO(this TipoControl source)
        {
            var objR = source.CreateMap<TipoControl, TipoControlDTO>();
            return objR;
        }

        public static TipoControl SetTipoControl(this TipoControlDTO source)
        {
            return source.CreateMap<TipoControlDTO, TipoControl>();
        }
    }
}
