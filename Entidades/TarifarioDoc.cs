namespace com.msc.infraestructure.entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_TARIFARIO_DOCUMENTO", Schema = "SISTEMA")]

    public class TarifarioDoc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_DOCUMENTO")]
        public int IdDocumento { get; set; }

        [ForeignKey("IdDocumento")]
        public virtual Documento Documento { get; set; }

        [Column("ID_TARIFARIO")]
        public int IdTarifario { get; set; }

        [InverseProperty("TarifarioDocs")]
        [ForeignKey("IdTarifario")]
        public virtual Tarifario Tarifario { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
