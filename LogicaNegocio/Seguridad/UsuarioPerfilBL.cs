using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class UsuarioPerfilBL
    {
        private Repository _repositorio;

        public UsuarioPerfilBL()
        {
            _repositorio = new Repository();
        }
        public Respuesta EditUsuarioPerfil(UsuarioPerfil obj)
        {
            return _repositorio.EditUsuarioPerfil(obj);
        }
        public Respuesta ElimUsuarioPerfil(int Id)
        {
            return _repositorio.ElimUsuarioPerfil(Id);
        }
    }
}
