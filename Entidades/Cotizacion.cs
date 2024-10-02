namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("T_COTIZACION", Schema = "SISTEMA")]
    public class Cotizacion
    {
        public Cotizacion()
        {
            this.DetalleCotizaciones = new List<DetalleCotizacion>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Codigo { get; set; }

        [Column("ID_PEDIDO")]
        [DisplayName("Pedido")]
        [MyRequired]
        public int? IdPedido { get; set; }

        [InverseProperty("Cotizaciones")]
        [ForeignKey("IdPedido")]
        public virtual Pedido Pedido { get; set; }


        [Column("ID_PROVEEDOR")]
        [DisplayName("Proveedor")]
        [MyRequired]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [NotMapped]
        public int IdOrdenCompra { get; set; }

        [NotMapped]
        public virtual OrdenCompra OrdenCompra { get; set; }

        [Column("ID_ESTADO")]
        [DisplayName("Estado")]
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public virtual Tabla Estado { get; set; }

        [Required]
        [Date(ErrorMessage = "La Fecha de Cotización tiene que tener formato dd/mm/yyyy")]
        [DisplayName("Fecha Cotización")]
        [Column("FECHA_COTIZACION")]
        public DateTime FechaCotizacion { get; set; }

        [Date(ErrorMessage = "La Fecha de Entrega tiene que tener formato dd/mm/yyyy")]
        [DisplayName("Fecha Entrega")]
        [Column("FECHA_ENTREGA")]
        public DateTime? FechaEntrega { get; set; }

        [MaxLength(250)]
        public string Observacion { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<DetalleCotizacion> DetalleCotizaciones { get; set; }
    }
}
