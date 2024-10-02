namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_CONTROL", Schema = "SEGURIDAD")]
    public class Control
    {
        public Control()
        {
            this.PerfilControls = new List<PerfilControl>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PAGINA")]
        [DisplayName("Página")]
        [MyRequired]
        public int IdPagina { get; set; }
   
        [InverseProperty("Controls")]
        [ForeignKey("IdPagina")]
        public virtual Pagina Pagina { get; set; }

        [Column("ID_TIPO_CONTROL")]
        [DisplayName("Tipo de Control")]
        [MyRequired]
        public int IdTipoControl { get; set; }

        [InverseProperty("Controls")]
        [ForeignKey("IdTipoControl")]
        public virtual TipoControl TipoControl { get; set; }

        [MaxLength(80)]
        [Required]
        [DisplayName("Url")]
        public string Url { get; set; }

        [MaxLength(150)]
        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<PerfilControl> PerfilControls { get; set; }
    }
}
