namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_PROVEEDOR", Schema = "SISTEMA")]
    public class Proveedor
    {
        public Proveedor()
        {
            Tarifarios = new List<Tarifario>();
            DatoComercial = new List<DatoComercial>();
            ContactoProveedor = new List<ContactoProveedor>();
            Impuestos = new List<ImpuestoProveedor>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        


        [Column("ID_TIPOPERSONA")]
        [DisplayName("Tipo Persona")]
        [MyRequired]
        public int? IdTipoPersona { get; set; }

        [ForeignKey("IdTipoPersona")]
        public virtual Tabla TipoPersona { get; set; }

        [Column("NUMERO_RUC")]
        [MaxLength(11)]
        [Required]
        public string Ruc { get; set; }

        [Column("RAZON_SOCIAL")]
        [DisplayName("Razón Social")]
        [MaxLength(150)]
        [Required]
        public string RazonSocial { get; set; }

        [Column("NOMBRE_COMERCIAL")]
        [DisplayName("Nombre Comercial")]
        [MaxLength(150)]
        [Required]
        public string NombreComercial { get; set; }

        [Column("APELLIDO_PATERNO")]
        [DisplayName("Ape. Paterno")]
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }

        [Column("APELLIDO_MATERNO")]
        [DisplayName("Ape. Materno")]
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }

        [Column("NOMBRES")]
        [DisplayName("Nombres")]
        [MaxLength(50)]
        public string Nombres { get; set; }


        [Column("ID_PAIS")]
        [DisplayName("País")]
        [MyRequired]
        public int? IdPais { get; set; }

        [ForeignKey("IdPais")]
        public virtual Tabla Pais { get; set; }

        [Column("ID_DEPARTAMENTO")]
        [DisplayName("Departamento")]
        [MyRequired]
        public int? IdDepartamento { get; set; }

        [ForeignKey("IdDepartamento")]
        public virtual Ubigeo Departamento { get; set; }

        [Column("ID_PROVINCIA")]
        [DisplayName("Provincia")]
        [MyRequired]
        public int? IdProvincia { get; set; }

        [ForeignKey("IdProvincia")]
        public virtual Ubigeo Provincia { get; set; }

        [Column("ID_DISTRITO")]
        [DisplayName("Distrito")]
        [MyRequired]
        public int? IdDistrito { get; set; }

        [ForeignKey("IdDistrito")]
        public virtual Ubigeo Distrito { get; set; }

        [Column("DIRECCION_PRINCIPAL")]
        [DisplayName("Dirección")]
        [MaxLength(100)]
        [Required]
        public string DireccionPrincipal { get; set; }

        [Column("NUMERO_TELEFONO")]
        [DisplayName("Teléfono")]
        [MaxLength(20)]
        [Required]
        public string Telefono { get; set; }

        [Column("CORREO")]
        [DisplayName("Correo")]
        [MaxLength(40)]
        public string Email { get; set; }

        [Column("REPRESENTANTE_LEGAL")]
        [DisplayName("Rep. Legal")]
        [MaxLength(100)]
        [Required]
        public string RepresentanteLegal { get; set; }

        [Column("ID_GIRONEGOCIO")]
        [DisplayName("Giro Negocio")]
        [MyRequired]
        public int? IdGiroNegocio { get; set; }

        [ForeignKey("IdGiroNegocio")]
        public virtual Tabla GiroNegocio { get; set; }

        [Column("CODIGO_SAP")]
        [DisplayName("Código SAP")]
        [Required]
        [MaxLength(50)]
        public string CodigoSAP { get; set; }

        [Column("ID_NIF")]
        [DisplayName("Tipo NIF")]
        [MyRequired]
        public int? IdTipoNIF { get; set; }

        [ForeignKey("IdTipoNIF")]
        public virtual Tabla TipoNIF { get; set; }

        [Column("ID_FORMA_COBRO")]
        [DisplayName("Forma Cobro")]
        [MyRequired]
        public int? IdFormaCobro { get; set; }

        [ForeignKey("IdFormaCobro")]
        public virtual Tabla FormaCobro { get; set; }


        [Column("ID_TIPO_CONT")]
        [DisplayName("Tipo Contribuyente")]
        public int? IdTipoContribuyente { get; set; }

        [ForeignKey("IdTipoContribuyente")]
        public virtual Tabla TipoContribuyente { get; set; }

        [Column("AUD_FECREG")]
        public DateTime? AudInsert { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime? AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }
        
        public virtual List<DatoComercial> DatoComercial { get; set; }

        public virtual List<ContactoProveedor> ContactoProveedor { get; set; }

        public virtual List<Tarifario> Tarifarios { get; set; }

        public virtual List<ImpuestoProveedor> Impuestos { get; set; }
    }
}
