namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_COND_ESP_CLI_DOCUMENTO", Schema = "SISTEMA")]
    public class CondEspeCliDoc
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_DOCUMENTO")]
        public int IdDocumento { get; set; }

        [ForeignKey("IdDocumento")]
        public virtual Documento Documento { get; set; }

        [Column("ID_COND_ESP_CLI")]
        public int IdCondEspeCli { get; set; }

        [InverseProperty("CondEspeCliDocs")]
        [ForeignKey("IdCondEspeCli")]
        public virtual CondEspeCli CondEspeCli { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
