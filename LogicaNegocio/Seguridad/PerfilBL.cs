using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Collections.Generic;

namespace com.msc.infraestructure.biz
{
    public class PerfilBL
    {
        private Repository _repositorio;

        public PerfilBL()
        {
            _repositorio = new Repository();
        }

        public List<Perfil> ObtPerfil()
        {
            return _repositorio.ObtPerfil();
        }

        public Perfil ObtPerfil(int Id)
        {
            return _repositorio.ObtPerfil(Id);
        }

        public Respuesta EditPerfil(Perfil obj)
        {
            return _repositorio.EditPerfil(obj);
        }

        public Respuesta ElimPerfil(int Id)
        {
            return _repositorio.ElimPerfil(Id);
        }
    }
}
