using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.entities.mvc
{
    public class Filtro
    {
        public string SortField { get; set; }
        public string FilterField { get; set; }
        public string FilterValue { get; set; }
        public string SortOrder { get; set; }
        public int CurrentPage { get; set; }
        public string AVCodigo { get; set; }
        public string AVDescripcion { get; set; }
    }
}
