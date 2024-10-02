namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_COND_ESP_CLI_DIA", Schema = "SISTEMA")]
    public class CondEspeCliDia
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_COND_ESP_CLI_DETALLE")]
        public int IdCondEspeCliDetalle { get; set; }

        [InverseProperty("CondEspeCliDias")]
        [ForeignKey("IdCondEspeCliDetalle")]
        public virtual CondEspeCliDetalle CondEspeCliDetalle { get; set; }

        [MyRequired]
        [Column("ID_TRANS")]
        public int IdTransporte { get; set; }

        [ForeignKey("IdTransporte")]
        public virtual Tabla Transporte { get; set; }

        [MyRequired]
        [DisplayName("Desde")]
        public Int16 DiaI { get; set; }

        [MyRequired]
        [DisplayName("Hasta")]
        
        public Int16 DiaF { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
