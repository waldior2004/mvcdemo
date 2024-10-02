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
        public static TarifaCEDTO GetTarifaCEDTO(this TarifaCE source)
        {
            var objR = source.CreateMap<TarifaCE, TarifaCEDTO>();

            if (source.Terminal != null)
                objR.Terminal = source.Terminal.CreateMap<Tabla, TablaDTO>();
            else
                objR.Terminal = new TablaDTO { Id = source.IdTerminal };

            if (source.PerTarifa != null)
                objR.PerTarifa = source.PerTarifa.CreateMap<Tabla, TablaDTO>();
            else
                objR.PerTarifa = new TablaDTO { Id = source.IdPerTar };

            if (source.Moneda != null)
                objR.Moneda = source.Moneda.CreateMap<Tabla, TablaDTO>();
            else
                objR.Moneda = new TablaDTO { Id = source.IdMoneda };

            if (source.Estado != null)
                objR.Estado = source.Estado.CreateMap<Tabla, TablaDTO>();
            else
                objR.Estado = new TablaDTO { Id = source.IdEstado };

            if (source.TarifaCEDocs.Count > 0)
                foreach (var item in source.TarifaCEDocs)
                {
                    objR.Documentos.Add(new DocumentoDTO
                    {
                        IdTarifaCEDoc = item.Id,
                        Id = item.IdDocumento,
                        Nombre = item.Documento.Nombre,
                        Extension = item.Documento.Extension,
                        RutaLocal = item.Documento.RutaLocal,
                        TamanoMB = item.Documento.TamanoMB
                    });
                }

            return objR;
        }

        public static TarifaCE SetTarifaCE(this TarifaCEDTO source)
        {
            var objR = source.CreateMap<TarifaCEDTO, TarifaCE>();

            if (source.Terminal != null)
            {
                objR.IdTerminal = source.Terminal.Id;
                objR.Terminal = source.Terminal.CreateMap<TablaDTO, Tabla>();
            }

            if (source.PerTarifa != null)
            {
                objR.IdPerTar = source.PerTarifa.Id;
                objR.PerTarifa = source.PerTarifa.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Moneda != null)
            {
                objR.IdMoneda = source.Moneda.Id;
                objR.Moneda = source.Moneda.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Estado != null)
            {
                objR.IdEstado = source.Estado.Id;
                objR.Estado = source.Estado.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Documentos != null)
            {
                foreach (var item in source.Documentos)
                {
                    var objI = new TarifaCEDoc
                    {
                        Id = item.IdTarifaCEDoc,
                        IdTarifaCE = source.Id,
                        IdDocumento = item.Id,
                        Documento = item.CreateMap<DocumentoDTO, Documento>()
                    };
                    objR.TarifaCEDocs.Add(objI);
                }
            }

            return objR;
        }
    }
}
