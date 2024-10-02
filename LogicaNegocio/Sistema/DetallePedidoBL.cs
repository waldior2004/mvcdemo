using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class DetallePedidoBL
    {
        private Repository _repositorio;

        public DetallePedidoBL()
        {
            _repositorio = new Repository();
        }
        public DetallePedido ObtDetallePedido(int Id)
        {
            return _repositorio.ObtDetallePedido(Id);
        }
        public Respuesta EditDetallePedido(DetallePedido obj)
        {
            return _repositorio.EditDetallePedido(obj);
        }
        public Respuesta ElimDetallePedido(int Id)
        {
            return _repositorio.ElimDetallePedido(Id);
        }
    }
}
