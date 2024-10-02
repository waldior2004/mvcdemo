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
    [KnownType(typeof(ProveedorDTO))]
    [KnownType(typeof(ImpuestoDTO))]
    public class ImpuestoProveedorDTO
    {
        public ImpuestoProveedorDTO()
        {
            Proveedor = new ProveedorDTO();
            Impuesto = new ImpuestoDTO();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdProveedor { get; set; }

        [DataMember]
        public virtual ProveedorDTO Proveedor { get; set; }

        [DataMember]
        public int IdImpuesto { get; set; }

        [DataMember]
        public virtual ImpuestoDTO Impuesto { get; set; }

        [DataMember]
        public DateTime? AudUpdate { get; set; }

        [DataMember]
        public Byte AudActivo { get; set; }
    }
}
