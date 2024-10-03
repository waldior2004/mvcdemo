namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_TAREA", Schema = "SISTEMA")]
    public class Tarea
    {
        public Tarea()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        [DisplayName("Titulo")]
        public string Titulo { get; set; }

        [MaxLength(200)]
        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [DisplayName("Completado")]
        public bool Completado { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
