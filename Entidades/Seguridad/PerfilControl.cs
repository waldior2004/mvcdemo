namespace com.msc.infraestructure.entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_PERFIL_CONTROL", Schema = "SEGURIDAD")]
    public class PerfilControl
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PERFIL")]
        public int IdPerfil { get; set; }

        [InverseProperty("PerfilControls")]
        [ForeignKey("IdPerfil")]
        public virtual Perfil Perfil { get; set; }

        [Column("ID_CONTROL")]
        public int IdControl { get; set; }

        [InverseProperty("PerfilControls")]
        [ForeignKey("IdControl")]
        public virtual Control Control { get; set; }

        [Required]
        public int Estado { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
