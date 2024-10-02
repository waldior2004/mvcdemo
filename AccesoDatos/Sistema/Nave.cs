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

        public Nave ObtNavexId(string Id)
        {
            Nave lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Naves
                           where p.Id == Id
                           select p).FirstOrDefault();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Nave> ObtAllNave(string desc)
        {
            List<Nave> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Naves
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

        public Nave ObtNave(string desc)
        {
            Nave lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Naves
                           where p.Descripcion.ToUpper().Contains(desc.ToUpper())
                           select p).FirstOrDefault();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }
    }
}
