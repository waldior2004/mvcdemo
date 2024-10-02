namespace com.msc.infraestructure.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_DOCUMENTO", Schema = "SISTEMA")]
    public class Documento
    {
        public Documento()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(400)]
        [Required]
        public string Nombre { get; set; }

        [MaxLength(400)]
        [Required]
        [Column("RUTA_LOCAL")]
        public string RutaLocal { get; set; }

        [MaxLength(10)]
        [Required]
        public string Extension { get; set; }

        [Required]
        [Column("TAMANO_MB")]
        public decimal TamanoMB { get; set; }

        [Required]
        public string Guid { get; set; }

        [Required]
        public string Type { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
