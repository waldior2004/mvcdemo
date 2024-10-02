using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities.reportes
{
    [DataContract]
    public class pEnergiaConten
    {
        [DataMember]
        public string IdNave { get; set; }
        [DataMember]
        public string IdPuerto { get; set; }
        [DataMember]
        public string IdViaje { get; set; }
        [DataMember]
        public string FecIniApro { get; set; }
        [DataMember]
        public string FecFinApro { get; set; }
        [DataMember]
        public string FecIniZarpe { get; set; }
        [DataMember]
        public string FecFinZarpe { get; set; }
    }
}
