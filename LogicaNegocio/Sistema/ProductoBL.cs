using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class ProductoBL
    {
        private Repository _repositorio;

        public ProductoBL()
        {
            _repositorio = new Repository();
        }

        public List<Producto> ObtProducto()
        {
            return _repositorio.ObtProducto();
        }

        public List<Producto> ObtAllProductoxProveedor(string desc, int id)
        {
            return _repositorio.ObtAllProductoxProveedor(desc, id);
        }

        public List<Producto> ObtAllProducto(string desc)
        {
            return _repositorio.ObtAllProducto(desc);
        }

        public Producto ObtProducto(int Id)
        {
            return _repositorio.ObtProducto(Id);
        }

        public Respuesta EditProducto(Producto obj)
        {
            return _repositorio.EditProducto(obj);
        }

        public Respuesta ElimProducto(int Id)
        {
            return _repositorio.ElimProducto(Id);
        }
    }
}
