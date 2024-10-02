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
    [KnownType(typeof(TablaDTO))]
    public class ContactoProveedorDTO
    {
        public ContactoProveedorDTO()
        {
            Proveedor = new ProveedorDTO();
            Cargo = new TablaDTO();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdProveedor { get; set; }

        [DataMember]
        public virtual ProveedorDTO Proveedor { get; set; }


        [DataMember]
        public int IdCargo { get; set; }

        [DataMember]
        public virtual TablaDTO Cargo { get; set; }

        [DataMember]
        public string NombreCompleto { get; set; }

        [DataMember]
        public string Correo { get; set; }

        [DataMember]
        public Byte IndContacto { get; set; }

        [DataMember]
        public DateTime? AudInsert { get; set; }

        [DataMember]
        public DateTime? AudUpdate { get; set; }

        [DataMember]
        public Byte AudActivo { get; set; }
    }
}
