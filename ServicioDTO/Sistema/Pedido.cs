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
    [KnownType(typeof(SucursalDTO))]
    [KnownType(typeof(AreaSolicitanteDTO))]
    [KnownType(typeof(TablaDTO))]
    [KnownType(typeof(CentroCostoDTO))]
    [KnownType(typeof(DetallePedidoDTO))]
    [KnownType(typeof(CotizacionDTO))]
    public class PedidoDTO
    {
        public PedidoDTO() {
            DetallePedidos = new List<DetallePedidoDTO>();
            Cotizaciones = new List<CotizacionDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public EmpresaDTO Empresa { get; set; }
        [DataMember]
        public SucursalDTO Sucursal { get; set; }
        [DataMember]
        public AreaSolicitanteDTO AreaSolicitante { get; set; }
        [DataMember]
        public TablaDTO TipoPeticion { get; set; }
        [DataMember]
        public TablaDTO Estado { get; set; }
        [DataMember]
        public CentroCostoDTO CentroCosto { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public DateTime FechaPeticion { get; set; }
        [DataMember]
        public DateTime FechaEsperada { get; set; }
        [DataMember]
        public string Observaciones { get; set; }
        [DataMember]
        public string UsuarioSol { get; set; }
        [DataMember]
        public string ComentAprobador { get; set; }
        [DataMember]
        public Byte FlagAprobacion { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public int IdOrdenCompra { get; set; }
        [DataMember]
        public OrdenCompraDTO OrdenCompra { get; set; }
        [DataMember]
        public List<DetallePedidoDTO> DetallePedidos { get; set; }
        [DataMember]
        public List<CotizacionDTO> Cotizaciones { get; set; }
    }
}
