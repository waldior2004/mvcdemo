using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq;
using System.Security.Cryptography;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public List<Pedido> ObtAllPedido(string desc)
        {
            List<Pedido> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Pedidos
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

        public string GeneraCodigoPedido(int IdSucursal, int IdArea)
        {
            var correlativo = "";
            try
            {
                using (var context = new CompanyContext())
                {
                    correlativo = context.Database.SqlQuery<string>("SISTEMA.PROC_GENERAPEDIDO @ID_SUCURSAL, @ID_AREA_SOL", new SqlParameter("ID_SUCURSAL", IdSucursal), new SqlParameter("ID_AREA_SOL", IdArea)).SingleOrDefault();
                }
                return correlativo;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Tarifario> ObtPedidoByTempo(int id)
        {
            List<Tarifario> lst = new List<Tarifario>();
            try
            {
                using (var context = new CompanyContext())
                {
                    var lstTempo = (from p in context.TempoPedidos
                                    where p.IdPedido == id
                                    select p).ToList();

                    foreach (var item in lstTempo)
                    {
                        var tarif = new Tarifario
                        {
                            Id = (item.IdTarifario == null ? 0 : Convert.ToInt32(item.IdTarifario)),
                            IdProveedor = item.IdProveedor,
                            Proveedor = ObtProveedor(item.IdProveedor),
                            IdProducto = item.IdProducto,
                            Producto = ObtProducto(item.IdProducto),
                            IdMoneda = item.IdMoneda,
                            Moneda = ObtTabla(item.IdMoneda),
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Cantidad = item.Cantidad
                        };

                        lst.Add(tarif);
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

        public List<Pedido> ObtPedidoPorValidar()
        {
            List<Pedido> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Pedidos.Include("Empresa").Include("Sucursal").Include("AreaSolicitante").Include("TipoPeticion").Include("Estado").Include("CentroCosto")
                           where p.AudActivo == 1
                           select p).ToList();
                    lst = lst.Where(p => p.Estado.Codigo != "001" && p.Estado.Codigo != "002").ToList();

                    foreach (var item in lst)
                    {
                        var lstCotizaciones = (from p in context.Cotizacions.Include("Proveedor").Include("Estado")
                                               where p.IdPedido == item.Id && p.AudActivo == 1
                                               select p).ToList();
                        item.Cotizaciones = lstCotizaciones;

                        var objOC = (from p in context.OrdenCompras
                                     join q in context.Cotizacions on p.IdCotizacion equals q.Id
                                     where q.IdPedido == item.Id && q.AudActivo == 1 && p.AudActivo == 1
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

        public List<Pedido> ObtPedidoPorCotizar()
        {
            List<Pedido> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Pedidos.Include("Empresa").Include("Sucursal").Include("AreaSolicitante").Include("TipoPeticion").Include("Estado").Include("CentroCosto")
                           where p.AudActivo == 1
                           select p).ToList();
                    lst = lst.Where(p => p.Estado.Codigo == "002" || p.Estado.Codigo == "004" || p.Estado.Codigo == "005").ToList();

                    foreach (var item in lst)
                    {
                        var lstCotizaciones = (from p in context.Cotizacions.Include("Proveedor").Include("Estado")
                                               where p.IdPedido == item.Id && p.AudActivo == 1
                                               select p).ToList();
                        item.Cotizaciones = lstCotizaciones;

                        var objOC = (from p in context.OrdenCompras
                                     join q in context.Cotizacions on p.IdCotizacion equals q.Id
                                     where q.IdPedido == item.Id && q.AudActivo == 1 && p.AudActivo == 1
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

        public List<Pedido> ObtPedido()
        {
            List<Pedido> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Pedidos.Include("Empresa").Include("Sucursal").Include("AreaSolicitante").Include("TipoPeticion").Include("Estado").Include("CentroCosto")
                           where p.AudActivo == 1
                           select p).ToList();

                    foreach (var item in lst)
                    { 
                        var lstCotizaciones = (from p in context.Cotizacions.Include("Proveedor").Include("Estado")
                                               where p.IdPedido == item.Id && p.AudActivo == 1
                                               select p).ToList();
                        item.Cotizaciones = lstCotizaciones;

                        var objOC = (from p in context.OrdenCompras
                                         join q in context.Cotizacions on p.IdCotizacion equals q.Id
                                         where q.IdPedido == item.Id && q.AudActivo == 1 && p.AudActivo == 1
                                         select p).FirstOrDefault();

                        if(objOC != null){
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

        public Pedido ObtPedido(int Id)
        {
            Pedido obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Pedidos.Include("Empresa").Include("Sucursal").Include("AreaSolicitante").Include("TipoPeticion").Include("Estado").Include("CentroCosto")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    if (obj != null)
                    {
                        var lstProductos = (from p in context.DetallePedidos.Include("Producto")
                                           where p.IdPedido == Id && p.AudActivo == 1
                                           select p).ToList();

                        foreach (var item in lstProductos)
                        {
                            var objUniMed = (from p in context.Productos
                                             join q in context.Tablas on p.IdUnidadMedida equals q.Id
                                             where p.Id == item.IdProducto && p.AudActivo == 1 && q.AudActivo == 1
                                             select q).FirstOrDefault();
                            item.Producto.UnidadMedida = objUniMed;
                        }

                        obj.DetallePedidos = lstProductos;

                        var lstCotizaciones = (from p in context.Cotizacions.Include("Proveedor").Include("Estado")
                                               where p.IdPedido == Id && p.AudActivo == 1
                                               select p).ToList();

                        foreach(var item in lstCotizaciones)
                        {
                            var contactos = (from p in context.ContactoProveedores
                                             where p.IdProveedor == item.IdProveedor && p.AudActivo == 1
                                             select p).ToList();

                            item.Proveedor.ContactoProveedor = contactos;

                            var detalleCotiza = (from p in context.DetalleCotizacions.Include("Producto").Include("Tarifario")
                                                 where p.IdCotizacion == item.Id && p.AudActivo == 1
                                                 select p).ToList();

                            foreach (var subitem in detalleCotiza)
                            {
                                var objUniMed = (from p in context.Productos
                                                 join q in context.Tablas on p.IdUnidadMedida equals q.Id
                                                 where p.Id == subitem.IdProducto && p.AudActivo == 1 && q.AudActivo == 1
                                                 select q).FirstOrDefault();

                                subitem.Producto.UnidadMedida = objUniMed;
                            }

                            item.DetalleCotizaciones = detalleCotiza;
                        }

                        obj.Cotizaciones = lstCotizaciones;

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

        public Respuesta EditPedido(Pedido obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        obj.Empresa = null;
                        obj.Sucursal = null;
                        obj.AreaSolicitante = null;
                        obj.CentroCosto = null;
                        obj.TipoPeticion = null;
                        obj.Estado = null;
                        obj.AudActivo = 1;
                        context.Pedidos.Add(obj);
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.Entry(obj).GetDatabaseValues();
                        objResp.Metodo = obj.Id.ToString();
                    }
                    else
                    {
                        var exists = (from p in context.Pedidos
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            exists.Empresa = null;
                            exists.Sucursal = null;
                            exists.AreaSolicitante = null;
                            exists.TipoPeticion = null;
                            exists.Estado = null;
                            exists.CentroCosto = null;
                            exists.FechaPeticion = obj.FechaPeticion;
                            exists.FechaEsperada = obj.FechaEsperada;
                            exists.Observaciones = obj.Observaciones;
                            exists.UsuarioSol = obj.UsuarioSol;
                            exists.IdEstado = obj.IdEstado;
                            exists.FlagAprobacion = obj.FlagAprobacion;
                            exists.ComentAprobador = obj.ComentAprobador;
                            exists.User = obj.User;
                            exists.AudUpdate = DateTime.Now;
                            objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
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

        public Respuesta ElimPedido(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Pedidos
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {

                        var docs = (from p in context.DetallePedidos
                                    where p.IdPedido == Id
                                    select p);

                        foreach (var item in docs)
                        {
                            item.AudActivo = 0;
                        }

                        var tmpped = (from p in context.TempoPedidos
                                    where p.IdPedido == Id
                                    select p);

                        foreach (var item in tmpped)
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
