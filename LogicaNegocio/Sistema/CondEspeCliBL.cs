using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Linq;
using System.Collections;
using System;
using com.msc.infraestructure.utils;

namespace com.msc.infraestructure.biz
{
    public class CondEspeCliBL
    {
        private Repository _repositorio;

        public CondEspeCliBL()
        {
            _repositorio = new Repository();
        }

        public List<CondEspeCli> ObtCondEspeCli()
        {
            return _repositorio.ObtCondEspeCli();
        }

        public List<CondEspeCli> ObtCondEspeCliHistorico()
        {
            return _repositorio.ObtCondEspeCliHistorico();
        }

        public CondEspeCli ObtCondEspeCli(int Id)
        {
            return _repositorio.ObtCondEspeCli(Id);
        }

        public Respuesta EditCondEspeCli(CondEspeCli obj)
        {
            var boolResp = (obj.Id == 0 ? true : false);
            var respT = _repositorio.EditCondEspeCli(obj);

            if (boolResp && respT.Id == 0)
            {
                var lstCorreos = (from p in _repositorio.ObtTablaGrupo("009")
                                  select p.Descripcion).ToList();
                if (lstCorreos == null)
                    throw new NotImplementedException("No se pudo obtener los correos de registro de condición especial.");
                else
                    Mailing.SendRegistroCondiciónEspecial(lstCorreos, obj);
            }
            return respT;
        }

        public Respuesta ElimCondEspeCli(int Id)
        {
            return _repositorio.ElimCondEspeCli(Id);
        }

    }
}
