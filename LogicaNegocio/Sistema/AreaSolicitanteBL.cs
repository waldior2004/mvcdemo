using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class AreaSolicitanteBL
    {
        private Repository _repositorio;

        public AreaSolicitanteBL()
        {
            _repositorio = new Repository();
        }

        public List<AreaSolicitante> ObtAreaSolicitante()
        {
            return _repositorio.ObtAreaSolicitante();
        }

        //public List<AreaSolicitante> ObtAreaSolicitantexEmpresa(int Id)
        //{
        //    return _repositorio.ObtAreaSolicitantexEmpresa(Id);
        //}

        public AreaSolicitante ObtAreaSolicitante(int Id)
        {
            return _repositorio.ObtAreaSolicitante(Id);
        }

        public Respuesta EditAreaSolicitante(AreaSolicitante obj)
        {
            return _repositorio.EditAreaSolicitante(obj);
        }

        public Respuesta ElimAreaSolicitante(int Id)
        {
            return _repositorio.ElimAreaSolicitante(Id);
        }
    }
}
