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

        public string GeneraCodigoProvisionGasto()
        {
            var correlativo = "";
            try
            {
                using (var context = new CompanyContext())
                {
                    correlativo = context.Database.SqlQuery<string>("SISTEMA.PROC_GENERAPROVISION_GASTO").SingleOrDefault();
                }
                return correlativo;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }
      
        public List<Provision> ObtProvision()
        {
            List<Provision> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Provisions.Include("Empresa").Include("Sucursal").Include("TipoProvision").Include("CuentaContable").Include("Proveedor").Include("Moneda").Include("Estado")
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

        public Provision ObtProvision(int Id)
        {
            Provision obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Provisions.Include("Empresa").Include("Sucursal").Include("TipoProvision").Include("CuentaContable").Include("Proveedor").Include("Moneda").Include("Estado").Include("OrdenCompra")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditProvision(Provision obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {

                        var codeex = (from p in context.Provisions
                                      where p.Codigo == obj.Codigo && p.AudActivo == 1
                                      select p).FirstOrDefault();

                        if (codeex != null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                        else
                        {
                            obj.Empresa = null;
                            obj.Sucursal = null;
                            obj.TipoProvision = null;
                            obj.CuentaContable = null;
                            obj.Proveedor = null;
                            obj.Moneda = null;
                            obj.Estado = null;
                            obj.OrdenCompra = null;
                            obj.IdOrdenCompra = (obj.IdOrdenCompra == 0 ? null : obj.IdOrdenCompra);
                            obj.AudActivo = 1;
                            context.Provisions.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();
                        }
                    }
                    else
                    {
                        var exists = (from p in context.Provisions
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var codeex = (from p in context.Provisions
                                          where p.Codigo == obj.Codigo && p.AudActivo == 1 && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (codeex != null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                            }
                            else
                            {
                                exists.IdEmpresa = obj.IdEmpresa;
                                exists.IdSucursal = obj.IdSucursal;
                                exists.IdTipoProvision = obj.IdTipoProvision;
                                exists.IdCuentaContable = obj.IdCuentaContable;
                                exists.IdProveedor = obj.IdProveedor;
                                exists.IdMoneda = obj.IdMoneda;
                                exists.IdEstado = obj.IdEstado;
                                exists.IdOrdenCompra = (obj.IdOrdenCompra == 0 ? null : obj.IdOrdenCompra);
                                exists.Codigo = obj.Codigo;
                                exists.User = obj.User;
                                exists.Monto = obj.Monto;
                                exists.Concepto = obj.Concepto;
                                exists.MesProv = obj.MesProv;
                                exists.AnioProv = obj.AnioProv;
                                exists.MesServ = obj.MesServ;
                                exists.AnioServ = obj.AnioServ;
                                exists.ComentarioUno = obj.ComentarioUno;
                                exists.ComentarioDos = obj.ComentarioDos;
                                exists.AudUpdate = DateTime.Now;
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

        public Respuesta ElimProvision(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Provisions
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
