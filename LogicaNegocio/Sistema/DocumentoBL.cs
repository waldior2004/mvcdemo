using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class DocumentoBL
    {
        private Repository _repositorio;

        public DocumentoBL()
        {
            _repositorio = new Repository();
        }

        public Respuesta EditDocumento(Documento obj)
        {
            return _repositorio.EditDocumento(obj);
        }

        public Respuesta ElimDocumento(int Id)
        {
            return _repositorio.ElimDocumento(Id);
        }
    }
}
