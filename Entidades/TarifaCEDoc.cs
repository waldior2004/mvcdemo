namespace com.msc.infraestructure.entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_TARIFA_CE_DOCUMENTO", Schema = "SISTEMA")]
    public class TarifaCEDoc
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_DOCUMENTO")]
        public int IdDocumento { get; set; }

        [ForeignKey("IdDocumento")]
        public virtual Documento Documento { get; set; }

        [Column("ID_TARIFA_CE")]
        public int IdTarifaCE { get; set; }

        [InverseProperty("TarifaCEDocs")]
        [ForeignKey("IdTarifaCE")]
        public virtual TarifaCE TarifaCE { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
