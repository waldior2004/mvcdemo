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
    [KnownType(typeof(BancoDTO))] 
    public class DatoComercialDTO
    {
        public DatoComercialDTO()
        {

        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdPais2 { get; set; }

        [DataMember]
        public virtual TablaDTO Pais { get; set; }

        [DataMember]
        public int IdProveedor { get; set; }

        [DataMember]
        public virtual ProveedorDTO Proveedor { get; set; }

        [DataMember]
        public int IdBanco { get; set; }

        [DataMember]
        public virtual BancoDTO Banco { get; set; }

        [DataMember]
        public int IdTipoCuenta { get; set; }

        [DataMember]
        public virtual TablaDTO TipoCuenta { get; set; }

        [DataMember]
        public int IdTipoInterlocutor { get; set; }

        [DataMember]
        public virtual TablaDTO TipoInterlocutor { get; set; }

        [DataMember]
        public string NroCuenta { get; set; }

        [DataMember]
        public string NroCCI { get; set; }

        [DataMember]
        public string Swift { get; set; }

        [DataMember]
        public DateTime? AudUpdate { get; set; }

        [DataMember]
        public Byte AudActivo { get; set; }

    }
}
