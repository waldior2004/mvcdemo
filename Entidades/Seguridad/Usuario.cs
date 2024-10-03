namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("T_USUARIO", Schema = "SEGURIDAD")]
    public class Usuario
    {
        public Usuario()
        {
            this.UsuarioPerfils = new List<UsuarioPerfil>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_ROL")]
        [DisplayName("Rol")]
        [MyRequired]
        public int IdRol { get; set; }

        [InverseProperty("Usuarios")]
        [ForeignKey("IdRol")]
        public virtual Rol Rol { get; set; }

        [MaxLength(30)]
        [Required]
        public string Login { get; set; }

        [MaxLength(500)]
        public string Clave { get; set; }

        [Compare("Clave",ErrorMessage = "Las claves no coinciden")]
        [NotMapped]
        public string Compare { get; set; }

        [Column("ULTIMO_ACCESO")]
        public DateTime? UltimoAcceso { get; set; }

        [MaxLength(100)]
        [Required]
        public string Descripcion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<UsuarioPerfil> UsuarioPerfils { get; set; }
    }
}
