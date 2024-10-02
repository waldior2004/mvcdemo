using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class TablaBL
    {
        private Repository _repositorio;

        public TablaBL()
        {
            _repositorio = new Repository();
        }

        public List<Tabla> ObtTabla()
        {
            return _repositorio.ObtTabla();
        }

        public Tabla ObtTabla(int Id)
        {
            return _repositorio.ObtTabla(Id);
        }

        public List<Tabla> ObtTablaGrupo(string Codigo)
        {
            return _repositorio.ObtTablaGrupo(Codigo);
        }
        public Respuesta EditTabla(Tabla obj)
        {
            return _repositorio.EditTabla(obj);
        }

        public Respuesta ElimTabla(int Id)
        {
            return _repositorio.ElimTabla(Id);
        }
    }
}
