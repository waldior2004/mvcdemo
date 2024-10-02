namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_PROVISION", Schema = "SISTEMA")]
    public class Provision
    {
        public Provision()
        {

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

        [Column("ID_TIPO_PROVISION")]
        [DisplayName("Tipo Provisión")]
        [MyRequired]
        public int IdTipoProvision { get; set; }

        [ForeignKey("IdTipoProvision")]
        public virtual Tabla TipoProvision { get; set; }

        [Column("ID_CUENTA_CONTABLE")]
        [DisplayName("Cuenta Contable")]
        [MyRequired]
        public int IdCuentaContable { get; set; }

        [ForeignKey("IdCuentaContable")]
        public virtual Tabla CuentaContable { get; set; }

        [Column("ID_PROVEEDOR")]
        [DisplayName("Proveedor")]
        [MyRequired]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [Column("ID_MONEDA")]
        [DisplayName("Moneda")]
        [MyRequired]
        public int IdMoneda { get; set; }

        [ForeignKey("IdMoneda")]
        public virtual Tabla Moneda { get; set; }

        [Column("ID_ESTADO")]
        [DisplayName("Estado")]
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public virtual Tabla Estado { get; set; }

        [Column("ID_ORDEN_COMPRA")]
        [DisplayName("Orden de Compra")]
        public int? IdOrdenCompra { get; set; }

        [ForeignKey("IdOrdenCompra")]
        public virtual OrdenCompra OrdenCompra { get; set; }

        [MaxLength(50)]
        public string Codigo { get; set; }

        [MaxLength(30)]
        [Column("USER")]
        [DisplayName("Registrado Por")]
        public string User { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [MaxLength(350)]
        [Required]
        public string Concepto { get; set; }

        [Column("MES_PROV")]
        public int MesProv { get; set; }

        [Column("ANIO_PROV")]
        public int AnioProv { get; set; }

        [Column("MES_SERV")]
        public int MesServ { get; set; }

        [Column("ANIO_SERV")]
        public int AnioServ { get; set; }

        [Column("COMENTARIO_UNO")]
        [DisplayName("Comentario Uno")]
        public string ComentarioUno { get; set; }

        [Column("COMENTARIO_DOS")]
        [DisplayName("Comentario Dos")]
        public string ComentarioDos { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime? AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
