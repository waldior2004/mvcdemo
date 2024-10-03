using com.msc.infraestructure.entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.msc.infraestructure.utils
{
    public static class MyException 
    {
        public static Respuesta OnException(Exception ex)
        {
            if (ex is DbEntityValidationException)
            {
                var errorMessages = (ex as DbEntityValidationException).EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                LogError.PostErrorMessage(ex, new Respuesta { Id = -1, Message = fullErrorMessage });
            }
            else if (ex is DbUpdateException)
            {
                var sb = new StringBuilder();
                sb.AppendLine(string.Format("DbUpdateException detalle - {0}", ex.InnerException.InnerException.Message));
                foreach (var eve in (ex as DbUpdateException).Entries)
                {
                    sb.AppendLine(string.Format("Entidad de Tipo {0} en estado {1} no se puede actualizar", eve.Entity.GetType().Name, eve.State));
                }
                LogError.PostErrorMessage(ex, new Respuesta { Id = -1, Message = sb.ToString() });
            }

            return MessagesApp.BackAppMessage(MessageCode.InternalError);
        }
    }
}
