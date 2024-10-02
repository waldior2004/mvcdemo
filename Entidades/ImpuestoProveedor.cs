using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities
{
    using com.msc.infraestructure.entities.dataannotations;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_IMPUESTO_PROVEEDOR", Schema = "SISTEMA")]
    public class ImpuestoProveedor
    {
        public ImpuestoProveedor()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PROVEEDOR")]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        [InverseProperty("Impuestos")]
        public virtual Proveedor Proveedor { get; set; }

        [Column("ID_IMPUESTO")]
        [MyRequired]
        public int IdImpuesto { get; set; }

        [ForeignKey("IdImpuesto")]
        public virtual Impuesto Impuesto { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime? AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
