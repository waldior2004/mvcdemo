using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Collections.Generic;

namespace com.msc.infraestructure.biz
{
    public class DatoComercialBL
    {
        private Repository _repositorio;

        public DatoComercialBL()
        {
            _repositorio = new Repository();
        }
        public DatoComercial ObtDatoComercial(int Id)
        {
            return _repositorio.ObtDatoComercial(Id);
        }
        public Respuesta EditDatoComercial(DatoComercial obj)
        {
            return _repositorio.EditDatoComercial(obj);
        }
        public Respuesta ElimDatoComercial(int Id)
        {
            return _repositorio.ElimDatoComercial(Id);
        }
    }
}
