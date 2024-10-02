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
    public class ImpuestoDTO
    {
        public ImpuestoDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public TablaDTO TipoImpuesto { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
    }
}
