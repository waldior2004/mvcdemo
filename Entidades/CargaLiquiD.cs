namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_CARGA_LIQ_D", Schema = "SISTEMA")]
    public class CargaLiquiD
    {
        public CargaLiquiD()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_CARGA_LIQ_C")]
        [DisplayName("Proceso")]
        [MyRequired]
        public int IdCargaLiquiC { get; set; }

        [InverseProperty("CargaLiquiDs")]
        [ForeignKey("IdCargaLiquiC")]
        public virtual CargaLiquiC CargaLiquiC { get; set; }

        [Column("NUM_CONTEN")]
        public string NumContenedores { get; set; }

        public string Booking { get; set; }

        public short Item { get; set; }

        public string Linea { get; set; }

        [Column("IN_DATE")]
        public string InDate { get; set; }

        [Column("OUT_DATE")]
        public string OutDate { get; set; }

        public string Shipper { get; set; }

        public string Commodity { get; set; }

        public string Nave { get; set; }

        public string Viaje { get; set; }

        public string Estado { get; set; }

        public decimal Total { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
