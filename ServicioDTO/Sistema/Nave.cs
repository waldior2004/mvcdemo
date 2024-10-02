using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{

    [DataContract]
    public class NaveDTO
    {
        public NaveDTO()
        {
        }

        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

    }
}
