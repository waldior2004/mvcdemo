using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(TarifarioDTO))]
    [KnownType(typeof(ProductoDTO))]
    public class DetalleCotizacionDTO
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdCotizacion { get; set; }

        [DataMember]
        public int IdProducto { get; set; }

        [DataMember]
        public ProductoDTO Producto { get; set; }

        [DataMember]
        public int? IdTarifario { get; set; }

        [DataMember]
        public TarifarioDTO Tarifario { get; set; }

        [DataMember]
        public int Cantidad { get; set; }

        [DataMember]
        public decimal Precio { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public string Observacion { get; set; }

    }
}
