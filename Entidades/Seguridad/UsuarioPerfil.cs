namespace com.msc.infraestructure.entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("T_USUARIO_PERFIL", Schema = "SEGURIDAD")]
    public class UsuarioPerfil
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [InverseProperty("UsuarioPerfils")]
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        [Column("ID_PERFIL")]
        public int IdPerfil { get; set; }

        [InverseProperty("UsuarioPerfils")]
        [ForeignKey("IdPerfil")]
        public virtual Perfil Perfil { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
