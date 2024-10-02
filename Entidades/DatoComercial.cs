using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_DATOS_COMERCIALES", Schema = "SISTEMA")]
    public class DatoComercial
    {
        public DatoComercial()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column("ID_PROVEEDOR")]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        [InverseProperty("DatoComercial")]
        public virtual Proveedor Proveedor { get; set; }

        [Column("ID_PAIS")]
        public int IdPais2 { get; set; }

        [ForeignKey("IdPais2")]
        public virtual Tabla Pais { get; set; }

        [Column("ID_BANCO")]
        public int IdBanco { get; set; }

        [ForeignKey("IdBanco")]
        public virtual Banco Banco { get; set; }
        
        [Column("ID_TIPO_CUENTA")]
        public int IdTipoCuenta { get; set; }

        [ForeignKey("IdTipoCuenta")]        
        public virtual Tabla TipoCuenta { get; set; }

        [Column("ID_TIPO_INTERLOCUTOR")]
        public int IdTipoInterlocutor { get; set; }

        [ForeignKey("IdTipoInterlocutor")]
        public virtual Tabla TipoInterlocutor { get; set; }

        [Column("NUMERO_CUENTA")]
        [MaxLength(30)]
        public string NroCuenta { get; set; }

        [Column("NUMERO_CCI")]
        [MaxLength(50)]
        public string NroCCI { get; set; }

        [MaxLength(35)]
        public string Swift { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime? AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

    }
}
