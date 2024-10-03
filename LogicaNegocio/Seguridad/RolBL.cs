using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Collections.Generic;

namespace com.msc.infraestructure.biz
{
    public class RolBL
    {
        private Repository _repositorio;

        public RolBL()
        {
            _repositorio = new Repository();
        }

        public List<Rol> ObtRol()
        {
            return _repositorio.ObtRol();
        }

        public Rol ObtRol(int Id)
        {
            return _repositorio.ObtRol(Id);
        }

        public Respuesta EditRol(Rol obj)
        {
            return _repositorio.EditRol(obj);
        }

        public Respuesta ElimRol(int Id)
        {
            return _repositorio.ElimRol(Id);
        }
    }
}
