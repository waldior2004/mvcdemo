namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("V_VIAJE", Schema = "SISTEMA")]
    public class Viaje
    {
        public Viaje()
        {

        }

        [Required]
        [Column("iditin")]
        public string Id { get; set; }

        [Required]
        [Column("numviaje")]
        public string Descripcion { get; set; }

        [Column("codnav")]
        [DisplayName("Nave")]
        [MyRequired]
        public string IdNave { get; set; }

        [ForeignKey("IdNave")]
        public virtual Nave Nave { get; set; }

        [Column("puerto_id")]
        [DisplayName("Puerto")]
        [MyRequired]
        public int IdPuerto { get; set; }

        [ForeignKey("IdPuerto")]
        public virtual Puerto Puerto { get; set; }


        [Column("fechasal")]
        public DateTime FechaZarpe { get; set; }

        [Column("fechalle")]
        public DateTime FechaETA { get; set; }

    }
}
