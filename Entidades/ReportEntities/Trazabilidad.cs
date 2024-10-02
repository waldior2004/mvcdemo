using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities.reportes
{
    [DataContract]
    public class Trazabilidad
    {
        [DataMember]
        public string Empresa { get; set; }
        [DataMember]
        public string Sucursal { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public string Tipo_Peticion { get; set; }
        [DataMember]
        public string Estado_TipoP { get; set; }
        [DataMember]
        public string CC { get; set; }
        [DataMember]
        public string Cotizacion { get; set; }
        [DataMember]
        public string Fecha_Cotizacion { get; set; }
        [DataMember]
        public string Provee_Coti { get; set; }
        [DataMember]
        public string Estado_Cotiz { get; set; }
        [DataMember]
        public string OrdenCompra { get; set; }
        [DataMember]
        public string Fecha_Registro { get; set; }
        [DataMember]
        public string Num_Factura { get; set; }
        [DataMember]
        public decimal SubTotal { get; set; }
        [DataMember]
        public decimal Igv { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public string Provee_OC { get; set; }
        [DataMember]
        public string Provision { get; set; }
        [DataMember]
        public decimal Monto_Provi { get; set; }
    }
}
