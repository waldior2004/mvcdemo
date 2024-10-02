using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class UbigeoBL
    {
        private Repository _repositorio;

        public UbigeoBL()
        {
            _repositorio = new Repository();
        }

        public List<Tabla> ObtPais()
        {
            return _repositorio.ObtPais();
        }

        public List<Ubigeo> ObtDepartamento(int IdPais)
        {
            return _repositorio.ObtDepartamento(IdPais);
        }

        public List<Ubigeo> ObtProvincia(int IdDep)
        {
            var objDep = _repositorio.ObtUbigeo(IdDep);
            return _repositorio.ObtProvincia(objDep.IdPais, objDep.CodDepartamento);
        }

        public List<Ubigeo> ObtDistrito(int IdProv)
        {
            var objProv = _repositorio.ObtUbigeo(IdProv);
            return _repositorio.ObtDistrito(objProv.IdPais, objProv.CodDepartamento, objProv.CodProvincia);
        }
    }
}
