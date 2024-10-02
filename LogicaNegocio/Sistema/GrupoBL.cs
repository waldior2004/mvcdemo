using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class GrupoBL
    {
        private Repository _repositorio;

        public GrupoBL()
        {
            _repositorio = new Repository();
        }

        public List<Grupo> ObtGrupo()
        {
            return _repositorio.ObtGrupo();
        }
    }
}
