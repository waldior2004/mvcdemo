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
        public List<TempoPedido> ObtTempoPedido(int Id)
        {
            List<TempoPedido> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.TempoPedidos
                           where p.IdPedido == Id
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

        public Respuesta EditTempoPedido(TempoPedido obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {

                    var exists = (from p in context.TempoPedidos
                                    where p.IdPedido == obj.IdPedido && p.IdProducto == obj.IdProducto
                                    select p).FirstOrDefault();

                    if (exists == null)
                    {
                        context.TempoPedidos.Add(obj);
                        objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
                        context.SaveChanges();
                    }
                    else
                    {
                        exists.IdProveedor = obj.IdProveedor;
                        exists.IdTarifario = (obj.IdTarifario == 0 ? null : obj.IdTarifario);
                        exists.IdMoneda = obj.IdMoneda;
                        exists.Precio = obj.Precio;
                        exists.Descripcion = obj.Descripcion;
                        exists.Cantidad = obj.Cantidad;
                        context.SaveChanges();
                        objResp = MessagesApp.BackAppMessage(MessageCode.UpdateOK);
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

    }
}
