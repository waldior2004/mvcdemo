using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(TablaDTO))]
    public class UbigeoDTO
    {
        public UbigeoDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public TablaDTO Pais { get; set; }
        [DataMember]
        public string CodDepartamento { get; set; }
        [DataMember]
        public string CodProvincia { get; set; }
        [DataMember]
        public string CodDistrito { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
    }
}
