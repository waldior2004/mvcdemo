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

        public List<Producto> ObtAllProductoxProveedor(string desc, int id)
        {
            List<Producto> lst;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Productos.Include("UnidadMedida")
                           where p.Descripcion.ToUpper().Contains(desc.ToUpper())
                           orderby p.Descripcion ascending
                           select p).Skip(0).Take(10).ToList();

                    foreach (var item in lst)
                    {
                        var lstTar = (from p in context.Tarifario.Include("Estado")
                                     where p.IdProveedor == id && p.AudActivo == 1 && p.IdProducto == item.Id
                                     select p).ToList();
                        if (lstTar.Count > 0)
                        {
                            var rango = new Range<DateTime>(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
                            var objT = lstTar.Where(p => p.Estado.Codigo == "001" && rango.IsOverlapped(new Range<DateTime>(p.InicioVigencia, p.FinVigencia))).OrderBy(q => q.Precio).FirstOrDefault();
                            if (objT != null)
                            {
                                item.Abreviatura = objT.Id.ToString();
                                item.Observaciones = objT.Precio.ToString();
                            }
                            else
                            {
                                item.IdCodigoFamilia = 0;
                                item.Observaciones = "0.000";
                            }
                        }
                        else
                        {
                            item.IdCodigoFamilia = 0;
                            item.Observaciones = "0.000";
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
        public List<Producto> ObtAllProducto(string desc)
        {
            List<Producto> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Productos.Include("UnidadMedida")
                           where p.Descripcion.ToUpper().Contains(desc.ToUpper())
                           orderby p.Descripcion ascending
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

        public List<Producto> ObtProducto()
        {
            List<Producto> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Productos.Include("Grupo").Include("Familia").Include("SubFamilia").Include("UnidadMedida").Include("TipoServicio")
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

        public Producto ObtProducto(int Id)
        {
            Producto obj = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    obj = (from p in context.Productos.Include("Grupo").Include("Familia").Include("SubFamilia").Include("UnidadMedida").Include("TipoServicio")
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

        public Respuesta EditProducto(Producto obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    if (obj.Id == 0)
                    {
                        var codeex = (from p in context.Productos
                                      where (p.Abreviatura == obj.Abreviatura || p.Descripcion.ToLower() == obj.Descripcion.ToLower())
                                      select p).FirstOrDefault();

                        if (codeex != null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                        }
                        else
                        {
                            obj.Familia = null;
                            obj.SubFamilia = null;
                            obj.Grupo = null;
                            obj.UnidadMedida = null;
                            obj.AudActivo = 1;
                            obj.TipoServicio = null;
                            context.Productos.Add(obj);
                            context.SaveChanges();
                            objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                            context.Entry(obj).GetDatabaseValues();
                            objResp.Metodo = obj.Id.ToString();
                        }
                    }
                    else
                    {
                        var exists = (from p in context.Productos
                                      where p.Id == obj.Id
                                      select p).FirstOrDefault();
                        if (exists == null)
                        {
                            objResp = MessagesApp.BackAppMessage(MessageCode.NotFoundRecord);
                        }
                        else
                        {
                            var codeex = (from p in context.Productos
                                          where (p.Abreviatura == obj.Abreviatura || p.Descripcion.ToLower() == obj.Descripcion.ToLower()) && p.Id != obj.Id
                                          select p).FirstOrDefault();

                            if (codeex != null)
                            {
                                objResp = MessagesApp.BackAppMessage(MessageCode.CodeDescAlreadyexists);
                            }
                            else
                            {
                                exists.Abreviatura = obj.Abreviatura;
                                exists.Descripcion = obj.Descripcion;
                                exists.IdCodigoFamilia = obj.IdCodigoFamilia;
                                exists.IdCodigoGrupo = obj.IdCodigoGrupo;
                                exists.IdTipoServicio = obj.IdTipoServicio;
                                exists.IdCodigoSubFamilia = obj.IdCodigoSubFamilia;
                                exists.IdUnidadMedida = obj.IdUnidadMedida;
                                exists.Observaciones = obj.Observaciones;

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

        public Respuesta ElimProducto(int Id)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    var exists = (from p in context.Productos
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
