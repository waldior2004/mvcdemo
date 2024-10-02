using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.entities.mvc;

namespace com.msc.infraestructure.biz
{
    public class LoginBL
    {
        private Repository _repositorio;

        public LoginBL()
        {
            _repositorio = new Repository();
        }

        public Respuesta ChangePassword(string Usuario, string Clave)
        {
            return _repositorio.ChangePassword(Usuario, Clave);
        }

        public Usuario GetUserAuthenticated(string user)
        {
            return _repositorio.GetUserAuthenticated(user);
        }

        public Externo GetUserExternalAuthenticated(string user)
        {
            return _repositorio.GetUserExternalAuthenticated(user);
        }

        public Respuesta Authenticate(Login obj)
        {
            return _repositorio.Authenticate(obj);
        }

        public Respuesta AuthenticateExterno(Login obj)
        {
            return _repositorio.AuthenticateExterno(obj);
        }
    }
}
