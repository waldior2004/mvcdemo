namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_IMPUESTO", Schema = "SISTEMA")]
    public class Impuesto
    {
        public Impuesto()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_TIPO_IMPUESTO")]
        [DisplayName("Tipo Impuesto")]
        [MyRequired]
        public int IdTipoImpuesto { get; set; }

        [ForeignKey("IdTipoImpuesto")]
        public virtual Tabla TipoImpuesto { get; set; }

        [MaxLength(10)]
        [Required]
        [Column("COD_RETEN")]
        [DisplayName("Código Retención")]
        public string Codigo { get; set; }

        [MaxLength(150)]
        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
