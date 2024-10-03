using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    public class LoginDTO
    {
        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool UserExternal { get; set; }
    }
}
