using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class DocumentoDTO
    {
        public DocumentoDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTarifaCEDoc { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string RutaLocal { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public decimal TamanoMB { get; set; }
        [DataMember]
        public string Guid { get; set; }

    }
}