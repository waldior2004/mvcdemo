using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class BitacoraBL
    {
        private Repository _repositorio;

        public BitacoraBL()
        {
            _repositorio = new Repository();
        }

        public List<Bitacora> ObtBitacora(int Id, string Tabla)
        {
            return _repositorio.ObtBitacora(Id, Tabla);
        }

        public Respuesta EditBitacora(Bitacora obj)
        {
            return _repositorio.EditBitacora(obj);
        }
    }
}
