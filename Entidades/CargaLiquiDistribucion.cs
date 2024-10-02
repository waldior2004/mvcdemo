namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_CARGA_LIQ_DISTRIBUCION", Schema = "SISTEMA")]
    public class CargaLiquiDistribucion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Column("ID_CARGA_LIQ_D")]
        [MyRequired]
        public int IdCargaLiquiD { get; set; }


        [Column("DIA")]
        [DisplayName("Día")]
        [MyRequired]
        public int Dia { get; set; }


        [Column("ID_SPLIT")]
        [DisplayName("Split")]
        [MyRequired]
        public int IdTransporte { get; set; }

        [Column("HORAS")]
        [DisplayName("Horas")]
        [MyRequired]
        public int Horas { get; set; }
    }
}
