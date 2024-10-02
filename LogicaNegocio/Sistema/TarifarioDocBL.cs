using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class TarifarioDocBL
    {
        private Repository _repositorio;

        public TarifarioDocBL()
        {
            _repositorio = new Repository();
        }
        public Documento ObtTarifarioDocNombre(int Id)
        {
            return _repositorio.ObtTarifarioDocNombre(Id);
        }
        public Respuesta EditTarifarioDoc(TarifarioDoc obj)
        {
            return _repositorio.EditTarifarioDoc(obj);
        }
        public Respuesta ElimTarifarioDoc(int Id)
        {
            return _repositorio.ElimTarifarioDoc(Id);
        }
    }
}
