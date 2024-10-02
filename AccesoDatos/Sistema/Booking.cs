using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {
        public Booking ObtBooking(string desc)
        {
            Booking lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Bookings
                           where p.AudActivo == 1 && p.Descripcion.ToUpper().Contains(desc.ToUpper())
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
