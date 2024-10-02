using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using System.Linq;
using System.Collections;
using System;
using com.msc.infraestructure.utils;

namespace com.msc.infraestructure.biz
{
    public class TarifaCEBL
    {
        private Repository _repositorio;

        public TarifaCEBL()
        {
            _repositorio = new Repository();
        }

        public List<TarifaCE> ObtTarifaCE()
        {
            return _repositorio.ObtTarifaCE();
        }

        public List<TarifaCE> ObtTarifaHistoricoCE()
        {
            return _repositorio.ObtTarifaHistoricoCE();
        }

        public TarifaCE ObtTarifaCE(int Id)
        {
            return _repositorio.ObtTarifaCE(Id);
        }

        public Respuesta EditTarifaCE(TarifaCE obj)
        {
            try
            {
                if (obj.Id == 0)
                {
                    //Al registrar colocar el estado 
                    var objEstado = (from p in _repositorio.ObtTablaGrupo("004")
                                     where p.Codigo == "001"
                                     select p).FirstOrDefault();

                    if (objEstado == null)
                        throw new NotImplementedException("No se pudo obtener el estado de registro de la tarifa.");
                    else
                        obj.IdEstado = objEstado.Id;

                    var respT = _repositorio.EditTarifaCE(obj);

                    if (respT.Id == 0)
                    {
                        var ojTari = _repositorio.ObtTarifaCE(Convert.ToInt32(respT.Metodo));

                        var lstCorreos = (from p in _repositorio.ObtTablaGrupo("012")
                                          select p.Descripcion).ToList();
                        if (lstCorreos == null)
                            throw new NotImplementedException("No se pudo obtener los correos de registro de tarifa.");
                        else
                            Mailing.SendRegistroTarifaCE(lstCorreos, ojTari);
                    }

                    return respT;
                }
                else
                    return _repositorio.EditTarifaCE(obj);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return MyException.OnException(ex);
            }
        }

        public Respuesta ElimTarifaCE(int Id)
        {
            return _repositorio.ElimTarifaCE(Id);
        }

        public Respuesta AprobarTarifaCE(TarifaCE obj)
        {
            try
            {
                //Al aprobar colocar el estado 
                var objEstado = (from p in _repositorio.ObtTablaGrupo("004")
                                    where p.Codigo == "002"
                                    select p).FirstOrDefault();

                if (objEstado == null)
                    throw new NotImplementedException("No se pudo obtener el estado de la tarifa.");
                else
                    obj.IdEstado = objEstado.Id;

                var respT = _repositorio.EditTarifaCE(obj);

                if (respT.Id == 0)
                {
                    var lstCorreos = (from p in _repositorio.ObtTablaGrupo("005")
                                      select p.Descripcion).ToList();
                    if (lstCorreos == null)
                        throw new NotImplementedException("No se pudo obtener los correos de aprobación.");
                    else
                        Mailing.SendAprobacionTarifaCE(lstCorreos, obj);
                }

                return respT;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return MyException.OnException(ex);
            }
        }
    }
}
