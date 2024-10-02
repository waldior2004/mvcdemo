using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class TarifaCEDocBL
    {
        private Repository _repositorio;

        public TarifaCEDocBL()
        {
            _repositorio = new Repository();
        }
        public Documento ObtTarifaCEDocNombre(int Id)
        {
            return _repositorio.ObtTarifaCEDocNombre(Id);
        }
        public Respuesta EditTarifaCEDoc(TarifaCEDoc obj)
        {
            return _repositorio.EditTarifaCEDoc(obj);
        }
        public Respuesta ElimTarifaCEDoc(int Id)
        {
            return _repositorio.ElimTarifaCEDoc(Id);
        }
    }
}
