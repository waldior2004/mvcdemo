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
    public class ProvisionDTO
    {
        public ProvisionDTO() {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public EmpresaDTO Empresa { get; set; }
        [DataMember]
        public SucursalDTO Sucursal { get; set; }
        [DataMember]
        public TablaDTO TipoProvision { get; set; }
        [DataMember]
        public TablaDTO CuentaContable { get; set; }
        [DataMember]
        public ProveedorDTO Proveedor { get; set; }
        [DataMember]
        public TablaDTO Moneda { get; set; }
        [DataMember]
        public TablaDTO Estado { get; set; }
        [DataMember]
        public OrdenCompraDTO OrdenCompra { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public decimal Monto { get; set; }
        [DataMember]
        public string Concepto { get; set; }
        [DataMember]
        public int MesProv { get; set; }
        [DataMember]
        public int AnioProv { get; set; }
        [DataMember]
        public int MesServ { get; set; }
        [DataMember]
        public int AnioServ { get; set; }
        [DataMember]
        public string ComentarioUno { get; set; }
        [DataMember]
        public string ComentarioDos { get; set; }
    }
}
