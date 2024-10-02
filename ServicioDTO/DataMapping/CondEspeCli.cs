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
        public static CondEspeCliDTO GetCondEspeCliDTO(this CondEspeCli source)
        {
            var objR = source.CreateMap<CondEspeCli, CondEspeCliDTO>();

            if (source.TipoCond != null)
                objR.TipoCond = source.TipoCond.CreateMap<Tabla, TablaDTO>();
            else
                objR.TipoCond = new TablaDTO { Id = source.IdTipoCond };

            if (source.CondEspeCliDocs.Count > 0)
                foreach (var item in source.CondEspeCliDocs)
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

            if (source.CondEspeCliDetalles.Count > 0)
                foreach (var item in source.CondEspeCliDetalles)
                {
                    objR.Detalles.Add(new CondEspeCliDetalleDTO
                    {
                        IdCondEspeCli = item.IdCondEspeCli,
                        Id = item.Id,
                        Dias = item.Dias,
                        RetiraPor = item.RetiraPor.CreateMap<Tabla, TablaDTO>(),
                        Terminal = item.Terminal.CreateMap<Tabla, TablaDTO>()
                    });
                }

            return objR;
        }

        public static CondEspeCli SetCondEspeCli(this CondEspeCliDTO source)
        {
            var objR = source.CreateMap<CondEspeCliDTO, CondEspeCli>();

            if (source.TipoCond != null)
            {
                objR.IdTipoCond = source.TipoCond.Id;
                objR.TipoCond = source.TipoCond.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Documentos != null)
            {
                foreach (var item in source.Documentos)
                {
                    var objI = new CondEspeCliDoc
                    {
                        Id = item.IdTarifaCEDoc,
                        IdCondEspeCli = source.Id,
                        IdDocumento = item.Id,
                        Documento = item.CreateMap<DocumentoDTO, Documento>()
                    };
                    objR.CondEspeCliDocs.Add(objI);
                }
            }

            if (source.Detalles != null)
            {
                foreach (var item in source.Detalles)
                {
                    var objI = new CondEspeCliDetalle
                    {
                        Id = item.Id,
                        IdCondEspeCli = source.Id,
                        IdTerminal = item.Terminal.Id,
                        Terminal = item.Terminal.CreateMap<TablaDTO, Tabla>(),
                        IdRetiraPor = item.RetiraPor.Id,
                        RetiraPor = item.RetiraPor.CreateMap<TablaDTO, Tabla>(),
                        Dias = item.Dias
                    };

                    foreach (var subi in item.CondEspeDias)
                    {
                        objI.CondEspeCliDias.Add(new CondEspeCliDia
                        {
                            Id = subi.Id,
                            IdCondEspeCliDetalle = item.Id,
                            DiaI = subi.DiaI,
                            DiaF = subi.DiaF,
                            IdTransporte = subi.Transporte.Id,
                            Transporte = subi.Transporte.CreateMap<TablaDTO, Tabla>()
                        });
                    }

                    objR.CondEspeCliDetalles.Add(objI);
                }
            }

            return objR;
        }
    }
}
