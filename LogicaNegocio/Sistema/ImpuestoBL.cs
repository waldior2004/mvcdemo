using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class ImpuestoBL
    {
        private Repository _repositorio;

        public ImpuestoBL()
        {
            _repositorio = new Repository();
        }

        public List<Impuesto> ObtImpuesto()
        {
            return _repositorio.ObtImpuesto();
        }

        public Impuesto ObtImpuesto(int Id)
        {
            return _repositorio.ObtImpuesto(Id);
        }

        public Respuesta EditImpuesto(Impuesto obj)
        {
            return _repositorio.EditImpuesto(obj);
        }

        public Respuesta ElimImpuesto(int Id)
        {
            return _repositorio.ElimImpuesto(Id);
        }
    }
}
