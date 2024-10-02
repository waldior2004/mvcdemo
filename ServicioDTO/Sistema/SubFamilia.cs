using System.Runtime.Serialization;
using System.Collections.Generic;
namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(FamiliaDTO))]
    public class SubFamiliaDTO
    {
        public SubFamiliaDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public FamiliaDTO Familia { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }

    }
}
