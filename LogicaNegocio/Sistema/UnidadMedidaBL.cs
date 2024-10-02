using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class UnidadMedidaBL
    {
        private Repository _repositorio;

        public UnidadMedidaBL()
        {
            _repositorio = new Repository();
        }

        public List<UnidadMedida> ObtUnidadMedida()
        {
            return _repositorio.ObtUnidadMedida();
        }
    }
}
