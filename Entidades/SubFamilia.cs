namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_SUB_FAMILIA", Schema = "SISTEMA")]
    public class SubFamilia
    {
        public SubFamilia()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_FAMILIA")]
        [DisplayName("Familia")]
        [MyRequired]
        public int IdFamilia { get; set; }

        [ForeignKey("IdFamilia")]
        public virtual Familia Familia { get; set; }

        [MaxLength(50)]
        [Required]
        public string Descripcion { get; set; }

        [MaxLength(50)]
        [Required]
        public string Abreviatura { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
