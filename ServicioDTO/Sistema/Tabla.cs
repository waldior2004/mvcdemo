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
    [KnownType(typeof(GrupoTablaDTO))]
    public class TablaDTO
    {
        public TablaDTO() {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public GrupoTablaDTO GrupoTabla { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public Int16 Orden { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Breve { get; set; }
    }
}
