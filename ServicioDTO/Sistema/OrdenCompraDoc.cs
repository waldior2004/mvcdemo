using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class OrdenCompraDocDTO
    {
        public OrdenCompraDocDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public int IdOrdenCompra { get; set; }
    }
}
