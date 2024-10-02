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
    public class AreaSolicitanteDTO
    {
        public AreaSolicitanteDTO() {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public EmpresaDTO Empresa { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }
        [DataMember]
        public string CorreoRep { get; set; }
    }
}
