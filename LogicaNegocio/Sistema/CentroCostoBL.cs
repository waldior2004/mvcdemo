using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class CentroCostoBL
    {
        private Repository _repositorio;

        public CentroCostoBL()
        {
            _repositorio = new Repository();
        }

        public List<CentroCosto> ObtCentroCosto()
        {
            return _repositorio.ObtCentroCosto();
        }

        public CentroCosto ObtCentroCosto(int Id)
        {
            return _repositorio.ObtCentroCosto(Id);
        }

        public Respuesta EditCentroCosto(CentroCosto obj)
        {
            return _repositorio.EditCentroCosto(obj);
        }

        public Respuesta ElimCentroCosto(int Id)
        {
            return _repositorio.ElimCentroCosto(Id);
        }
    }
}
