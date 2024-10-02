using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Linq;
using com.msc.infraestructure.entities.impresion;

namespace com.msc.infraestructure.biz
{
    public class OrdenCompraBL
    {
        private Repository _repositorio;

        public OrdenCompraBL()
        {
            _repositorio = new Repository();
        }

        public List<OrdenCompra> ObtAllOrdenCompra(string desc)
        {
            return _repositorio.ObtAllOrdenCompra(desc);
        }

        public List<OrdenCompra> ObtOrdenCompra()
        {
            return _repositorio.ObtOrdenCompra();
        }

        public List<OrdenCompra> ObtOrdenCompraPorValidar()
        {
            return _repositorio.ObtOrdenCompraPorValidar();
        }

        public OrdenCompra ObtOrdenCompra(int Id)
        {
            return _repositorio.ObtOrdenCompra(Id);
        }

        public Respuesta ValidarOrdenCompra(string Id, string Correo, string Tipo, string Comentario, string Usuario)
        {
            try
            {
                List<string> lstCorreos = new List<string>();
                lstCorreos.Add(Correo);

                var objOrden = _repositorio.ObtOrdenCompra(Convert.ToInt32(Id));
                var obj01 = _repositorio.ObtCotizacion(objOrden.IdCotizacion);
                var obj02 = _repositorio.ObtPedido(Convert.ToInt32(obj01.IdPedido));
                var objRep = _repositorio.ObtAreaSolicitante(obj02.IdAreaSolicitante);
                if (objRep != null)
                {
                    lstCorreos.Add(objRep.CorreoRep);
                }

                Tabla objEstado;
                if (Tipo == "Aprobado")
                    objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                 where p.Codigo == "004"
                                 select p).FirstOrDefault();
                else if (Tipo == "Rechazado")
                    objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                 where p.Codigo == "007"
                                 select p).FirstOrDefault();
                else
                    objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                 where p.Codigo == "006"
                                 select p).FirstOrDefault();

                objOrden.IdEstado = objEstado.Id;
                objOrden.ComentAprobador = Comentario;
                objOrden.UserAprob = Usuario;
                var objResp = _repositorio.EditOrdenCompra(objOrden);

                Mailing.SendValidarOrdenCompra(lstCorreos, objOrden);

                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return MyException.OnException(ex);
            }
        }

        public Respuesta EditOrdenCompra(OrdenCompra obj)
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
               obj.Codigo = _repositorio.GeneraCodigoOrdenCompra();
               obj.SubTotal = 0;
               obj.Igv = 0;
               obj.Total = 0;
            }
            else
            {
                ChkCorreo = false;
            }

            var resp = _repositorio.EditOrdenCompra(obj);

            if (resp.Id == 0 && ChkCorreo)
            {
                List<string> lstCorreos = new List<string>();
                var obj00 = _repositorio.ObtOrdenCompra(Convert.ToInt32(resp.Metodo));
                var obj01 = _repositorio.ObtCotizacion(obj00.IdCotizacion);
                var obj02 = _repositorio.ObtPedido(Convert.ToInt32(obj01.IdPedido));
                var objRep = _repositorio.ObtAreaSolicitante(obj02.IdAreaSolicitante);
                if (objRep != null)
                {
                    lstCorreos.Add(objRep.CorreoRep);
                    Mailing.SendRegistrarOrdenCompra(lstCorreos, obj00);
                }
            }

            return resp;
        }

        public Respuesta ElimOrdenCompra(int Id)
        {
            return _repositorio.ElimOrdenCompra(Id);
        }

        public Respuesta EnviarOrdenCompra(int Id)
        {
            Respuesta resp = new Respuesta();
            var lstDetalleOrdenCompraes = _repositorio.ObtDetallexOrdenCompra(Id);
            bool errores = lstDetalleOrdenCompraes.Exists(p => p.Cantidad == 0);
            if (errores)
                resp = MessagesApp.BackAppMessage(MessageCode.OrdenCompraDetalleErrores);
            else
            {
                var objOC = _repositorio.ObtOrdenCompra(Id);
                Tabla objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                                   where p.Codigo == "008"
                                   select p).FirstOrDefault();
                objOC.IdEstado = objEstado.Id;
                resp = _repositorio.EditOrdenCompra(objOC);

                if (resp.Id == 0)
                {
                    var objC = _repositorio.ObtCotizacion(objOC.IdCotizacion);
                    var objPed = _repositorio.ObtPedido(Convert.ToInt32(objC.IdPedido));

                    var objEstadoProv = (from p in _repositorio.ObtTablaGrupo("024")
                                 where p.Codigo == "01"
                                 select p).FirstOrDefault();

                    var objTipoProvision = (from p in _repositorio.ObtTablaGrupo("022")
                                            where p.Codigo == "04"
                                            select p).FirstOrDefault();

                    var objCuentaContable = (from p in _repositorio.ObtTablaGrupo("023")
                                             where p.Codigo == "915005"
                                             select p).FirstOrDefault();

                    var objMoneda = (from p in _repositorio.ObtTablaGrupo("003")
                                     where p.Codigo == "001"
                                     select p).FirstOrDefault();

                    //Registro de Provision
                    var objProvision = new Provision
                    {
                        Id = 0,
                        Codigo = _repositorio.GeneraCodigoProvisionGasto(),
                        IdEmpresa = objPed.IdEmpresa,
                        IdSucursal = objPed.IdSucursal,
                        IdEstado = objEstadoProv.Id,
                        IdTipoProvision = objTipoProvision.Id,
                        IdCuentaContable = objCuentaContable.Id,
                        IdProveedor = objOC.IdProveedor,
                        IdMoneda = objMoneda.Id,
                        IdOrdenCompra = objOC.Id,
                        User = objOC.User,
                        Monto = objOC.Total,
                        Concepto = "Referencia O/C " + objOC.Codigo,
                        MesProv = DateTime.Now.Month,
                        AnioProv = DateTime.Now.Year,
                        MesServ = DateTime.Now.Month,
                        AnioServ = DateTime.Now.Year,
                        ComentarioUno = "Pedido: " + objPed.Codigo + ", Cotización: " + objC.Codigo,
                        ComentarioDos = ""
                    };

                    resp = _repositorio.EditProvision(objProvision);

                    if (resp.Id == 0)
                    {
                        //envio de correos
                        var lstConta = _repositorio.ObtGetContactosByProveedor(objOC.IdProveedor);
                        var lstCorreos = (from p in lstConta
                                          select p.Correo).ToList();
                        Mailing.SendEnviarOrdenCompraProveedores(lstCorreos, objOC);
                    }
                }
            }

            return resp;
        }

        public Respuesta FactOrdenCompra(int Id, string Factura)
        {
            return _repositorio.FactOrdenCompra(Id, Factura);
        }

        public List<IMP_ORDENCOMPRA> ImprimirOrdenCompra(int Id)
        {
            return _repositorio.ImprimirOrdenCompra(Id);
        }
    }
}