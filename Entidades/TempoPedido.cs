namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("T_TEMPO_PEDIDO", Schema = "SISTEMA")]
    public class TempoPedido
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PEDIDO")]
        public int IdPedido { get; set; }

        [Column("ID_TARIFARIO")]
        public int? IdTarifario { get; set; }

        [Column("ID_PROVEEDOR")]
        public int IdProveedor { get; set; }

        [Column("ID_PRODUCTO")]
        public int IdProducto { get; set; }

        [Column("ID_MONEDA")]
        public int IdMoneda { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        [Column("DESCRIPCION_PRODUCTO")]
        public string Descripcion { get; set; }
    }
}
