using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class TempoPedidoDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdPedido { get; set; }
        [DataMember]
        public int? IdTarifario { get; set; }
        [DataMember]
        public int IdProveedor { get; set; }
        [DataMember]
        public int IdProducto { get; set; }
        [DataMember]
        public int IdMoneda { get; set; }
        [DataMember]
        public decimal Precio { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int Cantidad { get; set; }
    }
}
