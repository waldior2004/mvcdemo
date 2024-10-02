namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("V_PUERTO", Schema = "SISTEMA")]
    public class Puerto
    {
        public Puerto()
        {

        }

        [Required]
        [Column("codigo")]
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; }

    }
}