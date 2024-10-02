namespace com.msc.infraestructure.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_EMPRESA", Schema = "SISTEMA")]
    public class Empresa
    {
        public Empresa()
        {
            
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Descripcion { get; set; }

        [MaxLength(35)]
        public string Telefono { get; set; }

        [MaxLength(10)]
        public string Abreviatura { get; set; }

        [MaxLength(250)]
        public string Direccion { get; set; }

        [MaxLength(11)]
        [Required]
        public string Ruc { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
