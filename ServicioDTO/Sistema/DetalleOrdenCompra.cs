using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(ProductoDTO))]
    public class DetalleOrdenCompraDTO
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdOrdenCompra { get; set; }

        [DataMember]
        public int IdProducto { get; set; }

        [DataMember]
        public ProductoDTO Producto { get; set; }

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
