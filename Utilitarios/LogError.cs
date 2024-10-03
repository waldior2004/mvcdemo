using com.msc.infraestructure.entities;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace com.msc.infraestructure.utils
{

    public static class LogError
    {
        public static void PostInfoMessage(string str)
        {
            var fileFolder = ConfigurationManager.AppSettings["ErrorFolder"];

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            var fileName = String.Format("Messages_{0}.txt", DateTime.Now.ToShortDateString().Replace("/", ""));
            var targetFolder = Path.Combine(fileFolder, fileName);
            FileInfo f = new FileInfo(targetFolder);
            if (!File.Exists(targetFolder))
            {
                StreamWriter wr = f.CreateText();
                using (wr)
                {
                    wr.WriteLine(String.Format("{1}: {0}", str, DateTime.Now.ToString()));
                    wr.WriteLine(("-").PadRight(80, '-'));
                }
            }
            else
            {
                StreamWriter wr = f.AppendText();
                using (wr)
                {
                    wr.WriteLine(String.Format("{1}: {0}", str, DateTime.Now.ToString()));
                    wr.WriteLine(("-").PadRight(80, '-'));
                }
            }
        }

        public static void PostErrorMessage(Exception ex, object obj)
        {
            var error = new Respuesta
            {
                Id = ex.HResult,
                Descripcion = ex.Message,
                Message = (ex.InnerException != null ? ex.InnerException.Message : ""),
                Aplicacion = ex.Source,
                TipoError = ex.HelpLink,
                PilaError = ex.StackTrace
            };

            var fileFolder = ConfigurationManager.AppSettings["ErrorFolder"];

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            var fileName = String.Format("Errors_{0}.txt", DateTime.Now.ToShortDateString().Replace("/", ""));

            var targetFolder = Path.Combine(fileFolder, fileName);
            FileInfo f = new FileInfo(targetFolder);
            if (!File.Exists(targetFolder))
            {
                StreamWriter wr = f.CreateText();
                WriteToText(wr, error, obj);
            }
            else
            {
                StreamWriter wr = f.AppendText();
                WriteToText(wr, error, obj);
            }
        }

        private static void WriteToText(StreamWriter wr, Respuesta error, object obj)
        {
            StringBuilder strb = new StringBuilder();
            if (obj != null)
            {
                foreach (PropertyInfo property in obj.GetType().GetProperties())
                {
                    var prop = obj.GetType().GetProperty(property.Name);
                    strb.Append(string.Format("{0}: {1},", property.Name, prop.GetValue(obj)));
                }
            }
            using (wr)
            {
                wr.WriteLine(String.Format("Id: {0}", error.Id));
                wr.WriteLine(String.Format("Application: {0}", error.Aplicacion));
                wr.WriteLine(String.Format("Time: {0}", DateTime.Now.ToLongTimeString()));
                wr.WriteLine(String.Format("Method Name: {0}", strb.ToString()));
                wr.WriteLine(String.Format("ErrorType: {0}", error.TipoError));
                wr.WriteLine(String.Format("ErrorMessage: {0}", error.Descripcion));
                wr.WriteLine(String.Format("InternalMessage: {0}", error.Message));
                wr.WriteLine(String.Format("Stack: {0}", error.PilaError));
                wr.WriteLine(("-").PadRight(80, '-'));
            }
        }
    }
}
