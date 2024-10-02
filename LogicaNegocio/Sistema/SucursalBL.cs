using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class SucursalBL
    {
        private Repository _repositorio;

        public SucursalBL()
        {
            _repositorio = new Repository();
        }

        public List<Sucursal> ObtSucursal()
        {
            return _repositorio.ObtSucursal();
        }

        public Sucursal ObtSucursal(int Id)
        {
            return _repositorio.ObtSucursal(Id);
        }

        //public List<Sucursal> ObtSucursalxEmpresa(int Id)
        //{
        //    return _repositorio.ObtSucursalxEmpresa(Id);
        //}

        public Respuesta EditSucursal(Sucursal obj)
        {
            return _repositorio.EditSucursal(obj);
        }

        public Respuesta ElimSucursal(int Id)
        {
            return _repositorio.ElimSucursal(Id);
        }
    }
}
