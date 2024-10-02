using System.Runtime.Serialization;
using System.Collections.Generic;

namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(ProductoDTO))]
    public class FamiliaDTO
    {
        public FamiliaDTO()
        {

        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }

    }
}
