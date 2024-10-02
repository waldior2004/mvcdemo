namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_DETALLE_PEDIDO", Schema = "SISTEMA")]
    public class DetallePedido
    {
        public DetallePedido()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PEDIDO")]
        public int IdPedido { get; set; }

        [InverseProperty("DetallePedidos")]
        [ForeignKey("IdPedido")]
        public virtual Pedido Pedido { get; set; }

        [DisplayName("Producto")]
        [Column("ID_PRODUCTO")]
        public int IdProducto { get; set; }

        [InverseProperty("DetallePedidos")]
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
        [DisplayName("Observaciones")]
        public string Observaciones { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
