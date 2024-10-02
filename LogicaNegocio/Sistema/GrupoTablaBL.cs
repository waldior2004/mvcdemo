using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class GrupoTablaBL
    {
        private Repository _repositorio;

        public GrupoTablaBL()
        {
            _repositorio = new Repository();
        }

        public List<GrupoTabla> ObtGrupoTabla()
        {
            return _repositorio.ObtGrupoTabla();
        }

        public GrupoTabla ObtGrupoTabla(int Id)
        {
            return _repositorio.ObtGrupoTabla(Id);
        }

        public Respuesta EditGrupoTabla(GrupoTabla obj)
        {
            return _repositorio.EditGrupoTabla(obj);
        }

        public Respuesta ElimGrupoTabla(int Id)
        {
            return _repositorio.ElimGrupoTabla(Id);
        }
    }
}
