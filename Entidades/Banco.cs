namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_BANCO", Schema = "SISTEMA")]
    public class Banco
    {
        public Banco()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PAIS")]
        [DisplayName("Pais")]
        [MyRequired]
        public int IdPais { get; set; }

        [ForeignKey("IdPais")]
        public virtual Tabla Pais { get; set; }

        [MaxLength(6)]
        [Required]
        [DisplayName("Código")]
        public string Codigo { get; set; }

        [MaxLength(200)]
        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [MaxLength(250)]
        [Required]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [MaxLength(35)]
        [Required]
        [DisplayName("SWIFT")]
        public string Swift { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
