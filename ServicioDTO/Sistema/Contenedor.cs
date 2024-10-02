using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{

    [DataContract]
    public class ContenedorDTO
    {
        public ContenedorDTO()
        {
        }

        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string IdViaje { get; set; }
        [DataMember]
        public string IdMovimiento { get; set; }
        [DataMember]
        public string IdPuerto { get; set; }

    }
}
