using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Collections.Generic;

namespace com.msc.infraestructure.biz
{
    public class ImpuestoProveedorBL
    {
        private Repository _repositorio;

        public ImpuestoProveedorBL()
        {
            _repositorio = new Repository();
        }
        public ImpuestoProveedor ObtImpuestoProveedor(int Id)
        {
            return _repositorio.ObtImpuestoProveedor(Id);
        }
        public Respuesta EditImpuestoProveedor(ImpuestoProveedor obj)
        {
            return _repositorio.EditImpuestoProveedor(obj);
        }
        public Respuesta ElimImpuestoProveedor(int Id)
        {
            return _repositorio.ElimImpuestoProveedor(Id);
        }
    }
}
