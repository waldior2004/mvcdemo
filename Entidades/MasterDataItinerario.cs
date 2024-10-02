using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities
{

    [DataContract]
    public class MasterDataItinerario
    {




        [Column("fechasal")]
        public DateTime fechasal { get; set; }

        [Column("fechalle")]
        public DateTime fechalle { get; set; }

        [Column("numviaje")]
        public string numviaje { get; set; }

        [Column("codigo")]
        public string codigo { get; set; }

        [Column("iditin")]
        public int iditin { get; set; }

        [Column("puerto_id")]
        public int puerto_id { get; set; }

        [Column("idbls")]
        public int idbls { get; set; }

        [Column("booking")]
        public string booking { get; set; }

        [Column("numbooking")]
        public string numbooking { get; set; }

        [Column("numbl")]
        public string numbl { get; set; }

        [Column("cnt")]
        public string cnt { get; set; }

        [Column("RucCliente")]
        public string RucCliente { get; set; }

        [Column("NombreCliente")]
        public string NombreCliente { get; set; }

        [Column("CodPartidaProducto")]
        public string CodPartidaProducto { get; set; }

        [Column("NombreProducto")]
        public string NombreProducto { get; set; }

        [Column("IdCliente")]
        public int? IdCliente { get; set; }

        [Column("NombreCommodity")]
        public string NombreCommodity { get; set; }


    }
}
