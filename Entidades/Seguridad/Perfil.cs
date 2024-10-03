namespace com.msc.infraestructure.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("T_PERFIL", Schema = "SEGURIDAD")]
    public class Perfil
    {
        public Perfil()
        {
            this.PerfilControls = new List<PerfilControl>();
            this.UsuarioPerfils = new List<UsuarioPerfil>();
            this.ExternoPerfils = new List<ExternoPerfil>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string Descripcion { get; set; }

        [Column("MENU_SUP")]
        public string MenuSup { get; set; }

        [Column("PERMISOS", TypeName = "xml")]
        public string Permisos { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<PerfilControl> PerfilControls { get; set; }
        public virtual List<UsuarioPerfil> UsuarioPerfils { get; set; }
        public virtual List<ExternoPerfil> ExternoPerfils { get; set; }
    }
}
