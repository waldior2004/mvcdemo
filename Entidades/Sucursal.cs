namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_SUCURSAL", Schema = "SISTEMA")]
    public class Sucursal
    {
        public Sucursal()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_EMPRESA")]
        [DisplayName("Empresa")]
        [MyRequired]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa { get; set; }

        [MaxLength(50)]
        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [MaxLength(5)]
        [Required]
        [DisplayName("Abreviación")]
        public string Abreviatura { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
