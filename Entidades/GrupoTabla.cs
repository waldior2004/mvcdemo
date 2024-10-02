namespace com.msc.infraestructure.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_GRUPO_TABLA", Schema = "SISTEMA")]
    public class GrupoTabla
    {
        public GrupoTabla()
        {
            this.Tablas = new List<Tabla>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(3)]
        [Required]
        public string Codigo { get; set; }

        [MaxLength(100)]
        [Required]
        public string Descripcion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<Tabla> Tablas { get; set; }
    }
}
