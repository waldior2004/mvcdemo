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
        public static CondEspeCliDetalleDTO GetCondEspeCliDetalleDTO(this CondEspeCliDetalle source)
        {
            var objR = source.CreateMap<CondEspeCliDetalle, CondEspeCliDetalleDTO>();

            if (source.Terminal != null)
                objR.Terminal = source.Terminal.CreateMap<Tabla, TablaDTO>();
            else
                objR.Terminal = new TablaDTO { Id = source.IdTerminal };

            if (source.RetiraPor != null)
                objR.RetiraPor = source.RetiraPor.CreateMap<Tabla, TablaDTO>();
            else
                objR.RetiraPor = new TablaDTO { Id = source.IdRetiraPor };

            if (source.CondEspeCliDias.Count > 0)
                foreach (var item in source.CondEspeCliDias)
                {
                    objR.CondEspeDias.Add(new CondEspeCliDiaDTO
                    {
                        IdCondEspeCliDetalle = source.Id,
                        Id = item.Id,
                        DiaI = item.DiaI,
                        DiaF = item.DiaF,
                        Transporte = item.Transporte.CreateMap<Tabla, TablaDTO>()
                    });
                }

            return objR;
        }

        public static CondEspeCliDetalle SetCondEspeCliDetalle(this CondEspeCliDetalleDTO source)
        {
            var objR = source.CreateMap<CondEspeCliDetalleDTO, CondEspeCliDetalle>();
            if (source.Terminal != null)
            {
                objR.IdTerminal = source.Terminal.Id;
                objR.Terminal = source.Terminal.CreateMap<TablaDTO, Tabla>();
            }
            if (source.RetiraPor != null)
            {
                objR.IdRetiraPor = source.RetiraPor.Id;
                objR.RetiraPor = source.RetiraPor.CreateMap<TablaDTO, Tabla>();
            }

            if (source.CondEspeDias != null)
            {
                foreach (var item in source.CondEspeDias)
                {
                    var objI = new CondEspeCliDia
                    {
                        Id = item.Id,
                        IdCondEspeCliDetalle = source.Id,
                        DiaI = item.DiaI,
                        DiaF = item.DiaF,
                        IdTransporte = item.Transporte.Id,
                        Transporte = item.Transporte.CreateMap<TablaDTO, Tabla>()
                    };
                    objR.CondEspeCliDias.Add(objI);
                }
            }

            return objR;
        }
    }
}
