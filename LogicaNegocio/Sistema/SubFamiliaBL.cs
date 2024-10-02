using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class SubFamiliaBL
    {
        private Repository _repositorio;

        public SubFamiliaBL()
        {
            _repositorio = new Repository();
        }

        public List<SubFamilia> ObtSubFamilia()
        {
            return _repositorio.ObtSubFamilia();
        }

        public SubFamilia ObtSubFamilia(int Id)
        {
            return _repositorio.ObtSubFamilia(Id);
        }

        public Respuesta EditSubFamilia(SubFamilia obj)
        {
            return _repositorio.EditSubFamilia(obj);
        }

        public Respuesta ElimSubFamilia(int Id)
        {
            return _repositorio.ElimSubFamilia(Id);
        }
    }
}
