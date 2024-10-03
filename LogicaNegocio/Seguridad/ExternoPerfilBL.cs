using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class ExternoPerfilBL
    {
        private Repository _repositorio;

        public ExternoPerfilBL()
        {
            _repositorio = new Repository();
        }
        public Respuesta EditExternoPerfil(ExternoPerfil obj)
        {
            return _repositorio.EditExternoPerfil(obj);
        }
        public Respuesta ElimExternoPerfil(int Id)
        {
            return _repositorio.ElimExternoPerfil(Id);
        }
    }
}
