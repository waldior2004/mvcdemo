using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class CondEspeCliDocBL
    {
        private Repository _repositorio;

        public CondEspeCliDocBL()
        {
            _repositorio = new Repository();
        }
        public Documento ObtCondEspeCliDocNombre(int Id)
        {
            return _repositorio.ObtCondEspeCliDocNombre(Id);
        }
        public Respuesta EditCondEspeCliDoc(CondEspeCliDoc obj)
        {
            return _repositorio.EditCondEspeCliDoc(obj);
        }
        public Respuesta ElimCondEspeCliDoc(int Id)
        {
            return _repositorio.ElimCondEspeCliDoc(Id);
        }
    }
}
