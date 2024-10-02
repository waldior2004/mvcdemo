namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_PRODUCTO", Schema = "SISTEMA")]
    public class Producto
    {
        public Producto()
        {
            this.Tarifarios = new List<Tarifario>();
            this.DetallePedidos = new List<DetallePedido>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_CODIGO_GRUPO")]
        [DisplayName("Grupo")]
        [MyRequired]
        public int IdCodigoGrupo { get; set; }

        [ForeignKey("IdCodigoGrupo")]
        public virtual Tabla Grupo { get; set; }

        [Column("ID_TIPO_PRODUCTO")]
        [DisplayName("Tipo de Producto")]
        [MyRequired]
        public int IdTipoServicio { get; set; }

        [ForeignKey("IdTipoServicio")]
        public virtual Tabla TipoServicio { get; set; }

        [Column("ID_CODIGO_FAMILIA")]
        [DisplayName("Familia")]
        [MyRequired]
        public int IdCodigoFamilia { get; set; }

        [ForeignKey("IdCodigoFamilia")]
        public virtual Familia Familia { get; set; }

        [Column("ID_CODIGO_SUB_FAMILIA")]
        [DisplayName("Sub Familia")]
        [MyRequired]
        public int IdCodigoSubFamilia { get; set; }

        [ForeignKey("IdCodigoSubFamilia")]
        public virtual SubFamilia SubFamilia { get; set; }

        [Column("ID_UNIDAD_MEDIDA")]
        [DisplayName("Unidad Medida")]
        [MyRequired]
        public int IdUnidadMedida { get; set; }

        [ForeignKey("IdUnidadMedida")]
        public virtual Tabla UnidadMedida { get; set; }

        [MaxLength(250)]
        [Required]
        public string Descripcion { get; set; }

        [MaxLength(50)]
        public string Abreviatura { get; set; }

        [MaxLength(50)]
        public string Observaciones { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }

        public virtual List<Tarifario> Tarifarios { get; set; }

        public virtual List<DetallePedido> DetallePedidos { get; set; }
    }
}
