using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Collections.Generic;

namespace com.msc.infraestructure.biz
{
    public class ContactoProveedorBL
    {
        private Repository _repositorio;

        public ContactoProveedorBL()
        {
            _repositorio = new Repository();
        }
        public ContactoProveedor ObtContactoProveedor(int Id)
        {
            return _repositorio.ObtContactoProveedor(Id);
        }
        public Respuesta EditContactoProveedor(ContactoProveedor obj)
        {
            return _repositorio.EditContactoProveedor(obj);
        }
        public Respuesta ElimContactoProveedor(int Id)
        {
            return _repositorio.ElimContactoProveedor(Id);
        }
    }
}
