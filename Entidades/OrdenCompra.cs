namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("T_ORDEN_COMPRA", Schema = "SISTEMA")]
    public class OrdenCompra
    {
        public OrdenCompra()
        {
            this.DetalleOrdenCompras = new List<DetalleOrdenCompra>();
            this.OrdenCompraDocs = new List<OrdenCompraDoc>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Codigo { get; set; }

        [Column("ID_COTIZACION")]
        [DisplayName("Cotización")]
        [MyRequired]
        public int IdCotizacion { get; set; }

        [ForeignKey("IdCotizacion")]
        public virtual Cotizacion Cotizacion { get; set; }


        [Column("ID_PROVEEDOR")]
        [DisplayName("Proveedor")]
        [MyRequired]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [Column("ID_ESTADO")]
        [DisplayName("Estado")]
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public virtual Tabla Estado { get; set; }

        [Required]
        [Date(ErrorMessage = "La Fecha de Registro tiene que tener formato dd/mm/yyyy")]
        [DisplayName("Fecha Registro")]
        [Column("FECHA_REGISTRO")]
        public DateTime FechaRegistro { get; set; }

        [MaxLength(250)]
        public string Observacion { get; set; }

        [DisplayName("Requiere Aprobación")]
        [Column("FLAG_APRO")]
        public Byte FlagAprobacion { get; set; }

        [MaxLength(250)]
        [DisplayName("Coment. Aprobador")]
        [Column("COMEN_APRO")]
        public string ComentAprobador { get; set; }

        [MaxLength(50)]
        [DisplayName("Nro. Factura")]
        [Column("NUM_FACTURA")]
        public string NumFactura { get; set; }

        [MaxLength(30)]
        [Column("USER")]
        public string User { get; set; }

        [MaxLength(30)]
        [Column("USER_APROB")]
        public string UserAprob { get; set; }
        
        [Required]
        public decimal SubTotal { get; set; }

        [Required]
        public decimal Igv { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<DetalleOrdenCompra> DetalleOrdenCompras { get; set; }
        public virtual List<OrdenCompraDoc> OrdenCompraDocs { get; set;}
    }
}
