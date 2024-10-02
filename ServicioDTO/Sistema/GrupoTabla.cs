using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(TablaDTO))]
    public class GrupoTablaDTO
    {
        public GrupoTablaDTO()
        {
            Tablas = new List<TablaDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<TablaDTO> Tablas { get; set; }
    }
}
