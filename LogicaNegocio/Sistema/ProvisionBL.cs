using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System.Linq;
using System;

namespace com.msc.infraestructure.biz
{
    public class ProvisionBL
    {
        private Repository _repositorio;

        public ProvisionBL()
        {
            _repositorio = new Repository();
        }
        public List<Provision> ObtProvision()
        {
            return _repositorio.ObtProvision();
        }
        public Provision ObtProvision(int Id)
        {
            return _repositorio.ObtProvision(Id);
        }
        public Respuesta EditProvision(Provision obj)
        {
            try
            {
                Tabla objEstado;
                if (obj.Id == 0)
                {
                    obj.Codigo = _repositorio.GeneraCodigoProvisionGasto();
                    objEstado = (from p in _repositorio.ObtTablaGrupo("024")
                                 where p.Codigo == "01"
                                 select p).FirstOrDefault();
                    obj.IdEstado = objEstado.Id;
                }
               
                return _repositorio.EditProvision(obj);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return MyException.OnException(ex);
            }
        }
        public Respuesta ElimProvision(int Id)
        {
            return _repositorio.ElimProvision(Id);
        }
    }
}
