using System.Runtime.Serialization;
using System.Collections.Generic;
namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(ProductoDTO))]
    public class UnidadMedidaDTO
    {
        public UnidadMedidaDTO()
        {
            Productos = new List<ProductoDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }

        public List<ProductoDTO> Productos { get; set; }
    }
}
