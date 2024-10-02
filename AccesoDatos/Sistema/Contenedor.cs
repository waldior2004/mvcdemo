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
        public Contenedor ObtContenedor(string Id, string viaje, string puerto, string movimiento)
        {
            Contenedor lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Contenedors
                           where p.Id == Id && p.IdViaje == viaje && p.IdPuerto == puerto && p.IdMovimiento == movimiento
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
