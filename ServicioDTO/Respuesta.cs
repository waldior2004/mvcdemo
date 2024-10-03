using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class RespuestaDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Aplicacion { get; set; }
        [DataMember]
        public string Metodo { get; set; }
        [DataMember]
        public string TipoError { get; set; }
        [DataMember]
        public string PilaError { get; set; }
    }
}
