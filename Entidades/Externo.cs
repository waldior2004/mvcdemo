namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_EXTERNO", Schema = "SISTEMA")]
    public class Externo
    {
        public Externo()
        {
            this.ExternoPerfils = new List<ExternoPerfil>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DESC_TERMINAL")]
        [MaxLength(200)]
        [Required]
        [DisplayName("Descripción Terminal")]
        public string DescTerminal { get; set; }

        [MaxLength(200)]
        [Required]
        [DisplayName("Contacto")]
        public string Contacto { get; set; }

        [MaxLength(11)]
        [Required]
        [DisplayName("RUC")]
        public string Ruc { get; set; }

        [MaxLength(20)]
        [Required]
        [DisplayName("Login")]
        public string Usuario { get; set; }

        [MaxLength(250)]
        [DisplayName("Clave")]
        public string Clave { get; set; }

        [EmailAddress]
        [DisplayName("Correo 1")]
        [Required]
        public string Email1 { get; set; }

        [EmailAddress]
        [DisplayName("Correo 2")]
        public string Email2 { get; set; }

        [Phone]
        [DisplayName("Teléfono 1")]
        public string Telefono1 { get; set; }

        [Phone]
        [DisplayName("Teléfono 2")]
        public string Telefono2 { get; set; }

        [DisplayName("Inicio sesión?")]
        [Column("INICIO_SESION", TypeName = "tinyint")]
        public Byte EsInicio { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<ExternoPerfil> ExternoPerfils { get; set; }
    }
}
