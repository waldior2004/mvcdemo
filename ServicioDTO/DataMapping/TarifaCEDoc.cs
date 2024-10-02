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
        public static TarifaCEDocDTO GetTarifaCEDocDTO(this TarifaCEDoc source)
        {
            var objR = source.CreateMap<TarifaCEDoc, TarifaCEDocDTO>();
            return objR;
        }

        public static TarifaCEDoc SetTarifaCEDoc(this TarifaCEDocDTO source)
        {
            var objR = source.CreateMap<TarifaCEDocDTO, TarifaCEDoc>();
            return objR;
        }

        public static List<Documento> TransformTarifaCEDoc(this List<TarifaCEDoc> lst)
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
