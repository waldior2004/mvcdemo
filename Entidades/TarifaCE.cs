namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_TARIFA_CE", Schema = "SISTEMA")]
    public class TarifaCE
    {
        public TarifaCE()
        {
            this.TarifaCEDocs = new List<TarifaCEDoc>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Column("ID_TERMINAL")]
        [DisplayName("Terminal")]
        [MyRequired]
        public int IdTerminal { get; set; }

        [ForeignKey("IdTerminal")]
        public virtual Tabla Terminal { get; set; }

        [Column("ID_PER_TAR")]
        [DisplayName("Período Tarifa")]
        [MyRequired]
        public int IdPerTar { get; set; }

        [ForeignKey("IdPerTar")]
        public virtual Tabla PerTarifa { get; set; }

        [Column("ID_MONEDA")]
        [DisplayName("Moneda")]
        [MyRequired]
        public int IdMoneda { get; set; }

        [ForeignKey("IdMoneda")]
        public virtual Tabla Moneda { get; set; }

        [Column("ID_ESTADO")]
        [DisplayName("Estado")]
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public virtual Tabla Estado { get; set; }

        [Required]
        [Numeric(ErrorMessage = "El Importe no tiene un valor correcto")]
        public decimal Importe { get; set; }

        [Required]
        [Date(ErrorMessage = "La Fecha de Inicio tiene que tener formato dd/mm/yyyy")]
        [DisplayName("Fecha Inicio")]
        [Column("FECHA_INICIO")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Date(ErrorMessage = "La Fecha de Fin tiene que tener formato dd/mm/yyyy")]
        [DisplayName("Fecha Fin")]
        [Column("FECHA_FIN")]
        public DateTime FechaFin { get; set; }

        public string Comentarios { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<TarifaCEDoc> TarifaCEDocs { get; set; }
    }
}
