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
        public static CondEspeCliDocDTO GetCondEspeCliDocDTO(this CondEspeCliDoc source)
        {
            var objR = source.CreateMap<CondEspeCliDoc, CondEspeCliDocDTO>();
            return objR;
        }

        public static CondEspeCliDoc SetCondEspeCliDoc(this CondEspeCliDocDTO source)
        {
            var objR = source.CreateMap<CondEspeCliDocDTO, CondEspeCliDoc>();
            return objR;
        }

        public static List<Documento> TransformCondEspeCliDoc(this List<CondEspeCliDoc> lst)
        {
            var lstDoc = new List<Documento>();
            foreach (var item in lst)
            {
                lstDoc.Add(new Documento
                {
                    Id = item.Id,
                    Nombre = item.Documento.Nombre,
                    Extension = item.Documento.Extension
                });
            }
            return lstDoc;
        }
    }
}
