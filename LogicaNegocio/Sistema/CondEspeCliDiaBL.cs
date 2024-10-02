using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Collections.Generic;

namespace com.msc.infraestructure.biz
{
    public class CondEspeCliDiaBL
    {
        private Repository _repositorio;

        public CondEspeCliDiaBL()
        {
            _repositorio = new Repository();
        }
        public List<CondEspeCliDia> ObtCondEspeCliDia(int Id)
        {
            return _repositorio.ObtCondEspeCliDia(Id);
        }
        public Respuesta EditCondEspeCliDia(CondEspeCliDia obj)
        {
            return _repositorio.EditCondEspeCliDia(obj);
        }
        public Respuesta ElimCondEspeCliDia(int Id)
        {
            return _repositorio.ElimCondEspeCliDia(Id);
        }
    }
}
