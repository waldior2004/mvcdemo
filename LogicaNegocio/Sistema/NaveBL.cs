using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class NaveBL
    {
        private Repository _repositorio;

        public NaveBL()
        {
            _repositorio = new Repository();
        }

        public List<Nave> ObtAllNave(string desc)
        {
            return _repositorio.ObtAllNave(desc);
        }
        
    }
}
