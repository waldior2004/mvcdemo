using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace com.msc.infraestructure.entities.mvc
{
    public class Login
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Soy Usuario Externo")]
        public bool UserExternal { get; set; }
    }
}
