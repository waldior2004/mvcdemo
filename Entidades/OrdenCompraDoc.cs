namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_ORDEN_COMPRA_DOCUMENTO", Schema = "SISTEMA")]
    public class OrdenCompraDoc
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_DOCUMENTO")]
        public int IdDocumento { get; set; }

        [ForeignKey("IdDocumento")]
        public virtual Documento Documento { get; set; }

        [Column("ID_ORDEN_COMPRA")]
        public int IdOrdenCompra { get; set; }

        [InverseProperty("OrdenCompraDocs")]
        [ForeignKey("IdOrdenCompra")]
        public virtual OrdenCompra OrdenCompra { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
    }
}
