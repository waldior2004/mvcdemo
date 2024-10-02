namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_TABLA", Schema = "SISTEMA")]
    public class Tabla
    {
        public Tabla()
        {
            //this.TiposServicio = new List<Producto>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_GRUPO_TABLA")]
        [DisplayName("Grupo Tabla")]
        [MyRequired]
        public int IdGrupoTabla { get; set; }

        [InverseProperty("Tablas")]
        [ForeignKey("IdGrupoTabla")]
        public virtual GrupoTabla GrupoTabla { get; set; }

        [MaxLength(8)]
        [Required]
        [DisplayName("Código")]
        public string Codigo { get; set; }

        [Required]
        [DisplayName("Orden")]
        public Int16 Orden { get; set; }

        [MaxLength(100)]
        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [MaxLength(15)]
        [DisplayName("Abreviación")]
        public string Breve { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        //public virtual List<Producto> TiposServicio { get; set; }
    }
}
