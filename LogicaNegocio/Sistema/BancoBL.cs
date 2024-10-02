using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class BancoBL
    {
        private Repository _repositorio;

        public BancoBL()
        {
            _repositorio = new Repository();
        }
        public List<Banco> ObtBanco()
        {
            return _repositorio.ObtBanco();
        }
        public Banco ObtBanco(int Id)
        {
            return _repositorio.ObtBanco(Id);
        }
        public Respuesta EditBanco(Banco obj)
        {
            return _repositorio.EditBanco(obj);
        }
        public Respuesta ElimBanco(int Id)
        {
            return _repositorio.ElimBanco(Id);
        }
    }
}
