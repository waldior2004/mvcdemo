namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_COND_ESP_CLI_DETALLE", Schema = "SISTEMA")]
    public class CondEspeCliDetalle
    {
        public CondEspeCliDetalle()
        {
            this.CondEspeCliDias = new List<CondEspeCliDia>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_COND_ESP_CLI")]
        public int IdCondEspeCli { get; set; }

        [InverseProperty("CondEspeCliDetalles")]
        [ForeignKey("IdCondEspeCli")]
        public virtual CondEspeCli CondEspeCli { get; set; }

        [MyRequired]
        [Column("ID_TERMINAL")]
        public int IdTerminal { get; set; }

        [ForeignKey("IdTerminal")]
        public virtual Tabla Terminal { get; set; }

        [MyRequired]
        [Column("ID_RETIRAPOR")]
        public int IdRetiraPor { get; set; }

        [ForeignKey("IdRetiraPor")]
        public virtual Tabla RetiraPor { get; set; }

        [Required]
        [DisplayName("Días")]
        [Digits]
        public Int16 Dias { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<CondEspeCliDia> CondEspeCliDias { get; set; }
    }
}
