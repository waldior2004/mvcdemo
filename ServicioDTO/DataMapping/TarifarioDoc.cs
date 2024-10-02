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
        public static TarifarioDocDTO GetTarifariDocDTO(this TarifarioDoc source)
        {
            var objR = source.CreateMap<TarifarioDoc, TarifarioDocDTO>();
            return objR;
        }

        public static TarifarioDoc SetTarifarioDoc(this TarifarioDocDTO source)
        {
            var objR = source.CreateMap<TarifarioDocDTO, TarifarioDoc>();
            return objR;
        }

        public static List<Documento> TransformTarifarioDoc(this List<TarifarioDoc> lst)
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
