namespace com.msc.infraestructure.entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("V_CONTENEDOR", Schema = "SISTEMA")]
    public class Contenedor
    {
        public Contenedor()
        {

        }

        [Column("CODIGO")]
        [Required]
        public string Id { get; set; }

        [Column("IDVIAJE")]
        [Required]
        public string IdViaje { get; set; }

        [Column("IMPEXP")]
        [Required]
        public string IdMovimiento { get; set; }

        [Column("PUERTO")]
        [Required]
        public string IdPuerto { get; set; }

    }
}
