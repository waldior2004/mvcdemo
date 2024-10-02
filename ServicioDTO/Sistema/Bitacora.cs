using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    public class BitacoraDTO
    {
        public BitacoraDTO()
        {

        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Tabla { get; set; }

        [DataMember]
        public int IdInterno { get; set; }

        [DataMember]
        public string Accion { get; set; }

        [DataMember]
        public string Detalle { get; set; }

        [DataMember]
        public string Tipo { get; set; }

        [DataMember]
        public DateTime FecRegistro { get; set; }

    }
}
