using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Linq;

namespace com.msc.infraestructure.biz
{
    public class TarifarioBL
    {
        private Repository _repositorio;

        public TarifarioBL()
        {
            _repositorio = new Repository();
        }

        public List<Tarifario> ObtTarifario()
        {
            return _repositorio.ObtTarifario();
        }

        public Tarifario ObtTarifario(int Id)
        {
            return _repositorio.ObtTarifario(Id);
        }
        public Respuesta EditTarifario(Tarifario obj)
        {
            if (obj.Id == 0)
            {
                Tabla objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                 where p.Codigo == "001"
                                 select p).FirstOrDefault();
                obj.IdEstado = objEstado.Id;
            }

            return _repositorio.EditTarifario(obj);
        }

        public Respuesta ElimTarifario(int Id)
        {
            return _repositorio.ElimTarifario(Id);
        }
    }
}
