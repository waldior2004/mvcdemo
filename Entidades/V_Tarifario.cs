namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("H_TARIFARIOS", Schema = "SISTEMA")]

    public class V_Tarifario
    {
        public V_Tarifario()
        {

        }

        [Column("ID")]
        public int Id { get; set; }

        [Column("ID_PROVEEDOR")]
        [DisplayName("Proveedor")]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [Column("ID_PRODUCTO")]
        [DisplayName("Producto")]
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [Column("ID_MONEDA")]
        [DisplayName("Moneda")]
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

        [Column("FECHA_INICIO_VIGENCIA")]
        [Date(ErrorMessage = "La Fecha de Inicio tiene que tener formato dd/mm/yyyy")]
        public DateTime InicioVigencia { get; set; }

        [Column("FECHA_FIN_VIGENCIA")]
        [Date(ErrorMessage = "La Fecha de Fin tiene que tener formato dd/mm/yyyy")]
        public DateTime FinVigencia { get; set; }

    }
}
