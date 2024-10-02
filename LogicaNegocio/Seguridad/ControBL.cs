using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class ControlBL
    {
        private Repository _repositorio;

        public ControlBL()
        {
            _repositorio = new Repository();
        }

        public List<Control> ObtControl()
        {
            return _repositorio.ObtControl();
        }

        public Control ObtControl(int Id)
        {
            return _repositorio.ObtControl(Id);
        }

        public Respuesta EditControl(Control obj)
        {
            return _repositorio.EditControl(obj);
        }

        public Respuesta ElimControl(int Id)
        {
            return _repositorio.ElimControl(Id);
        }
    }
}
