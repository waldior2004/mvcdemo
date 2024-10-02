using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(ContenedorDTO))]
    [KnownType(typeof(BookingDTO))]
    [KnownType(typeof(ClienteDTO))]
    [KnownType(typeof(TablaDTO))]
    [KnownType(typeof(NaveDTO))]
    [KnownType(typeof(ViajeDTO))]
    [KnownType(typeof(TarifaCEDTO))]
    [KnownType(typeof(CondEspeCliDTO))]
    public class CargaLiquiDTO
    {
        public CargaLiquiDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public ContenedorDTO Contenedor { get; set; }

        [DataMember]
        public BookingDTO Booking { get; set; }

        [DataMember]
        public ClienteDTO Cliente { get; set; }

        [DataMember]
        public TablaDTO TipoCarga { get; set; }

        [DataMember]
        public NaveDTO Nave { get; set; }

        [DataMember]
        public ViajeDTO Viaje { get; set; }

        [DataMember]
        public TarifaCEDTO Tarifa { get; set; }

        [DataMember]
        public CondEspeCliDTO CondEspeCli { get; set; }

        [DataMember]
        public string Linea { get; set; }

        [DataMember]
        public DateTime FecEntrada { get; set; }

        [DataMember]
        public DateTime FecSalida { get; set; }

        [DataMember]
        public short HorasTotal { get; set; }

        [DataMember]
        public short HorasReal { get; set; }

        [DataMember]
        public decimal TarifaHora { get; set; }

        [DataMember]
        public decimal Total { get; set; }
    }
}
