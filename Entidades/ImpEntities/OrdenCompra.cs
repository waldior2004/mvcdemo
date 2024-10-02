using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities.impresion
{
    [DataContract]
    public class IMP_ORDENCOMPRA
    {
        [DataMember]
        public string EMPRESA { get; set; }
        [DataMember]
        public string TELEFONO { get; set; }
        [DataMember]
        public string RUC { get; set; }
        [DataMember]
        public string DIRECCION { get; set; }
        [DataMember]
        public string CODIGO { get; set; }
        [DataMember]
        public string FECHAREGISTRO { get; set; }
        [DataMember]
        public string RAZON_SOCIAL { get; set; }
        [DataMember]
        public int CANTIDAD { get; set; }
        [DataMember]
        public string PRODUCTO { get; set; }
        [DataMember]
        public decimal PRECIO { get; set; }
        [DataMember]
        public decimal TOTAL { get; set; }
        [DataMember]
        public string CENTROCOSTO { get; set; }
        [DataMember]
        public string OCREG { get; set; }
        [DataMember]
        public string OCAPROB { get; set; }
        [DataMember]
        public string PEDUSER { get; set; }
        [DataMember]
        public decimal SUBTOTAL { get; set; }
        [DataMember]
        public decimal IGV { get; set; }
        [DataMember]
        public decimal TOTALTODO { get; set; }
        [DataMember]
        public string TOTAL_LETRAS { get; set; }
    }
}
