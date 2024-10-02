using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class TarifarioDocDTO
    {
        public TarifarioDocDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public int IdTarifario { get; set; }

    }
}
