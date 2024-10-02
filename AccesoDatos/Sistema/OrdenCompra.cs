using com.msc.infraestructure.entities;
using com.msc.infraestructure.entities.impresion;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public List<OrdenCompra> ObtAllOrdenCompra(string desc)
        {
            List<OrdenCompra> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.OrdenCompras
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

        public string GeneraCodigoOrdenCompra()
        {
            var correlativo = "";
            try
            {
                using (var context = new CompanyContext())
                {
                    correlativo = context.Database.SqlQuery<string>("SISTEMA.PROC_GENERAOC").SingleOrDefault();
                }
                return correlativo;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<OrdenCompra> ObtOrdenCompra()
        {
            List<OrdenCompra> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.OrdenCompras.Include("Proveedor").Include("Estado").Include("Cotizacion")
                           where p.AudActivo == 1
                           select p).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<OrdenCompra> ObtOrdenCompraPorValidar()
        {
            List<OrdenCompra> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.OrdenCompras.Include("Proveedor").Include("Estado").Include("Cotizacion")
                           where p.AudActivo == 1
                           select p).ToList();
                    lst = lst.Where(p => p.Estado.Codigo != "001" && p.Estado.Codigo != "002").ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public OrdenCompra ObtOrdenCompra(int Id)
        {
            OrdenCompra obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.OrdenCompras.Include("Proveedor").Include("Estado").Include("Cotizacion")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    if (obj != null)
                    {
                        var lstProductos = (from p in context.DetalleOrdenCompras.Include("Producto")
                                            where p.IdOrdenCompra == Id && p.AudActivo == 1
                                            select p).ToList();

                        foreach (var item in lstProductos)
                        {
                            var objUniMed = (from p in context.Productos
                                             join q in context.Tablas on p.IdUnidadMedida equals q.Id
                                             where p.Id == item.IdProducto && p.AudActivo == 1 && q.AudActivo == 1
                                             select q).FirstOrDefault();
                            item.Producto.UnidadMedida = objUniMed;
                        }

                        obj.DetalleOrdenCompras = lstProductos;

                        var lstDocumentos = (from p in context.OrdenCompraDocs.Include("Documento")
                                             where p.IdOrdenCompra == Id && p.AudActivo == 1
                                             select p).ToList();

                        obj.OrdenCompraDocs = lstDocumentos;

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

        public Respuesta EditOrdenCompra(OrdenCompra obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        var exists = (from p in context.OrdenCompras
                                      where p.AudActivo == 1 && p.IdCotizacion == obj.IdCotizacion
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {

                            var idpedido = (from p in context.Pedidos
                                            join q in context.Cotizacions on p.Id equals q.IdPedido
                                            where q.Id == obj.IdCotizacion && p.AudActivo == 1 && q.AudActivo == 1
                                            select p.Id).FirstOrDefault();

                            var lstCot = (from p in context.Cotizacions
                                          join q in context.OrdenCompras on p.Id equals q.IdCotizacion
                                          where p.IdPedido == idpedido && p.AudActivo == 1 && q.AudActivo == 1
                                          select p).ToList();

                            if (lstCot.Count == 0)
                            {
                                var lstestado = (from p in context.Tablas
                                                 join q in context.GrupoTablas on p.IdGrupoTabla equals q.Id
                                                 where q.Codigo == "015" && p.AudActivo == 1 && q.AudActivo == 1
                                                 select p).ToList();

                                obj.IdEstado = lstestado.Where(p => p.Codigo == "002").First().Id;

                                obj.Cotizacion = null;
                                obj.Proveedor = null;
                                obj.Estado = null;
                                obj.AudActivo = 1;
                                context.OrdenCompras.Add(obj);

                                lstCot = (from p in context.Cotizacions
                                          where p.IdPedido == idpedido && p.AudActivo == 1 
                                          select p).ToList();

                                foreach (var item in lstCot)
                                {
                                    item.IdEstado = lstestado.Where(p => p.Codigo == "009").First().Id;
                                }

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
                            objResp = MessagesApp.BackAppMessage(MessageCode.CotizacionExistsOC);
                        }
                    }
                    else
                    {
                        var exists = (from p in context.OrdenCompras
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var subexists = (from p in context.OrdenCompras
                                          where p.AudActivo == 1 && p.IdCotizacion == obj.IdCotizacion && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (subexists == null)
                            {
                                exists.Cotizacion = null;
                                exists.Proveedor = null;
                                exists.Estado = null;
                                exists.IdProveedor = obj.IdProveedor;
                                exists.IdEstado = obj.IdEstado;
                                exists.FechaRegistro = obj.FechaRegistro;
                                exists.Observacion = obj.Observacion;
                                exists.FlagAprobacion = obj.FlagAprobacion;
                                exists.ComentAprobador = obj.ComentAprobador;
                                exists.UserAprob = obj.UserAprob;
                                exists.AudUpdate = DateTime.Now;
                                objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                            }
                            else
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CotizacionExistsOC);
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

        public Respuesta ElimOrdenCompra(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.OrdenCompras
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {

                        var docs = (from p in context.DetalleOrdenCompras
                                    where p.IdOrdenCompra == Id
                                    select p);

                        foreach (var item in docs)
                        {
                            item.AudActivo = 0;
                        }

                        exists.AudActivo = 0;
                        context.SaveChanges();

                        var lstestado = (from p in context.Tablas
                                            join q in context.GrupoTablas on p.IdGrupoTabla equals q.Id
                                                where q.Codigo == "015" && p.AudActivo == 1 && q.AudActivo == 1
                                            select p).ToList();

                        var idpedido = (from p in context.Pedidos
                                        join q in context.Cotizacions on p.Id equals q.IdPedido
                                        where q.Id == exists.IdCotizacion && p.AudActivo == 1 && q.AudActivo == 1
                                        select p.Id).FirstOrDefault();


                        var lstCot = (from p in context.Cotizacions
                                      where p.IdPedido == idpedido && p.AudActivo == 1
                                      select p).ToList();

                        foreach (var item in lstCot)
                        {
                            item.IdEstado = lstestado.Where(p => p.Codigo == "002").First().Id;
                        }

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

        public Respuesta FactOrdenCompra(int Id, string Factura)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    
                    var exists = (from p in context.OrdenCompras
                                    where p.Id == Id && p.AudActivo == 1
                                    select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        exists.Cotizacion = null;
                        exists.Proveedor = null;
                        exists.Estado = null;
                        exists.NumFactura = Factura;
                        exists.AudUpdate = DateTime.Now;
                        objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                    }
                    context.SaveChanges();
                }
                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return MyException.OnException(ex);
            }
        }

        public List<IMP_ORDENCOMPRA> ImprimirOrdenCompra(int Id)
        {
            List<IMP_ORDENCOMPRA> lstReporte;
            try
            {
                using (var context = new CompanyContext())
                {
                    lstReporte = context.Database.SqlQuery<IMP_ORDENCOMPRA>("[SISTEMA].[IMP_ORDENCOMPRA] @IDORDEN", new SqlParameter("IDORDEN", Id)).ToList();
                }

                return lstReporte;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

    }
}