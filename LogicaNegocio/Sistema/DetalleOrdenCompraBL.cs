using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class DetalleOrdenCompraBL
    {
        private Repository _repositorio;

        public DetalleOrdenCompraBL()
        {
            _repositorio = new Repository();
        }
        public DetalleOrdenCompra ObtDetalleOrdenCompra(int Id)
        {
            return _repositorio.ObtDetalleOrdenCompra(Id);
        }
        public Respuesta EditDetalleOrdenCompra(DetalleOrdenCompra obj)
        {
            return _repositorio.EditDetalleOrdenCompra(obj);
        }
        public Respuesta ElimDetalleOrdenCompra(int Id)
        {
            return _repositorio.ElimDetalleOrdenCompra(Id);
        }
    }
}
