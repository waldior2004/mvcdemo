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

        public List<Proveedor> ObtAllProveedorTarifaProducto(string desc, int id)
        {
            List<Proveedor> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Proveedores
                           where p.RazonSocial.ToUpper().Contains(desc.ToUpper()) && p.AudActivo == 1
                           orderby p.RazonSocial ascending
                           select p).Skip(0).Take(10).ToList();

                    var rango = new Range<DateTime>(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));

                    foreach (var item in lst)
                    {
                        var tarif = (from p in context.Tarifario.Include("Estado")
                                     where p.IdProducto == id && p.IdProveedor == item.Id && p.AudActivo == 1
                                     select p).ToList();
                        var objT = tarif.Where(p => p.Estado.Codigo == "001" && rango.IsOverlapped(new Range<DateTime>(p.InicioVigencia, p.FinVigencia))).OrderBy(q => q.Precio).FirstOrDefault();

                        if (objT != null)
                        {
                            item.IdTipoNIF = objT.Id;
                            item.Telefono = objT.Precio.ToString();
                        }
                        else {
                            item.IdTipoNIF = 0;
                            item.Telefono = "0.000";
                        }
                    }

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

        public List<Proveedor> ObtAllProveedor(string desc)
        {
            List<Proveedor> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Proveedores
                           where p.RazonSocial.ToUpper().Contains(desc.ToUpper()) && p.AudActivo == 1
                           orderby p.RazonSocial ascending
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

        public List<Proveedor> ObtProveedor()
        {
            List<Proveedor> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Proveedores.Include("TipoPersona").Include("GiroNegocio").Include("TipoNIF").Include("FormaCobro").Include("TipoContribuyente")
                           where p.AudActivo == 1 && p.Ruc != "11111111111"
                           select p)                           
                           .ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Proveedor ObtProveedor(int Id)
        {
            Proveedor obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Proveedores.Include("TipoPersona").Include("GiroNegocio").Include("TipoNIF").Include("FormaCobro").Include("TipoContribuyente")
                           where p.Id == Id && p.AudActivo == 1
                           select p).FirstOrDefault();

                    if (obj != null)
                    {
                        var lstDatoComercial = (from p in context.DatoComerciales.Include("Banco").Include("TipoCuenta").Include("Pais").Include("TipoInterlocutor")
                                                where p.IdProveedor == Id && p.AudActivo == 1
                                           select p).ToList();

                        obj.DatoComercial = lstDatoComercial;

                        var lstContactoProveedor = (from p in context.ContactoProveedores.Include("Cargo")
                                                    where p.IdProveedor == Id && p.AudActivo == 1
                                                select p).ToList();

                        obj.ContactoProveedor = lstContactoProveedor;

                        var lstImpuestoProveedor = (from p in context.ImpuestoProveedors.Include("Impuesto")
                                                    where p.IdProveedor == Id && p.AudActivo == 1
                                                    select p).ToList();

                        foreach (var item in lstImpuestoProveedor)
                        {
                            var objTipoImp = (from p in context.Tablas
                                              where p.Id == item.Impuesto.IdTipoImpuesto && p.AudActivo == 1
                                              select p).FirstOrDefault();

                            item.Impuesto.TipoImpuesto = objTipoImp;
                        }

                        obj.Impuestos = lstImpuestoProveedor; 
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

        public Respuesta EditProveedor(Proveedor obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        // verifica si el proveedor ya existe
                        var codeex = (from p in context.Proveedores
                                      where (p.Ruc == obj.Ruc || p.RazonSocial.ToLower() == obj.RazonSocial.ToLower()) && p.AudActivo == 1 
                                      select p).FirstOrDefault();

                        if (codeex != null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                        else
                        {
                            obj.TipoPersona = null;
                            obj.GiroNegocio = null;
                            obj.TipoNIF = null;
                            obj.FormaCobro = null;
                            obj.Pais = null;
                            obj.Departamento = null;
                            obj.Provincia = null;
                            obj.Distrito = null;
                            obj.TipoContribuyente = null;
                            obj.AudActivo = 1;
                            obj.AudInsert = DateTime.Now;
                            context.Proveedores.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();

                        }
                    }
                    else
                    {
                        var exists = (from p in context.Proveedores
                                      where p.Id == obj.Id && p.AudActivo == 1
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var codeex = (from p in context.Proveedores
                                          where (p.Ruc == obj.Ruc || p.RazonSocial.ToLower() == obj.RazonSocial.ToLower()) && p.AudActivo == 1 && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (codeex != null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                            }
                            else
                            {
                                exists.IdTipoPersona = obj.IdTipoPersona;
                                exists.Ruc = obj.Ruc;
                                exists.RazonSocial = obj.RazonSocial;
                                exists.NombreComercial = obj.NombreComercial;
                                exists.ApellidoPaterno = obj.ApellidoPaterno;
                                exists.ApellidoMaterno = obj.ApellidoMaterno;
                                exists.Nombres = obj.Nombres;
                                exists.DireccionPrincipal = obj.DireccionPrincipal;
                                exists.Telefono = obj.Telefono;
                                exists.Email = obj.Email;
                                exists.RepresentanteLegal = obj.RepresentanteLegal;
                                exists.IdGiroNegocio = obj.IdGiroNegocio;
                                exists.CodigoSAP = obj.CodigoSAP;
                                exists.IdTipoNIF = obj.IdTipoNIF;
                                exists.IdFormaCobro = obj.IdFormaCobro;
                                exists.IdPais = obj.IdPais;
                                exists.IdDepartamento = obj.IdDepartamento;
                                exists.IdProvincia = obj.IdProvincia;
                                exists.IdDistrito = obj.IdDistrito;
                                exists.IdTipoContribuyente = obj.IdTipoContribuyente;
                                exists.AudUpdate = DateTime.Now;
                                context.SaveChanges();
                                objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
                                
                            }
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

        public Respuesta ElimProveedor(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Proveedores
                                  where p.Id == Id && p.AudActivo == 1
                                  select p).FirstOrDefault();
                    if (exists == null)
                    {
                        objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                    }
                    else
                    {
                        foreach (var item in context.DatoComerciales.Where(x => x.IdProveedor == Id))
                        {
                            item.AudActivo = 0;
                        }

                        foreach (var item in context.ContactoProveedores.Where(x => x.IdProveedor == Id))
                        {
                            item.AudActivo = 0;
                            item.AudUpdate = DateTime.Now;
                        }

                        exists.AudActivo = 0;
                        exists.AudUpdate = DateTime.Now;
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
