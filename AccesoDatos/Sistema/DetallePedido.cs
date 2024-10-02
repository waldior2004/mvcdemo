using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public DetallePedido ObtDetallePedido(int Id)
        {
            DetallePedido obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.DetallePedidos.Include("Producto")
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

        public Respuesta EditDetallePedido(DetallePedido obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {

                    if (obj.Id == 0)
                    {
                        var exists = (from p in context.DetallePedidos
                                      where p.IdProducto == obj.IdProducto && p.AudActivo == 1 && p.IdPedido == obj.IdPedido
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            obj.Pedido = null;
                            obj.Producto = null;
                            obj.AudActivo = 1;
                            context.DetallePedidos.Add(obj);

                            var docs = (from p in context.TempoPedidos
                                        where p.IdPedido == obj.IdPedido
                                        select p);

                            foreach (var item in docs)
                            {
                                context.TempoPedidos.Remove(item);
                            }

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
                        var exists = (from p in context.DetallePedidos
                                      where p.IdProducto == obj.IdProducto && p.AudActivo == 1 && p.IdPedido == obj.IdPedido && p.Id != obj.Id
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            var subexists = (from p in context.DetallePedidos
                                             where p.AudActivo == 1  && p.Id == obj.Id
                                             select p).FirstOrDefault();

                            obj.Pedido = null;
                            obj.Producto = null;
                            subexists.IdProducto = obj.IdProducto;
                            subexists.Cantidad = obj.Cantidad;
                            subexists.Precio = obj.Precio;
                            subexists.Total = obj.Total;
                            subexists.Observaciones = obj.Observaciones;
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.SaveChanges();

                            var docs = (from p in context.TempoPedidos
                                        where p.IdPedido == subexists.IdPedido
                                        select p);

                            foreach (var item in docs)
                            {
                                context.TempoPedidos.Remove(item);
                            }

                            context.SaveChanges();

                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
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

        public Respuesta ElimDetallePedido(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.DetallePedidos
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {

                        var docs = (from p in context.TempoPedidos
                                    where p.IdPedido == exists.IdPedido
                                    select p);

                        foreach(var item in docs)
                        {
                            context.TempoPedidos.Remove(item);
                        }

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
