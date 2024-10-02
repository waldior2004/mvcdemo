using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public List<Cotizacion> ObtAllCotizacion(string desc)
        {
            List<Cotizacion> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Cotizacions
                           where p.Codigo.ToUpper().Contains(desc.ToUpper()) && p.AudActivo == 1
                           orderby p.Codigo ascending
                           select p).Skip(0).Take(10).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                LogError.PostErrorMessage(ex.InnerException, null);
                return null;
            }
        }

        public string GeneraCodigoCotizacion()
        {
            var correlativo = "";
            try
            {
                using (var context = new CompanyContext())
                {
                    correlativo = context.Database.SqlQuery<string>("SISTEMA.PROC_GENERACOTIZACION").SingleOrDefault();
                }
                return correlativo;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Cotizacion> ObtCotizacion()
        {
            List<Cotizacion> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Cotizacions.Include("Proveedor").Include("Estado").Include("Pedido")
                           where p.AudActivo == 1
                           select p).ToList();

                    foreach (var item in lst)
                    {
                        var objOC = (from p in context.OrdenCompras
                                     where p.IdCotizacion == item.Id && p.AudActivo == 1
                                     select p).FirstOrDefault();
                        if (objOC != null)
                        {
                            item.IdOrdenCompra = objOC.Id;
                            item.OrdenCompra = objOC;
                        }
                    }

                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Cotizacion ObtCotizacion(int Id)
        {
            Cotizacion obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Cotizacions.Include("Proveedor").Include("Estado").Include("Pedido")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    if (obj != null)
                    {
                        var lstProductos = (from p in context.DetalleCotizacions.Include("Producto").Include("Tarifario")
                                            where p.IdCotizacion == Id && p.AudActivo == 1
                                            select p).ToList();

                        foreach (var item in lstProductos)
                        {
                            var objUniMed = (from p in context.Productos
                                             join q in context.Tablas on p.IdUnidadMedida equals q.Id
                                             where p.Id == item.IdProducto && p.AudActivo == 1 && q.AudActivo == 1
                                             select q).FirstOrDefault();
                            item.Producto.UnidadMedida = objUniMed;
                        }

                        obj.DetalleCotizaciones = lstProductos;

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

        public Respuesta EditCotizacion(Cotizacion obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        var exists = (from p in context.Cotizacions
                                      where p.AudActivo == 1 && p.IdPedido == obj.IdPedido && p.IdProveedor == obj.IdProveedor
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {

                            var existsOC = (from p in context.Pedidos
                                            join q in context.Cotizacions on p.Id equals q.IdPedido
                                            join r in context.OrdenCompras on q.Id equals r.IdCotizacion
                                            where p.AudActivo == 1 && q.AudActivo == 1 && r.AudActivo == 1 && p.Id == obj.IdPedido
                                            select r).FirstOrDefault();

                            if (existsOC == null)
                            {
                                obj.Pedido = null;
                                obj.Proveedor = null;
                                obj.Estado = null;
                                obj.OrdenCompra = null;
                                obj.AudActivo = 1;
                                context.Cotizacions.Add(obj);
                                context.SaveChanges();
                                objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                                context.Entry(obj).GetDatabaseValues();
                                objResp.Metodo = obj.Id.ToString();
                            }
                            else
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.OrdenCompraExists);
                            }
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.PedidoExistsCotizacion);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.Cotizacions
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var subexists = (from p in context.Cotizacions
                                          where p.AudActivo == 1 && p.IdPedido == obj.IdPedido && p.IdProveedor == obj.IdProveedor && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (subexists == null)
                            {

                                var existsOC = (from p in context.Pedidos
                                                join q in context.Cotizacions on p.Id equals q.IdPedido
                                                join r in context.OrdenCompras on q.Id equals r.IdCotizacion
                                                where p.AudActivo == 1 && q.AudActivo == 1 && r.AudActivo == 1 && p.Id == obj.IdPedido
                                                select r).FirstOrDefault();

                                if (existsOC == null)
                                {
                                    exists.Pedido = null;
                                    exists.Proveedor = null;
                                    exists.Estado = null;
                                    exists.IdProveedor = obj.IdProveedor;
                                    exists.IdEstado = obj.IdEstado;
                                    exists.FechaCotizacion = obj.FechaCotizacion;
                                    exists.FechaEntrega = obj.FechaEntrega;
                                    exists.Observacion = obj.Observacion;
                                    exists.AudUpdate = DateTime.Now;
                                    objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                                }
                                else
                                {
                                    objResp = MessagesApp.BackAppMessage(MessageCode.OrdenCompraExists);
                                }
                            }
                            else
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.PedidoExistsCotizacion);
                            }
                        }
                    }
                    context.SaveChanges();
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
                return MyException.OnException(ex);
            }
        }

        public Respuesta ElimCotizacion(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Cotizacions
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {

                        var existsOC = (from p in context.Pedidos
                                        join q in context.Cotizacions on p.Id equals q.IdPedido
                                        join r in context.OrdenCompras on q.Id equals r.IdCotizacion
                                        where p.AudActivo == 1 && q.AudActivo == 1 && r.AudActivo == 1 && p.Id == exists.IdPedido
                                        select r).FirstOrDefault();

                        if (existsOC == null)
                        {
                            var docs = (from p in context.DetalleCotizacions
                                        where p.IdCotizacion == Id
                                        select p);

                            foreach (var item in docs)
                            {
                                item.AudActivo = 0;
                            }

                            exists.AudActivo = 0;
                            context.SaveChanges();

                            var lstCotizaciones = (from p in context.Cotizacions
                                                   where p.IdPedido == exists.IdPedido && p.AudActivo == 1
                                                   select p).ToList();

                            if (lstCotizaciones.Count == 0)
                            {
                                var objPedido = (from p in context.Pedidos
                                                 where p.Id == exists.IdPedido && p.AudActivo == 1
                                                 select p).FirstOrDefault();

                                var lstestado = (from p in context.Tablas
                                                 join q in context.GrupoTablas on p.IdGrupoTabla equals q.Id
                                                 where q.Codigo == "015" && p.AudActivo == 1 && q.AudActivo == 1
                                                 select p).ToList();

                                if (objPedido.FlagAprobacion == 1)
                                    objPedido.IdEstado = lstestado.Where(p => p.Codigo == "004").First().Id;
                                else
                                    objPedido.IdEstado = lstestado.Where(p => p.Codigo == "002").First().Id;

                                context.SaveChanges();
                            }

                            objResp = MessagesApp.BackAppMessage(MessageCode.DeleteOK);
                        }
                        else
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.OrdenCompraExists);
                        }
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
