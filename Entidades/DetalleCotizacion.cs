namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_DETALLE_COTIZACION", Schema = "SISTEMA")]
    public class DetalleCotizacion
    {
        public DetalleCotizacion()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_COTIZACION")]
        public int IdCotizacion { get; set; }

        [InverseProperty("DetalleCotizaciones")]
        [ForeignKey("IdCotizacion")]
        public virtual Cotizacion Cotizacion { get; set; }

        [Column("ID_PRODUCTO")]
        [DisplayName("Producto")]
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [Column("ID_TARIFARIO")]
        [DisplayName("Tarifario")]
        public int? IdTarifario { get; set; }

        [ForeignKey("IdTarifario")]
        public virtual Tarifario Tarifario { get; set; }

        [Required]
        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [DisplayName("Precio")]
        public decimal Precio { get; set; }

        [Required]
        [DisplayName("Total")]
        public decimal Total { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
