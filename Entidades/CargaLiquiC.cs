namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_CARGA_LIQ_C", Schema = "SISTEMA")]
    public class CargaLiquiC
    {
        public CargaLiquiC()
        {
            this.CargaLiquiDs = new List<CargaLiquiD>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Column("ID_NAVE")]
        [DisplayName("Nave")]
        [MyRequired]
        public string IdNave { get; set; }

        [ForeignKey("IdNave")]
        public virtual Nave Nave { get; set; }

        [Column("ID_VIAJE")]
        [DisplayName("Viaje")]
        [MyRequired]
        public string IdViaje { get; set; }

        [ForeignKey("IdViaje")]
        public virtual Viaje Viaje { get; set; }

        [Column("ID_PUERTO")]
        [DisplayName("Puerto")]
        [MyRequired]
        public int IdPuerto { get; set; }

        [ForeignKey("IdPuerto")]
        public virtual Puerto Puerto { get; set; }

        [Column("TIPO_ENV")]
        [Required]
        public short TipoEnvio { get; set; }

        [Column("ID_DOCUMENTO")]
        [DisplayName("Documento")]
        [MyRequired]
        public int IdDocumento { get; set; }

        [ForeignKey("IdDocumento")]
        public virtual Documento Documento { get; set; }


        [Column("ID_DOCUMENTO2")]
        public int IdDocumento2 { get; set; }

        [ForeignKey("IdDocumento2")]
        public virtual Documento Documento2 { get; set; }

        [Column("ID_TERMINAL")]
        [DisplayName("Terminal")]
        [MyRequired]
        public int IdTerminal { get; set; }

        [ForeignKey("IdTerminal")]
        public virtual Tabla Terminal { get; set; }

        [Required]
        [Digits]
        public short Procesados { get; set; }

        [Required]
        [Digits]
        public short Correctos { get; set; }

        [Required]
        [Digits]
        public short Errados { get; set; }

        [MaxLength(255)]
        [Required]
        public string Estado { get; set; }

        [MaxLength(30)]
        [Required]
        public string Usuario { get; set; }

        public decimal Total { get; set; }

        [MaxLength(300)]
        public string Comentario { get; set; }

        [MaxLength(10)]
        public string Provision { get; set; }

        [Column("FEC_VALIDA")]
        public DateTime? FecValida { get; set; }

        [Column("AUD_FECREG")]
        public DateTime FecRegistro { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<CargaLiquiD> CargaLiquiDs { get; set; }
    }
}
