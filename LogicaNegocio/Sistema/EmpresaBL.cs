using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class EmpresaBL
    {
        private Repository _repositorio;

        public EmpresaBL()
        {
            _repositorio = new Repository();
        }

        public List<Empresa> ObtEmpresa()
        {
            return _repositorio.ObtEmpresa();
        }

        public Empresa ObtEmpresa(int Id)
        {
            return _repositorio.ObtEmpresa(Id);
        }

        public Respuesta EditEmpresa(Empresa obj)
        {
            return _repositorio.EditEmpresa(obj);
        }

        public Respuesta ElimEmpresa(int Id)
        {
            return _repositorio.ElimEmpresa(Id);
        }
    }
}
