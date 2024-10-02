namespace com.msc.infraestructure.entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("V_NAVE", Schema = "SISTEMA")]
    public class Nave
    {
        public Nave()
        {

        }

        [Column("CODNAV")]
        [Required]
        public string Id { get; set; }

        [Column("NOMNAV")]
        [Required]
        public string Descripcion { get; set; }


    }
}
