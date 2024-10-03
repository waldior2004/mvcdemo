using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Collections.Generic;

namespace com.msc.infraestructure.biz
{
    public class ExternoBL
    {
        private Repository _repositorio;

        public ExternoBL()
        {
            _repositorio = new Repository();
        }

        public Externo ObtExterno(string username)
        {
            return _repositorio.ObtExterno(username);
        }


        public List<Externo> ObtExterno()
        {
            return _repositorio.ObtExterno();
        }

        public Externo ObtExterno(int Id)
        {
            return _repositorio.ObtExterno(Id);
        }

        public Respuesta ResetKeyExterno(int Id)
        {
            return _repositorio.ResetKeyExterno(Id);
        }

        public Respuesta EditExterno(Externo obj)
        {
            return _repositorio.EditExterno(obj);
        }

        public Respuesta ElimExterno(int Id)
        {
            return _repositorio.ElimExterno(Id);
        }
    }
}
