using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{

    [DataContract]
    public class ClienteDTO
    {
        public ClienteDTO()
        {
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

    }
}
