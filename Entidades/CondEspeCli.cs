namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_COND_ESP_CLI", Schema = "SISTEMA")]
    public class CondEspeCli
    {
        public CondEspeCli()
        {
            this.CondEspeCliDocs = new List<CondEspeCliDoc>();
            this.CondEspeCliDetalles = new List<CondEspeCliDetalle>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_TIPO_COND")]
        [DisplayName("Tipo Condición")]
        [MyRequired]
        public int IdTipoCond { get; set; }

        [ForeignKey("IdTipoCond")]
        public virtual Tabla TipoCond { get; set; }

        [Required]
        [Column("ID_REFERENCIA")]
        public string IdReferencia { get; set; }

        [Column("DESC_REFERENCIA")]
        public string DescReferencia { get; set; }

        [MaxLength(300)]
        [Required]
        public string Nombre { get; set; }

        [MaxLength(1500)]
        [Required]
        public string Descripcion { get; set; }

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

        [Date(ErrorMessage = "La Fecha de Fin tiene que tener formato dd/mm/yyyy")]
        [DisplayName("Fecha Aprobación")]
        [Column("FECHA_APRO")]
        public DateTime FechaAprobacion { get; set; }

        [MaxLength(30)]
        [DisplayName("Usuario Aprobación")]
        [Column("USU_APRO")]
        public string UsuAprobacion { get; set; }

        [DisplayName("Días Libres")]
        [Column("DIAS_LIBRES")]
        [Digits]
        public Int16 DiasLibres { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<CondEspeCliDoc> CondEspeCliDocs { get; set; }

        public virtual List<CondEspeCliDetalle> CondEspeCliDetalles { get; set; }
    }
}
