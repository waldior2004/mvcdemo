namespace com.msc.infraestructure.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("T_TIPO_CONTROL", Schema = "SEGURIDAD")]
    public class TipoControl
    {
        public TipoControl()
        {
            this.Controls = new List<Control>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo Descripción tiene máximo 100 caracteres")]
        [DisplayName("Descripción")]
        [Required]
        public string Descripcion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<Control> Controls { get; set; }
    }
}
