using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class OrdenCompraDocBL
    {
        private Repository _repositorio;

        public OrdenCompraDocBL()
        {
            _repositorio = new Repository();
        }
        public Documento ObtOrdenCompraDocNombre(int Id)
        {
            return _repositorio.ObtOrdenCompraDocNombre(Id);
        }
        public Respuesta EditOrdenCompraDoc(OrdenCompraDoc obj)
        {
            return _repositorio.EditOrdenCompraDoc(obj);
        }
        public Respuesta ElimOrdenCompraDoc(int Id)
        {
            return _repositorio.ElimOrdenCompraDoc(Id);
        }
    }
}
