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
        public static OrdenCompraDocDTO GetOrdenCompraDocDTO(this OrdenCompraDoc source)
        {
            var objR = source.CreateMap<OrdenCompraDoc, OrdenCompraDocDTO>();
            return objR;
        }

        public static OrdenCompraDoc SetOrdenCompraDoc(this OrdenCompraDocDTO source)
        {
            var objR = source.CreateMap<OrdenCompraDocDTO, OrdenCompraDoc>();
            return objR;
        }

        public static List<Documento> TransformOrdenCompraDoc(this List<OrdenCompraDoc> lst)
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
