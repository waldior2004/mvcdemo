namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_DETALLE_ORDEN_COMPRA", Schema = "SISTEMA")]
    public class DetalleOrdenCompra
    {
        public DetalleOrdenCompra()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_ORDEN_COMPRA")]
        public int IdOrdenCompra { get; set; }

        [InverseProperty("DetalleOrdenCompras")]
        [ForeignKey("IdOrdenCompra")]
        public virtual OrdenCompra OrdenCompra { get; set; }

        [DisplayName("Producto")]
        [Column("ID_PRODUCTO")]
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [Required]
        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [DisplayName("Precio")]
        public decimal Precio { get; set; }

        [Required]
        [DisplayName("Total")]
        public decimal Total { get; set; }

        [MaxLength(250)]
        [DisplayName("Observacion")]
        public string Observacion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}