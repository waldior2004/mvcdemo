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
        public static ProductoDTO GetProductoDTO(this Producto source)
        {
            var objR = source.CreateMap<Producto, ProductoDTO>();

            if (source.TipoServicio != null)
                objR.TipoServicio = source.TipoServicio.CreateMap<Tabla, TablaDTO>();
            else
                objR.TipoServicio = new TablaDTO { Id = source.IdTipoServicio };

            if (source.Familia != null)
                objR.Familia = source.Familia.CreateMap<Familia, FamiliaDTO>();
            else
                objR.Familia = new FamiliaDTO { Id = source.IdCodigoFamilia };

            if (source.SubFamilia != null)
                objR.SubFamilia = source.SubFamilia.CreateMap<SubFamilia, SubFamiliaDTO>();
            else
                objR.SubFamilia = new SubFamiliaDTO { Id = source.IdCodigoSubFamilia };

            if (source.Grupo != null)
                objR.Grupo = source.Grupo.CreateMap<Tabla, TablaDTO>();
            else
                objR.Grupo = new TablaDTO { Id = source.IdCodigoGrupo };

            if (source.UnidadMedida != null)
                objR.UnidadMedida = source.UnidadMedida.CreateMap<Tabla, TablaDTO>();
            else
                objR.UnidadMedida = new TablaDTO { Id = source.IdUnidadMedida };

            return objR;
        }

        public static Producto SetProducto(this ProductoDTO source)
        {
            var objR = source.CreateMap<ProductoDTO, Producto>();

            if (source.TipoServicio != null)
            {
                objR.IdTipoServicio = source.TipoServicio.Id;
                objR.TipoServicio = source.TipoServicio.CreateMap<TablaDTO, Tabla>();
            }

            if (source.Familia != null)
            {
                objR.IdCodigoFamilia = source.Familia.Id;
                objR.Familia = source.Familia.CreateMap<FamiliaDTO, Familia>();
            }

            if (source.SubFamilia != null)
            {
                objR.IdCodigoSubFamilia = source.SubFamilia.Id;
                objR.SubFamilia = source.SubFamilia.CreateMap<SubFamiliaDTO, SubFamilia>();
            }

            if (source.Grupo != null)
            {
                objR.IdCodigoGrupo = source.Grupo.Id;
                objR.Grupo = source.Grupo.CreateMap<TablaDTO, Tabla>();
            }

            if (source.UnidadMedida != null)
            {
                objR.IdUnidadMedida = source.UnidadMedida.Id;
                objR.UnidadMedida = source.UnidadMedida.CreateMap<TablaDTO, Tabla>();
            }

            return objR;
        }
    }
}
