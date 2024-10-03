namespace com.msc.infraestructure.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_PAGINA", Schema = "SEGURIDAD")]
    public class Pagina
    {
        public Pagina()
        {
            this.Controls = new List<Control>();
            this.Hijas = new List<Pagina>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PAGINA")]
        [DisplayName("Pag. Padre")]
        public int? IdPagina { get; set; }

        [InverseProperty("Hijas")]
        [ForeignKey("IdPagina")]
        public virtual Pagina PaginaPadre { get; set; }

        [MaxLength(100, ErrorMessage = "El campo Título tiene máximo 100 caracteres")]
        [Required]
        [DisplayName("Título")]
        public string Titulo { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Url tiene máximo 50 caracteres")]
        [Required]
        [DisplayName("Url")]
        public string Url { get; set; }

        [RegularExpression("^([0-9]+)$", ErrorMessage = "El Orden de menú debe ser un entero positivo")]
        [DisplayName("Orden Menú")]
        public Int16? Orden { get; set; }

        [MaxLength(150, ErrorMessage = "El campo Descripción tiene máximo 150 caracteres")]
        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<Control> Controls { get; set; }

        public virtual List<Pagina> Hijas { get; set; }
    }
}
