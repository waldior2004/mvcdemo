using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public Tarifario ObtTarifarioDummy(int id)
        {
            Tarifario objT = new Tarifario();
            try
            {
                using (var context = new CompanyContext())
                {
                    var objo = (from p in context.Productos.Include("UnidadMedida")
                                where p.AudActivo == 1 && p.Id == id
                                select p).FirstOrDefault();
                    var objP = (from p in context.Proveedores
                                where p.AudActivo == 1 && p.Ruc == "11111111111"
                                select p).FirstOrDefault();
                    var objM = (from p in ObtTablaGrupo("003")
                                where p.Codigo == "001"
                                select p).FirstOrDefault();
                    var objE = (from p in ObtTablaGrupo("015")
                                where p.Codigo == "001"
                                select p).FirstOrDefault();
                    objT = new Tarifario { 
                        Id = 0,
                        Proveedor = objP,
                        IdProveedor = objP.Id,
                        Producto = objo,
                        IdProducto = objo.Id,
                        Moneda = objM,
                        IdMoneda = objM.Id,
                        Estado = objE,
                        IdEstado = objE.Id,
                        Precio = 0,
                        InicioVigencia = DateTime.Now,
                        FinVigencia =DateTime.Now
                    };
                    
                }
                return objT;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Tarifario ObtTarifarioxProveedorProducto(int idprov, int idprod)
        { 
            Tarifario objT;
            try
            {
                using (var context = new CompanyContext())
                {
                    var lstT = (from p in context.Tarifario.Include("Proveedor").Include("Producto").Include("Moneda").Include("Estado")
                                where p.AudActivo == 1 && !(p.FinVigencia < DateTime.Now) && p.IdProducto == idprod && p.IdProveedor == idprov
                                select p).ToList();
                    var rango = new Range<DateTime>(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
                    objT = lstT.Where(p => p.Estado.Codigo == "001" && rango.IsOverlapped(new Range<DateTime>(p.InicioVigencia, p.FinVigencia))).OrderBy(q => q.Precio).FirstOrDefault();
                    if (objT != null)
                    {
                        var uniMed = context.Tablas.Where(p => p.Id == objT.Producto.IdUnidadMedida && p.AudActivo == 1).FirstOrDefault();
                        objT.Producto.UnidadMedida = uniMed;
                    }
                    else
                        objT = ObtTarifarioDummy(idprod);
                }

                return objT;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Tarifario ObtTarifarioMinPrecioProducto(int id)
        {
            Tarifario objT;
            try
            {
                using (var context = new CompanyContext())
                {
                    var lstT = (from p in context.Tarifario.Include("Proveedor").Include("Producto").Include("Moneda").Include("Estado")
                           where p.AudActivo == 1 && !(p.FinVigencia < DateTime.Now) && p.IdProducto == id
                           select p).ToList();
                    var rango = new Range<DateTime>(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
                    objT = lstT.Where(p => p.Estado.Codigo == "001" && rango.IsOverlapped(new Range<DateTime>(p.InicioVigencia, p.FinVigencia))).OrderBy(q => q.Precio).FirstOrDefault();
                    if (objT != null)
                    {
                        var uniMed = context.Tablas.Where(p => p.Id == objT.Producto.IdUnidadMedida && p.AudActivo == 1).FirstOrDefault();
                        objT.Producto.UnidadMedida = uniMed;
                    }
                    else
                        objT = ObtTarifarioDummy(id);

                }
                return objT;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Tarifario> ObtTarifario()
        {
            List<Tarifario> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Tarifario.Include("Proveedor").Include("Producto").Include("Moneda").Include("Estado")
                           where p.AudActivo == 1 && !(p.FinVigencia < DateTime.Now)
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

        public Tarifario ObtTarifario(int Id)
        {
            Tarifario obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Tarifario.Include("Proveedor").Include("Producto").Include("Moneda").Include("Estado")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    if (obj != null)
                    {
                        var lstDocumentos = (from p in context.TarifarioDocs.Include("Documento")
                                             where p.IdTarifario == Id && p.AudActivo == 1
                                             select p).ToList();

                        var lstHistorico = (from p in context.VTarifarios
                                            where p.IdProveedor == obj.IdProveedor && p.IdProducto == obj.IdProducto && p.Id != obj.Id
                                            select p).ToList();

                        foreach (var item in lstHistorico)
                        {
                            item.Proveedor = context.Proveedores.Where(p => p.Id == item.IdProveedor).FirstOrDefault();
                            item.Producto = context.Productos.Where(p => p.Id == item.IdProducto).FirstOrDefault();
                            item.Moneda = context.Tablas.Where(p => p.Id == item.IdMoneda).FirstOrDefault();
                            item.Estado = context.Tablas.Where(p => p.Id == item.IdEstado).FirstOrDefault();
                        }

                        obj.TarifarioDocs = lstDocumentos;
                        obj.V_Tarifarios = lstHistorico;
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

        public Respuesta EditTarifario(Tarifario obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        Range<DateTime> rango = new Range<DateTime>(obj.InicioVigencia, obj.FinVigencia);
                        var exists = (from p in context.Tarifario
                                      where p.IdProveedor == obj.IdProveedor && p.IdProducto == obj.IdProducto && p.AudActivo == 1
                                      select p).ToList();

                        if (exists != null)
                        {

                            var subexists = (from p in exists
                                             where rango.IsOverlapped(new Range<DateTime>(p.InicioVigencia, p.FinVigencia))
                                             select p).FirstOrDefault();

                            if (subexists != null)
                                objResp = MessagesApp.BackAppMessage(MessageCode.RangeDatesTarifario);
                            else
                            {
                                obj.Producto = null;
                                obj.Moneda = null;
                                obj.Estado = null;
                                obj.Proveedor = null;
                                obj.AudActivo = 1;
                                obj.Descripcion = "";
                                context.Tarifario.Add(obj);
                                context.SaveChanges();
                                objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                                context.Entry(obj).GetDatabaseValues();
                                objResp.Metodo = obj.Id.ToString();
                            }
                        }

                    }
                    else
                    {
                        var exists = (from p in context.Tarifario
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {

                            Range<DateTime> rango = new Range<DateTime>(obj.InicioVigencia, obj.FinVigencia);
                            var exists2 = (from p in context.Tarifario
                                          where p.IdProveedor == obj.IdProveedor && p.IdProducto == obj.IdProducto && p.AudActivo == 1
                                          && p.Id != obj.Id
                                          select p).ToList();

                            if (exists2 != null)
                            {
                                var subexists2 = (from p in exists2
                                                  where rango.IsOverlapped(new Range<DateTime>(p.InicioVigencia, p.FinVigencia))
                                                  select p).FirstOrDefault();

                                if (subexists2 != null)
                                    objResp = MessagesApp.BackAppMessage(MessageCode.RangeDatesTarifario);
                                else
                                {
                                    exists.IdEstado = obj.IdEstado;
                                    exists.IdMoneda = obj.IdMoneda;
                                    exists.IdProducto = obj.IdProducto;
                                    exists.IdProveedor = obj.IdProveedor;
                                    exists.Precio = obj.Precio;
                                    exists.InicioVigencia = obj.InicioVigencia;
                                    exists.FinVigencia = obj.FinVigencia;
                                    objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                                }
                            }
                            else
                            {
                                exists.IdEstado = obj.IdEstado;
                                exists.IdMoneda = obj.IdMoneda;
                                exists.IdProducto = obj.IdProducto;
                                exists.IdProveedor = obj.IdProveedor;
                                exists.Precio = obj.Precio;
                                exists.InicioVigencia = obj.InicioVigencia;
                                exists.FinVigencia = obj.FinVigencia;
                                objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
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

        public Respuesta ElimTarifario(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Tarifario
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {

                        var docs = (from p in context.TarifarioDocs
                                            where p.IdTarifario == Id
                                            select p);
                        foreach (var item in docs)
                        {
                            item.AudActivo = 0;
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
