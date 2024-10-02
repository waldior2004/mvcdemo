using System;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{

    [DataContract]
    public class ViajeDTO
    {
        public ViajeDTO()
        {
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public DateTime FechaZarpe { get; set; }

        [DataMember]
        public DateTime FechaETA { get; set; }

        [DataMember]
        public NaveDTO Nave { get; set; }

        [DataMember]
        public PuertoDTO Puerto { get; set; }

    }
}
