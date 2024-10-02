using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{

    [DataContract]
    public class CotizacionDTO
    {
        public CotizacionDTO()
        {
            DetalleCotizaciones = new List<DetalleCotizacionDTO>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public int IdPedido { get; set; }
        [DataMember]
        public PedidoDTO Pedido { get; set; }
        [DataMember]
        public int IdProveedor { get; set; }
        [DataMember]
        public ProveedorDTO Proveedor { get; set; }
        [DataMember]
        public int IdOrdenCompra { get; set; }
        [DataMember]
        public OrdenCompraDTO OrdenCompra { get; set; }
        [DataMember]
        public int IdEstado { get; set; }
        [DataMember]
        public TablaDTO Estado { get; set; }
        [DataMember]
        public DateTime FechaCotizacion { get; set; }
        [DataMember]
        public DateTime? FechaEntrega { get; set; }
        [DataMember]
        public string Observacion { get; set; }
        [DataMember]
        public virtual List<DetalleCotizacionDTO> DetalleCotizaciones { get; set; }
    }
}
