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
    public class DetallePedidoDTO
    {
        public DetallePedidoDTO()
        {

        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdPedido { get; set; }

        [DataMember]
        public virtual PedidoDTO Pedido { get; set; }

        [DataMember]
        public int IdProducto { get; set; }

        [DataMember]
        public virtual ProductoDTO Producto { get; set; }

        [DataMember]
        public int Cantidad { get; set; }

        [DataMember]
        public decimal Precio { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public string Observaciones { get; set; }

    }
}
