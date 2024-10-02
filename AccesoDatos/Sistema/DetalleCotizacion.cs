using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public List<DetalleCotizacion> ObtDetallexCotizacion(int Id)
        {
            List<DetalleCotizacion> obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.DetalleCotizacions.Include("Producto").Include("Tarifario")
                           where p.IdCotizacion == Id && p.AudActivo == 1
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

        public DetalleCotizacion ObtDetalleCotizacion(int Id)
        {
            DetalleCotizacion obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.DetalleCotizacions.Include("Producto").Include("Tarifario")
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

        public Respuesta EditDetalleCotizacion(DetalleCotizacion obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        var exists = (from p in context.DetalleCotizacions
                                      where p.IdProducto == obj.IdProducto && p.AudActivo == 1 && p.IdCotizacion == obj.IdCotizacion
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            obj.Cotizacion = null;
                            obj.Producto = null;
                            obj.Tarifario = null;
                            obj.IdTarifario = (obj.IdTarifario == 0 ? null : obj.IdTarifario);
                            obj.Total = obj.Precio * obj.Cantidad;
                            obj.AudActivo = 1;
                            context.DetalleCotizacions.Add(obj);
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.SaveChanges();
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.DetalleCotizacions
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists != null)
                        {

                            var subexists = (from p in context.DetalleCotizacions
                                             where p.IdProducto == obj.IdProducto && p.AudActivo == 1 && p.IdCotizacion == obj.IdCotizacion && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (subexists == null)
                            {
                                exists.IdTarifario = (obj.IdTarifario == 0 ? null : obj.IdTarifario);
                                exists.Cantidad = obj.Cantidad;
                                exists.Precio = obj.Precio;
                                exists.Total = obj.Precio * obj.Cantidad;
                                exists.Observacion = obj.Observacion;
                                exists.AudUpdate = DateTime.Now;
                                objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
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

        public Respuesta ElimDetalleCotizacion(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.DetalleCotizacions
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
