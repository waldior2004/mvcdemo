using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Collections.Generic;

namespace com.msc.infraestructure.biz
{
    public class TareaBL
    {
        private Repository _repositorio;

        public TareaBL()
        {
            _repositorio = new Repository();
        }
        public List<Tarea> ObtTarea()
        {
            return _repositorio.ObtTarea();
        }
        public Tarea ObtTarea(int Id)
        {
            return _repositorio.ObtTarea(Id);
        }
        public Respuesta EditTarea(Tarea obj)
        {
            return _repositorio.EditTarea(obj);
        }
        public Respuesta ElimTarea(int Id)
        {
            return _repositorio.ElimTarea(Id);
        }
    }
}
