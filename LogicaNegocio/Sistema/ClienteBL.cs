using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class ClienteBL
    {
        private Repository _repositorio;

        public ClienteBL()
        {
            _repositorio = new Repository();
        }

        public List<Cliente> ObtCliente()
        {
            return _repositorio.ObtCliente();
        }

        public List<Cliente> ObtAllCliente(string desc)
        {
            return _repositorio.ObtAllCliente(desc);
        }
        
    }
}
