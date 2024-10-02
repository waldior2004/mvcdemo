using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class PaginaBL
    {
        private Repository _repositorio;

        public PaginaBL()
        {
            _repositorio = new Repository();
        }

        public List<Pagina> ObtPagina()
        {
            return _repositorio.ObtPagina();
        }

        public Pagina ObtPagina(int Id)
        {
            return _repositorio.ObtPagina(Id);
        }

        public Respuesta EditPagina(Pagina obj)
        {
            return _repositorio.EditPagina(obj);
        }

        public Respuesta ElimPagina(int Id)
        {
            return _repositorio.ElimPagina(Id);
        }
    }
}
