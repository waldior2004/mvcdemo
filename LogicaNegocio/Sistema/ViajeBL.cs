using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.biz
{
    public class ViajeBL
    {
        private Repository _repositorio;

        public ViajeBL()
        {
            _repositorio = new Repository();
        }

        public List<Viaje> ObtAllViaje(string desc)
        {
            return _repositorio.ObtAllViaje(desc);
        }

        public List<Viaje> ObtViajexNave(string id, int port)
        {
            return _repositorio.ObtViajexNave(id, port);
        }
        
    }
}
