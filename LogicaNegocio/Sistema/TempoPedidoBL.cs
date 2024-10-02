using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class TempoPedidoBL
    {
        private Repository _repositorio;

        public TempoPedidoBL()
        {
            _repositorio = new Repository();
        }
        public Respuesta EditTempoPedido(TempoPedido obj)
        {
            return _repositorio.EditTempoPedido(obj);
        }
    }
}
