using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class CondEspeCliDetalleBL
    {
        private Repository _repositorio;

        public CondEspeCliDetalleBL()
        {
            _repositorio = new Repository();
        }
        public CondEspeCliDetalle ObtCondEspeCliDetalle(int Id)
        {
            return _repositorio.ObtCondEspeCliDetalle(Id);
        }
        public Respuesta EditCondEspeCliDetalle(CondEspeCliDetalle obj)
        {
            return _repositorio.EditCondEspeCliDetalle(obj);
        }
        public Respuesta ElimCondEspeCliDetalle(int Id)
        {
            return _repositorio.ElimCondEspeCliDetalle(Id);
        }
    }
}
