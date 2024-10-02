using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class UsuarioBL
    {
        private Repository _repositorio;

        public UsuarioBL()
        {
            _repositorio = new Repository();
        }

        public List<Usuario> ObtUsuario()
        {
            return _repositorio.ObtUsuario();
        }

        public Usuario ObtUsuario(int Id)
        {
            return _repositorio.ObtUsuario(Id);
        }

        public Respuesta EditUsuario(Usuario obj)
        {
            return _repositorio.EditUsuario(obj);
        }

        public Respuesta ElimUsuario(int Id)
        {
            return _repositorio.ElimUsuario(Id);
        }
    }
}
