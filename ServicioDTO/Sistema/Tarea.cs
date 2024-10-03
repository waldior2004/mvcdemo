using System;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    public class TareaDTO
    {
        public TareaDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Completado { get; set; }
    }
}
