using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    
    public partial class Repository
    {
        private const decimal _igv18 = 0.18M;

        public List<DetalleOrdenCompra> ObtDetallexOrdenCompra(int Id)
        {
            List<DetalleOrdenCompra> obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.DetalleOrdenCompras.Include("Producto")
                           where p.IdOrdenCompra == Id && p.AudActivo == 1
                           select p).ToList();

                    foreach (var item in obj)
                    {

                        var objUniMed = (from p in context.Productos
                                         join q in context.Tablas on p.IdUnidadMedida equals q.Id
                                         where p.Id == item.IdProducto && p.AudActivo == 1 && q.AudActivo == 1
                                         select q).FirstOrDefault();

                        item.Producto.UnidadMedida = objUniMed;
                    }

                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public DetalleOrdenCompra ObtDetalleOrdenCompra(int Id)
        {
            DetalleOrdenCompra obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.DetalleOrdenCompras.Include("Producto")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    var objUniMed = (from p in context.Productos
                                     join q in context.Tablas on p.IdUnidadMedida equals q.Id
                                     where p.Id == obj.IdProducto && p.AudActivo == 1 && q.AudActivo == 1
                                     select q).FirstOrDefault();

                    obj.Producto.UnidadMedida = objUniMed;

                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditDetalleOrdenCompra(DetalleOrdenCompra obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        var exists = (from p in context.DetalleOrdenCompras
                                      where p.IdProducto == obj.IdProducto && p.AudActivo == 1 && p.IdOrdenCompra == obj.IdOrdenCompra
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            obj.OrdenCompra = null;
                            obj.Producto = null;
                            obj.AudActivo = 1;
                            context.DetalleOrdenCompras.Add(obj);
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.SaveChanges();

                            var _subtotal = (from p in context.DetalleOrdenCompras
                                          where p.IdOrdenCompra == obj.IdOrdenCompra && p.AudActivo == 1
                                          select p.Total).Sum();

                            var _igv = _subtotal * _igv18;

                            var _total = _subtotal + _igv;

                            var _objOC = (from p in context.OrdenCompras
                                          where p.Id == obj.IdOrdenCompra && p.AudActivo == 1
                                          select p).FirstOrDefault();

                            _objOC.Cotizacion = null;
                            _objOC.Proveedor = null;
                            _objOC.Estado = null;
                            _objOC.SubTotal = _subtotal;
                            _objOC.Igv = _igv;
                            _objOC.Total = _total;
                            context.SaveChanges();

                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.DetalleOrdenCompras
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists != null)
                        {

                            var subexists = (from p in context.DetalleOrdenCompras
                                             where p.IdProducto == obj.IdProducto && p.AudActivo == 1 && p.IdOrdenCompra == obj.IdOrdenCompra && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (subexists == null)
                            {
                                exists.Cantidad = obj.Cantidad;
                                exists.Precio = obj.Precio;
                                exists.Total = obj.Total;
                                exists.Observacion = obj.Observacion;
                                exists.AudUpdate = DateTime.Now;
                                objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                                context.SaveChanges();

                                var _subtotal = (from p in context.DetalleOrdenCompras
                                                 where p.IdOrdenCompra == obj.IdOrdenCompra && p.AudActivo == 1
                                                 select p.Total).Sum();

                                var _igv = _subtotal * _igv18;

                                var _total = _subtotal + _igv;

                                var _objOC = (from p in context.OrdenCompras
                                              where p.Id == obj.IdOrdenCompra && p.AudActivo == 1
                                              select p).FirstOrDefault();

                                _objOC.Cotizacion = null;
                                _objOC.Proveedor = null;
                                _objOC.Estado = null;
                                _objOC.SubTotal = _subtotal;
                                _objOC.Igv = _igv;
                                _objOC.Total = _total;
                                context.SaveChanges();

                            }
                            else
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                            }

                        }
                        else {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }

                    }

                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return MyException.OnException(ex);
            }
        }

        public Respuesta ElimDetalleOrdenCompra(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.DetalleOrdenCompras
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();

                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        exists.AudActivo = 0;
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.DeleteOK);


                        var _subtotal = (from p in context.DetalleOrdenCompras
                                         where p.IdOrdenCompra == exists.IdOrdenCompra && p.AudActivo == 1
                                         select p.Total).Sum();

                        var _igv = _subtotal * _igv18;

                        var _total = _subtotal + _igv;

                        var _objOC = (from p in context.OrdenCompras
                                      where p.Id == exists.IdOrdenCompra && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        _objOC.Cotizacion = null;
                        _objOC.Proveedor = null;
                        _objOC.Estado = null;
                        _objOC.SubTotal = _subtotal;
                        _objOC.Igv = _igv;
                        _objOC.Total = _total;
                        context.SaveChanges();

                    }
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, new Respuesta { Id = Id });
                return MyException.OnException(ex);
            }
        }
    }
}