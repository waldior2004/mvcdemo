using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class CondEspeCliDocDTO
    {
        public CondEspeCliDocDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public int IdCondEspeCli { get; set; }
    }
}
