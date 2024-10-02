using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class TarifaCEDocDTO
    {
        public TarifaCEDocDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public int IdTarifaCE { get; set; }
    }
}
