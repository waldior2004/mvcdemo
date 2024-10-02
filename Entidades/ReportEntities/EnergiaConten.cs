using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities.reportes
{
    [DataContract]
    public class EnergiaConten
    {
        [DataMember]
        public long Item { get; set; }
        [DataMember]
        public string Mn { get; set; }
        [DataMember]
        public string Contenedor { get; set; }
        [DataMember]
        public string Booking { get; set; }
        [DataMember]
        public string Linea { get; set; }
        [DataMember]
        public string Cliente { get; set; }
        [DataMember]
        public string TipoCarga { get; set; }
        [DataMember]
        public string FechaEntrada { get; set; }
        [DataMember]
        public string FechaSalida { get; set; }
        [DataMember]
        public short HorasTotal { get; set; }
        [DataMember]
        public short HorasReal { get; set; }
        [DataMember]
        public short HorasExcep { get; set; }
        [DataMember]
        public short HorasFinal { get; set; }
        [DataMember]
        public decimal Total { get; set; }


        [DataMember]
        public decimal Horasgvn { get; set; }
        [DataMember]
        public decimal Horasmsc { get; set; }


        [DataMember]
        public string Nave { get; set; }

        [DataMember]
        public string Viaje { get; set; }
    }
}
