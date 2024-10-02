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
        public List<Bitacora> ObtBitacora(int Id, string Tabla)
        {
            List<Bitacora> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Bitacoras
                           where p.IdInterno == Id && p.Tabla == Tabla && p.AudActivo == 1
                           select p).OrderByDescending(q=>q.FecRegistro).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public Respuesta EditBitacora(Bitacora obj)
        {
            var objResp = new Respuesta();
            try
            {
                using (var context = new CompanyContext())
                {
                    obj.FecRegistro = DateTime.Now;
                    obj.AudActivo = 1;
                    context.Bitacoras.Add(obj);
                    objResp = MessagesApp.BackAppMessage(MessageCode.InsertOK);
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
    }
}
