using com.msc.infraestructure.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static CargaLiquiDTO GetCargaLiquiDTO(this CargaLiqui source)
        {
            var objR = source.CreateMap<CargaLiqui, CargaLiquiDTO>();

            if (source.Contenedor != null)
                objR.Contenedor = source.Contenedor.CreateMap<Contenedor, ContenedorDTO>();
            else
                objR.Contenedor = new ContenedorDTO { Id = source.IdContenedor };

            if (source.Booking != null)
                objR.Booking = source.Booking.CreateMap<Booking, BookingDTO>();
            else
                objR.Booking = new BookingDTO { Id = source.IdBooking };

            if (source.TipoCarga != null)
                objR.TipoCarga = source.TipoCarga.CreateMap<Tabla, TablaDTO>();
            else
                objR.TipoCarga = new TablaDTO { Id = source.IdTipoCarga };

            if (source.Cliente != null)
                objR.Cliente = source.Cliente.CreateMap<Cliente, ClienteDTO>();
            else
                objR.Cliente = new ClienteDTO { Id = source.IdCliente };

            if (source.Nave != null)
                objR.Nave = source.Nave.CreateMap<Nave, NaveDTO>();
            else
                objR.Nave = new NaveDTO { Id = source.IdNave };

            if (source.Viaje != null)
                objR.Viaje = source.Viaje.CreateMap<Viaje, ViajeDTO>();
            else
                objR.Viaje = new ViajeDTO { Id = source.IdViaje };

            if (source.Tarifa != null)
            {
                objR.Tarifa = source.Tarifa.CreateMap<TarifaCE, TarifaCEDTO>();
                objR.Tarifa.PerTarifa = source.Tarifa.PerTarifa.CreateMap<Tabla, TablaDTO>();
                objR.Tarifa.Moneda = source.Tarifa.Moneda.CreateMap<Tabla, TablaDTO>();
            }
            else
                objR.Tarifa = new TarifaCEDTO { Id = source.IdTarifa };

            if (source.CondEspeCli != null)
            {
                objR.CondEspeCli = source.CondEspeCli.CreateMap<CondEspeCli, CondEspeCliDTO>();
                objR.CondEspeCli.TipoCond = source.CondEspeCli.TipoCond.CreateMap<Tabla, TablaDTO>();
            }
            else
                objR.CondEspeCli = new CondEspeCliDTO { Id = source.IdCondEspeCli };

            return objR;
        }

        public static CargaLiqui SetCargaLiqui(this CargaLiquiDTO source)
        {
            var objR = source.CreateMap<CargaLiquiDTO, CargaLiqui>();
            if (source.Contenedor != null)
            {
                objR.IdContenedor = source.Contenedor.Id;
                objR.Contenedor = source.Contenedor.CreateMap<ContenedorDTO, Contenedor>();
            }

            if (source.Booking != null)
            {
                objR.IdBooking = source.Booking.Id;
                objR.Booking = source.Booking.CreateMap<BookingDTO, Booking>();
            }

            if (source.Cliente != null)
            {
                objR.IdCliente = source.Cliente.Id;
                objR.Cliente = source.Cliente.CreateMap<ClienteDTO, Cliente>();
            }

            if (source.TipoCarga != null)
            {
                objR.IdTipoCarga = source.TipoCarga.Id;
                objR.TipoCarga = source.TipoCarga.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Nave != null)
            {
                objR.IdNave = source.Nave.Id;
                objR.Nave = source.Nave.CreateMap<NaveDTO, Nave>();
            }

            if (source.Viaje != null)
            {
                objR.IdViaje = source.Viaje.Id;
                objR.Viaje = source.Viaje.CreateMap<ViajeDTO, Viaje>();
            }

            if (source.Tarifa != null)
            {
                objR.IdTarifa = source.Tarifa.Id;
                objR.Tarifa = source.Tarifa.CreateMap<TarifaCEDTO, TarifaCE>();
                objR.Tarifa.Moneda = source.Tarifa.Moneda.CreateMap<TablaDTO, Tabla>();
                objR.Tarifa.PerTarifa = source.Tarifa.PerTarifa.CreateMap<TablaDTO, Tabla>();
            }

            if (source.CondEspeCli != null)
            {
                objR.IdCondEspeCli = source.CondEspeCli.Id;
                objR.CondEspeCli = source.CondEspeCli.CreateMap<CondEspeCliDTO, CondEspeCli>();
                objR.CondEspeCli.TipoCond = source.CondEspeCli.TipoCond.CreateMap<TablaDTO, Tabla>();
            }

            return objR;
        }
    }
}
