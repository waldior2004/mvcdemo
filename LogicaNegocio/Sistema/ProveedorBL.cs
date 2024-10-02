using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class ProveedorBL
    {
        private Repository _repositorio;

        public ProveedorBL()
        {
            _repositorio = new Repository();
        }

        public List<Proveedor> ObtProveedor()
        {
            return _repositorio.ObtProveedor();
        }

        public List<Proveedor> ObtAllProveedorTarifaProducto(string desc, int id)
        {
            return _repositorio.ObtAllProveedorTarifaProducto(desc, id);
        }

        public List<Proveedor> ObtAllProveedor(string desc)
        {
            return _repositorio.ObtAllProveedor(desc);
        }

        public Proveedor ObtProveedor(int Id)
        {
            return _repositorio.ObtProveedor(Id);
        }

        public Respuesta EditProveedor(Proveedor obj)
        {
            return _repositorio.EditProveedor(obj);
        }

        public Respuesta ElimProveedor(int Id)
        {
            return _repositorio.ElimProveedor(Id);
        }
    }
}
