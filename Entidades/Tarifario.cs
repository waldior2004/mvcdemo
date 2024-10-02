namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_TARIFARIOS", Schema = "SISTEMA")]

    public class Tarifario
    {
        public Tarifario()
        {
            this.TarifarioDocs = new List<TarifarioDoc>();
            this.V_Tarifarios = new List<V_Tarifario>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PROVEEDOR")]
        [DisplayName("Proveedor")]
        [MyRequired]
        public int IdProveedor { get; set; }

        [InverseProperty("Tarifarios")]
        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [Column("ID_PRODUCTO")]
        [DisplayName("Producto")]
        [MyRequired]
        public int IdProducto { get; set; }

        [InverseProperty("Tarifarios")]
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

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

        [MaxLength(250)]        
        [Column("DESCRIPCION_PRODUCTO")]
        public string Descripcion { get; set; }

        [Required]
        public Decimal Precio { get; set; }

        [NotMapped]
        public int Cantidad { get; set; }

        [Column("FECHA_INICIO_VIGENCIA")]
        [Required]
        [Date(ErrorMessage = "La Fecha de Inicio tiene que tener formato dd/mm/yyyy")]
        public DateTime InicioVigencia { get; set; }

        [Column("FECHA_FIN_VIGENCIA")]
        [Required]
        [Date(ErrorMessage = "La Fecha de Fin tiene que tener formato dd/mm/yyyy")]
        public DateTime FinVigencia { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<TarifarioDoc> TarifarioDocs { get; set; }
        public virtual List<V_Tarifario> V_Tarifarios { get; set; }

    }
}
