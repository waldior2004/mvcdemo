namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_BITACORA", Schema = "SISTEMA")]
    public class Bitacora
    {
        public Bitacora()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string User { get; set; }

        [MaxLength(100)]
        [Required]
        public string Tabla { get; set; }

        [Column("ID_INTERNO")]
        [MyRequired]
        public int IdInterno { get; set; }

        [MaxLength(50)]
        [Required]
        public string Accion { get; set; }

        [Required]
        public string Detalle { get; set; }

        [MaxLength(25)]
        [Required]
        public string Tipo { get; set; }

        [Column("AUD_FECREG")]
        public DateTime FecRegistro { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
