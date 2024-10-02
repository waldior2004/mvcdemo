using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class DetalleCotizacionBL
    {
        private Repository _repositorio;

        public DetalleCotizacionBL()
        {
            _repositorio = new Repository();
        }
        public DetalleCotizacion ObtDetalleCotizacion(int Id)
        {
            return _repositorio.ObtDetalleCotizacion(Id);
        }
        public Respuesta EditDetalleCotizacion(DetalleCotizacion obj)
        {
            return _repositorio.EditDetalleCotizacion(obj);
        }
        public Respuesta ElimDetalleCotizacion(int Id)
        {
            return _repositorio.ElimDetalleCotizacion(Id);
        }
    }
}
