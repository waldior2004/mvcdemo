using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class PerfilControlBL
    {
        private Repository _repositorio;

        public PerfilControlBL()
        {
            _repositorio = new Repository();
        }

        public Respuesta EditPerfilControl(PerfilControl obj)
        {
            return _repositorio.EditPerfilControl(obj);
        }

        public Respuesta ElimPerfilControl(int Id)
        {
            return _repositorio.ElimPerfilControl(Id);
        }
    }
}
