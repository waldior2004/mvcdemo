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
        public List<Viaje> ObtViajexNave(string id, int port)
        {
            List<Viaje> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Viajes
                           where p.IdNave == id && p.IdPuerto == port
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

        public List<Viaje> ObtAllViaje(string desc)
        {
            List<Viaje> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Viajes
                           where p.Descripcion.ToUpper().Contains(desc.ToUpper())
                           orderby p.Descripcion ascending
                           select p).DistinctBy(p=>p.Descripcion).Skip(0).Take(10).ToList();
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

        public Viaje ObtViaje(string viaje)
        {
            Viaje lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Viajes
                           where p.Id == viaje
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
