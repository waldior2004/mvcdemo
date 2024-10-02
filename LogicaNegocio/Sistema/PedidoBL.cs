using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Linq;

namespace com.msc.infraestructure.biz
{
    public class PedidoBL
    {
        private Repository _repositorio;

        public PedidoBL()
        {
            _repositorio = new Repository();
        }

        public Respuesta GenerarCotizacion(int Id, int IdPadre)
        {
            try
            {
                Respuesta resp = new Respuesta();

                var objPedido = _repositorio.ObtPedido(IdPadre);

                var codCoti = _repositorio.GeneraCodigoCotizacion();

                Tabla objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                   where p.Codigo == "002"
                                   select p).FirstOrDefault();

                Cotizacion objCot = new Cotizacion
                {
                    Id = 0,
                    Codigo = codCoti,
                    IdPedido = IdPadre,
                    IdProveedor = Id,
                    FechaCotizacion = DateTime.Now,
                    FechaEntrega = null,
                    IdEstado = objEstado.Id,
                    Observacion = "Cotización Automática"
                };

                resp = _repositorio.EditCotizacion(objCot);

                if (resp.Id == 0)
                {
                    var lstDetalles = objPedido.DetallePedidos;

                    foreach (var item in lstDetalles)
                    {
                        var objTar = _repositorio.ObtTarifarioxProveedorProducto(Id, item.IdProducto);
                        DetalleCotizacion objDet = new DetalleCotizacion
                        {
                            Id = 0,
                            IdCotizacion = Convert.ToInt32(resp.Metodo),
                            IdProducto = item.IdProducto,
                            IdTarifario = objTar.Id,
                            Cantidad = item.Cantidad,
                            Precio = objTar.Precio,
                            Observacion = item.Observaciones,
                            Total = item.Cantidad * objTar.Precio
                        };

                        var respDeta = _repositorio.EditDetalleCotizacion(objDet);
                    }

                    Tabla objEstadoPed = (from p in _repositorio.ObtTablaGrupo("015")
                                          where p.Codigo == "005"
                                          select p).FirstOrDefault();

                    objPedido.IdEstado = objEstadoPed.Id;

                    var respPe = _repositorio.EditPedido(objPedido);
                }

                return resp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return MyException.OnException(ex);
            }
        }

        public List<Pedido> ObtAllPedido(string desc)
        {
            return _repositorio.ObtAllPedido(desc);
        }

        public List<Tarifario> ObtPedidoMapProductos(int Id)
        {
            List<Tarifario> lstOUT = _repositorio.ObtPedidoByTempo(Id);

            if (lstOUT.Count == 0)
            {
                lstOUT = new List<Tarifario>();
                var lstDetallePeds = _repositorio.ObtPedido(Id).DetallePedidos;
                foreach (var item in lstDetallePeds)
                {
                    var tarifario = _repositorio.ObtTarifarioMinPrecioProducto(item.IdProducto);
                    tarifario.Cantidad = item.Cantidad;
                    lstOUT.Add(tarifario);
                }
            }
            return lstOUT;
        }

        public Respuesta ValidarPedido(string Id, string Correo, string Tipo, string Comentario, string Usuario)
        {
            try
            {
                List<string> lstCorreos = new List<string>();
                lstCorreos.Add(Correo);

                var objPedido = _repositorio.ObtPedido(Convert.ToInt32(Id));
                var objRep = _repositorio.ObtAreaSolicitante(objPedido.IdAreaSolicitante);
                if (objRep != null)
                {
                    lstCorreos.Add(objRep.CorreoRep);
                }

                Tabla objEstado;
                if(Tipo == "Aprobado")
                    objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                 where p.Codigo == "004"
                                 select p).FirstOrDefault();
                else if(Tipo == "Rechazado")
                    objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                 where p.Codigo == "007"
                                 select p).FirstOrDefault();
                else
                    objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                 where p.Codigo == "006"
                                 select p).FirstOrDefault();

                objPedido.IdEstado = objEstado.Id;
                objPedido.ComentAprobador = Comentario;
                objPedido.User = Usuario;
                var objResp = _repositorio.EditPedido(objPedido);

                Mailing.SendValidarPedido(lstCorreos, objPedido);

                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return MyException.OnException(ex);
            }
        }

        public List<Pedido> ObtPedidoPorValidar()
        {
            return _repositorio.ObtPedidoPorValidar();
        }

        public List<Pedido> ObtPedidoPorCotizar()
        {
            return _repositorio.ObtPedidoPorCotizar();
        }

        public List<Pedido> ObtPedido()
        {
            return _repositorio.ObtPedido();
        }

        public Pedido ObtPedido(int Id)
        {
            return _repositorio.ObtPedido(Id);
        }

        public Respuesta EditPedido(Pedido obj)
        {
            try
            {
                    Tabla objEstado;
                    bool ChkCorreo = true;
                    if (obj.FlagAprobacion == 1)
                    {
                        //estado pend aprobar
                        objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                     where p.Codigo == "003"
                                     select p).FirstOrDefault();
                    }
                    else
                    {
                        //estado borrador
                        objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                     where p.Codigo == "002"
                                     select p).FirstOrDefault();
                        ChkCorreo = false;
                    }

                    obj.IdEstado = objEstado.Id;

                    if (obj.Id == 0)
                    {
                        obj.Codigo = _repositorio.GeneraCodigoPedido(obj.IdSucursal, obj.IdAreaSolicitante);
                    }
                    else
                    {
                        ChkCorreo = false;
                    }

                    var resp = _repositorio.EditPedido(obj);

                    if (resp.Id == 0 && ChkCorreo)
                    {
                        List<string> lstCorreos = new List<string>();
                        var objPedido = _repositorio.ObtPedido(Convert.ToInt32(resp.Metodo));
                        var objRep = _repositorio.ObtAreaSolicitante(objPedido.IdAreaSolicitante);
                        if (objRep != null)
                        {
                            lstCorreos.Add(objRep.CorreoRep);
                        }
                        Mailing.SendRegistrarPedido(lstCorreos, objPedido);
                    }

                    return resp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return MyException.OnException(ex);
            }
        }

        public Respuesta ElimPedido(int Id)
        {
            return _repositorio.ElimPedido(Id);
        }
    }
}
