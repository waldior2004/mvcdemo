using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class FamiliaBL
    {
        private Repository _repositorio;

        public FamiliaBL()
        {
            _repositorio = new Repository();
        }

        public List<Familia> ObtFamilia()
        {
            return _repositorio.ObtFamilia();
        }

        public Familia ObtFamilia(int Id)
        {
            return _repositorio.ObtFamilia(Id);
        }

        public Respuesta EditFamilia(Familia obj)
        {
            return _repositorio.EditFamilia(obj);
        }

        public Respuesta ElimFamilia(int Id)
        {
            return _repositorio.ElimFamilia(Id);
        }
    }
}
