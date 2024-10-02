namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_UBIGEO", Schema = "SISTEMA")]
    public class Ubigeo
    {
        public Ubigeo()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PAIS")]
        [DisplayName("País")]
        [MyRequired]
        public int IdPais { get; set; }

        [ForeignKey("IdPais")]
        public virtual Tabla Pais { get; set; }

        public string CodDepartamento { get; set; }

        public string CodProvincia { get; set; }

        public string CodDistrito { get; set; }

        public string Descripcion { get; set; }
    }
}
