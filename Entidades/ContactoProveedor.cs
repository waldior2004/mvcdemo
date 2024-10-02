using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_CONTACTO_PROVEEDOR", Schema = "SISTEMA")]
    public class ContactoProveedor
    {
        public ContactoProveedor()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_PROVEEDOR")]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        [InverseProperty("ContactoProveedor")]
        public virtual Proveedor Proveedor { get; set; }


        [Column("ID_CARGO")]
        public int IdCargo { get; set; }

        [ForeignKey("IdCargo")]
        public virtual Tabla Cargo { get; set; }

        [Column("NOMBRES_COMPLETOS")]
        public string NombreCompleto { get; set; }

        [Column("CORREO")]
        public string Correo { get; set; }

        [Column("INDICADOR_CONTACTO")]
        public Byte IndContacto { get; set; }

        [Column("AUD_FECREG")]
        public DateTime? AudInsert { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime? AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }


    }
}
