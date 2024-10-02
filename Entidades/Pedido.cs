namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_PEDIDO", Schema = "SISTEMA")]
    public class Pedido
    {
        public Pedido()
        {
            this.DetallePedidos = new List<DetallePedido>();
            this.Cotizaciones = new List<Cotizacion>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_EMPRESA")]
        [DisplayName("Empresa")]
        [MyRequired]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa { get; set; }

        [Column("ID_SUCURSAL")]
        [DisplayName("Sucursal")]
        [MyRequired]
        public int IdSucursal { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }

        [Column("ID_AREA_SOLICITANTE")]
        [DisplayName("Area Solicitante")]
        [MyRequired]
        public int IdAreaSolicitante { get; set; }

        [ForeignKey("IdAreaSolicitante")]
        public virtual AreaSolicitante AreaSolicitante { get; set; }

        [Column("ID_TIPO_PETICION")]
        [DisplayName("Tipo Petición")]
        [MyRequired]
        public int IdTipoPeticion { get; set; }

        [ForeignKey("IdTipoPeticion")]
        public virtual Tabla TipoPeticion { get; set; }

        [Column("ID_ESTADO")]
        [DisplayName("Estado")]
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public virtual Tabla Estado { get; set; }

        [Column("ID_CENTRO_COSTO")]
        [DisplayName("Centro de Costo")]
        [MyRequired]
        public int IdCentroCosto { get; set; }

        [ForeignKey("IdCentroCosto")]
        public virtual CentroCosto CentroCosto { get; set; }

        [MaxLength(50)]
        [DisplayName("Código")]
        public string Codigo { get; set; }

        [Required]
        [Date(ErrorMessage = "La Fecha de Petición tiene que tener formato dd/mm/yyyy")]
        [DisplayName("Fecha Petición")]
        [Column("FECHA_PETICION")]
        public DateTime FechaPeticion { get; set; }

        [Required]
        [Date(ErrorMessage = "La Fecha Esperada tiene que tener formato dd/mm/yyyy")]
        [DisplayName("Fecha Esperada")]
        [Column("FECHA_ESPERADA")]
        public DateTime FechaEsperada { get; set; }

        [MaxLength(250)]
        [Required]
        [DisplayName("Observaciones")]
        public string Observaciones { get; set; }

        [MaxLength(30)]
        [Required]
        [DisplayName("Usuario")]
        [Column("USUARIO_SOL")]
        public string UsuarioSol { get; set; }

        [DisplayName("Requiere Aprobación")]
        [Column("FLAG_APRO")]
        public Byte FlagAprobacion { get; set; }

        [MaxLength(250)]
        [DisplayName("Coment. Aprobador")]
        [Column("COMEN_APRO")]
        public string ComentAprobador { get; set; }

        [MaxLength(30)]
        [Column("USER")]
        public string User { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        [NotMapped]
        public int IdOrdenCompra { get; set; }

        [NotMapped]
        public virtual OrdenCompra OrdenCompra { get; set; }

        public virtual List<DetallePedido> DetallePedidos { get; set; }
        public virtual List<Cotizacion> Cotizaciones { get; set; }

    }
}
