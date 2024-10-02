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
        public static CargaLiquiCDTO GetCargaLiquiCDTO(this CargaLiquiC source)
        {
            var objR = source.CreateMap<CargaLiquiC, CargaLiquiCDTO>();

            if (source.Nave != null)
                objR.Nave = source.Nave.CreateMap<Nave, NaveDTO>();
            else
                objR.Nave = new NaveDTO { Id = source.IdNave };

            if (source.Viaje != null)
                objR.Viaje = source.Viaje.CreateMap<Viaje, ViajeDTO>();
            else
                objR.Viaje = new ViajeDTO { Id = source.IdViaje };

            if (source.Puerto != null)
                objR.Puerto = source.Puerto.CreateMap<Puerto, PuertoDTO>();
            else
                objR.Puerto = new PuertoDTO { Id = source.IdPuerto };

            if (source.Documento != null)
                objR.Documento = source.Documento.CreateMap<Documento, DocumentoDTO>();
            else
                objR.Documento = new DocumentoDTO { Id = source.IdDocumento };

            if (source.Documento2 != null)
                objR.Documento2 = source.Documento2.CreateMap<Documento, DocumentoDTO>();
            else
                objR.Documento2 = new DocumentoDTO { Id = source.IdDocumento2 };

            if (source.Terminal != null)
                objR.Terminal = source.Terminal.CreateMap<Tabla, TablaDTO>();
            else
                objR.Terminal = new TablaDTO { Id = source.IdTerminal };

            if (source.CargaLiquiDs.Count > 0)
                foreach (var item in source.CargaLiquiDs)
                {
                    objR.CargaLiquiDetalles.Add(new CargaLiquiDDTO
                    {
                        Id = item.Id,
                        Item = item.Item,
                        Nave = item.Nave,
                        Viaje = item.Viaje,
                        Shipper = item.Shipper,
                        Booking = item.Booking,
                        NumContenedores = item.NumContenedores,
                        Linea = item.Linea,
                        InDate = item.InDate,
                        Commodity = item.Commodity,
                        OutDate = item.OutDate,
                        Estado = item.Estado,
                        Total = item.Total
                    });
                }

            return objR;
        }

        public static CargaLiquiC SetCargaLiquiC(this CargaLiquiCDTO source)
        {
            var objR = source.CreateMap<CargaLiquiCDTO, CargaLiquiC>();

            if (source.Nave != null)
            {
                objR.IdNave = source.Nave.Id;
                objR.Nave = source.Nave.CreateMap<NaveDTO, Nave>();
            }

            if (source.Puerto != null)
            {
                objR.IdPuerto = source.Puerto.Id;
                objR.Puerto = source.Puerto.CreateMap<PuertoDTO, Puerto>();
            }

            if (source.Viaje != null)
            {
                objR.IdViaje = source.Viaje.Id;
                objR.Viaje = source.Viaje.CreateMap<ViajeDTO, Viaje>();
            }

            if (source.Documento != null)
            {
                objR.IdDocumento = source.Documento.Id;
                objR.Documento = source.Documento.CreateMap<DocumentoDTO, Documento>();
            }

            if (source.Documento2 != null)
            {
                objR.IdDocumento2 = source.Documento2.Id;
                objR.Documento2 = source.Documento2.CreateMap<DocumentoDTO, Documento>();
            }

            if (source.Terminal != null)
            {
                objR.IdTerminal = source.Terminal.Id;
                objR.Terminal = source.Terminal.CreateMap<TablaDTO, Tabla>();
            }

            if (source.CargaLiquiDetalles != null)
            {
                foreach (var item in source.CargaLiquiDetalles)
                {
                    var objI = new CargaLiquiD
                    {
                        Id = item.Id,
                        IdCargaLiquiC = source.Id,
                        Item = item.Item,
                        Nave = item.Nave,
                        Viaje = item.Viaje,
                        Shipper = item.Shipper,
                        Booking = item.Booking,
                        NumContenedores = item.NumContenedores,
                        Linea = item.Linea,
                        InDate = item.InDate,
                        OutDate = item.OutDate,
                        Commodity = item.Commodity,
                        Estado = item.Estado,
                        Total = item.Total
                    };
                    objR.CargaLiquiDs.Add(objI);
                }
            }

            return objR;
        }
    }
}
