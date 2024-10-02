namespace com.msc.infraestructure.entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("V_CLIENTE", Schema = "SISTEMA")]
    public class Cliente
    {
        public Cliente()
        {

        }

        [Column("Id_Cliente")]
        [Required]
        public int Id { get; set; }

        [Column("Nombre")]
        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Ruc { get; set; }

    }
}
