using System;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class PuertoDTO
    {
        public PuertoDTO()
        {
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

    }
}
