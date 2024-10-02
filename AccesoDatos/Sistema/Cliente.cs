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
        public List<Cliente> ObtAllCliente(string desc)
        {
            List<Cliente> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Clientes
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

        public Cliente ObtCliente(string desc)
        {
            Cliente lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Clientes
                           where p.Descripcion.ToUpper().Contains(desc.ToUpper())
                           select p).FirstOrDefault();
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

        public List<Cliente> ObtCliente()
        {
            List<Cliente> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Clientes
                           select p).ToList();
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
    }
}
