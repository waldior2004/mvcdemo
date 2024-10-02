using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Linq;

namespace com.msc.infraestructure.biz
{
    public class CotizacionBL
    {
        private Repository _repositorio;

        public CotizacionBL()
        {
            _repositorio = new Repository();
        }

        public List<Cotizacion> ObtAllCotizacion(string desc)
        {
            return _repositorio.ObtAllCotizacion(desc);
        }

        public List<Cotizacion> ObtCotizacion()
        {
            return _repositorio.ObtCotizacion();
        }

        public Cotizacion ObtCotizacion(int Id)
        {
            return _repositorio.ObtCotizacion(Id);
        }

        public Respuesta EditCotizacion(Cotizacion obj)
        {
            if (obj.Id == 0)
            {
                //estado borrador
               Tabla objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                             where p.Codigo == "002"
                             select p).FirstOrDefault();
               
               obj.IdEstado = objEstado.Id;
               obj.Codigo = _repositorio.GeneraCodigoCotizacion();
            }
            return _repositorio.EditCotizacion(obj);
        }

        public List<DetalleCotizacion> AjustarCotizacion(int IdProv, int IdCotizacion)
        {
            var lstDetalle = _repositorio.ObtDetallexCotizacion(IdCotizacion);

            foreach(var item in lstDetalle)
            {
                var objT = _repositorio.ObtTarifarioxProveedorProducto(IdProv, item.IdProducto);
                item.IdTarifario = objT.Id;
                item.Precio = objT.Precio;
                item.Total = item.Cantidad * objT.Precio;
                item.Observacion = string.Empty;
                var _respDet = _repositorio.EditDetalleCotizacion(item);
            }

            return lstDetalle;
        }

        public Respuesta EnviarCotizacion(int Id)
        {
            Respuesta resp = new Respuesta();
            var lstDetalleCotizaciones = _repositorio.ObtDetallexCotizacion(Id);
            bool errores = lstDetalleCotizaciones.Exists(p => p.Cantidad == 0);
            if (errores)
                resp = MessagesApp.BackAppMessage(MessageCode.CotizacionDetalleErrores);
            else
            {
                 var objCot = _repositorio.ObtCotizacion(Id);
                 Tabla objEstado = (from p in _repositorio.ObtTablaGrupo("015")
                             where p.Codigo == "008"
                             select p).FirstOrDefault();
                 objCot.IdEstado = objEstado.Id;
                 resp = _repositorio.EditCotizacion(objCot);

                //envio de correos
                 var lstConta = _repositorio.ObtGetContactosByProveedor(objCot.IdProveedor);
                 var lstCorreos = (from p in lstConta
                                   select p.Correo).ToList();
                 Mailing.SendEnviarCotizacionProveedores(lstCorreos, objCot);
            }

            return resp;
        }

        public Respuesta ElimCotizacion(int Id)
        {
            return _repositorio.ElimCotizacion(Id);
        }

        public Respuesta GenerarOC(int Id, string usuario)
        {
            Respuesta resp = new Respuesta();

            var objCot = _repositorio.ObtCotizacion(Id);

            var codOC = _repositorio.GeneraCodigoOrdenCompra();

            //decimal SubTotal = 0;

            //foreach (var item in objCot.DetalleCotizaciones)
            //{
            //    SubTotal += item.Cantidad * item.Precio;
            //}

            //decimal Igv = Convert.ToDecimal(0.18) * SubTotal;
            //decimal Total = SubTotal + Igv;

            OrdenCompra objOC = new OrdenCompra
            {
                Id = 0,
                Codigo = codOC,
                IdCotizacion = Id,
                IdProveedor = objCot.IdProveedor,
                FechaRegistro = DateTime.Now,
                FlagAprobacion = 0,
                Observacion = "O/C Automática",
                User = usuario,
                SubTotal = 0,
                Igv = 0,
                Total = 0
            };

            resp = _repositorio.EditOrdenCompra(objOC);

            if (resp.Id == 0)
            {
                var lstDetalles = objCot.DetalleCotizaciones;

                foreach (var item in lstDetalles)
                {
                    DetalleOrdenCompra objDet = new DetalleOrdenCompra
                    {
                        Id = 0,
                        IdOrdenCompra = Convert.ToInt32(resp.Metodo),
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Observacion = item.Observacion,
                        Total = item.Cantidad * item.Precio
                    };

                    var respDeta = _repositorio.EditDetalleOrdenCompra(objDet);
                }

            }

            return resp;
        }

    }
}
