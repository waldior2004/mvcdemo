namespace com.msc.infraestructure.entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("T_EXTERNO_PERFIL", Schema = "SEGURIDAD")]
    public class ExternoPerfil
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_EXTERNO")]
        public int IdExterno { get; set; }

        [InverseProperty("ExternoPerfils")]
        [ForeignKey("IdExterno")]
        public virtual Externo Externo { get; set; }

        [Column("ID_PERFIL")]
        public int IdPerfil { get; set; }

        [InverseProperty("ExternoPerfils")]
        [ForeignKey("IdPerfil")]
        public virtual Perfil Perfil { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
