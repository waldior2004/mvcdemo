using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class PuertoBL
    {
        private Repository _repositorio;

        public PuertoBL()
        {
            _repositorio = new Repository();
        }

        public List<Puerto> ObtPuerto()
        {
            return _repositorio.ObtPuerto();
        }        
    }
}
