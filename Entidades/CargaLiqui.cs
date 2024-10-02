namespace com.msc.infraestructure.entities
{
    using dataannotations;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("T_CARGA_LIQ", Schema = "SISTEMA")]
    public class CargaLiqui
    {
        public CargaLiqui()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_CARGA_LIQ_D")]
        [MyRequired]
        public int IdCargaLiquiD { get; set; }

        [ForeignKey("IdCargaLiquiD")]
        public virtual CargaLiquiD CargaLiquiD { get; set; }

        [Column("ID_CONTENEDOR")]
        [DisplayName("Contenedor")]
        [MyRequired]
        public string IdContenedor { get; set; }

        [ForeignKey("IdContenedor")]
        public virtual Contenedor Contenedor { get; set; }

        [Column("ID_BOOKING")]
        [DisplayName("Booking")]
        [MyRequired]
        public int IdBooking { get; set; }

        [ForeignKey("IdBooking")]
        public virtual Booking Booking { get; set; }

        [Column("ID_CLIENTE")]
        [DisplayName("Cliente")]
        [MyRequired]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [Column("ID_TIPOCARGA")]
        [DisplayName("TipoCarga")]
        [MyRequired]
        public int IdTipoCarga { get; set; }

        [ForeignKey("IdTipoCarga")]
        public virtual Tabla TipoCarga { get; set; }

        [Column("ID_NAVE")]
        [DisplayName("Nave")]
        [MyRequired]
        public string IdNave { get; set; }

        [ForeignKey("IdNave")]
        public virtual Nave Nave { get; set; }

        [Column("ID_VIAJE")]
        [DisplayName("Viaje")]
        [MyRequired]
        public string IdViaje { get; set; }

        [ForeignKey("IdViaje")]
        public virtual Viaje Viaje { get; set; }

        [Column("ID_TARIFA")]
        [DisplayName("Tarifa")]
        [MyRequired]
        public int IdTarifa { get; set; }

        [ForeignKey("IdTarifa")]
        public virtual TarifaCE Tarifa { get; set; }

        [Column("ID_COND_ESPE")]
        [DisplayName("Condición Especial")]
        [MyRequired]
        public int IdCondEspeCli { get; set; }

        [ForeignKey("IdCondEspeCli")]
        public virtual CondEspeCli CondEspeCli { get; set; }

        [Required]
        [DisplayName("Línea")]
        public string Linea { get; set; }

        [Column("FEC_ENTRADA")]
        [DisplayName("Fecha Entrada")]
        [Required]        
        public DateTime FecEntrada { get; set; }

        [Column("FEC_SALIDA")]
        [DisplayName("Fecha Salida")]
        [Required]
        public DateTime FecSalida { get; set; }

        [Column("HORAS_TOTAL")]
        [DisplayName("Horas Total")]
        [Required]
        public short HorasTotal { get; set; }

        [Column("HORAS_REAL")]
        [DisplayName("Horas Facturar")]
        [Required]
        public short HorasReal { get; set; }

        [Column("TARIFA_HORA")]
        [DisplayName("Tarifa/Hora")]
        [Required]
        public decimal TarifaHora { get; set; }

        [Column("TOTAL")]
        [DisplayName("Total")]
        [Required]
        public decimal Total { get; set; }

        [Column("AUD_FECMOD")]
        public DateTime AudUpdate { get; set; }

        [Column("AUD_ACTIVE", TypeName = "tinyint")]
        public Byte AudActivo { get; set; }



        [DisplayFormat(DataFormatString = "{0:n3}")]
        [DisplayName("Total (Sin IGV)")]
              public decimal TotalSinIGV { get
            {
                return Total / 1.18M;
            }
            
        }

    }
}
