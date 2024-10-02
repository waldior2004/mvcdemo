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
        public static DocumentoDTO GetDocumentoDTO(this Documento source)
        {
            var objR = source.CreateMap<Documento, DocumentoDTO>();
            return objR;
        }

        public static Documento SetDocumento(this DocumentoDTO source)
        {
            return source.CreateMap<DocumentoDTO, Documento>();
        }
    }
}
