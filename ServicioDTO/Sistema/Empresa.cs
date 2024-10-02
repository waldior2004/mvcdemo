using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    public class EmpresaDTO
    {
        public EmpresaDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Ruc { get; set; }
    }
}
