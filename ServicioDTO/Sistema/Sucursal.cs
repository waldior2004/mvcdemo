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
    public class SucursalDTO
    {
        public SucursalDTO() {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public EmpresaDTO Empresa { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }
    }
}
