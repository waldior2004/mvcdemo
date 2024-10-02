using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class TipoControlBL
    {
        private Repository _repositorio;

        public TipoControlBL()
        {
            _repositorio = new Repository();
        }

        public List<TipoControl> ObtTipoControl()
        {
            return _repositorio.ObtTipoControl();
        }

        public TipoControl ObtTipoControl(int Id)
        {
            return _repositorio.ObtTipoControl(Id);
        }

        public Respuesta EditTipoControl(TipoControl obj)
        {
            return _repositorio.EditTipoControl(obj);
        }

        public Respuesta ElimTipoControl(int Id)
        {
            return _repositorio.ElimTipoControl(Id);
        }
    }
}
