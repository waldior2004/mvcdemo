using com.msc.services.dto;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;

namespace com.msc.infraestructure.utils
{
    public static class DirectoryUtil
    {
        public static DocumentoDTO SaveFileOnFolder(HttpPostedFileBase file, string name)
        {
            string pathString = Path.Combine(ConfigurationManager.AppSettings["UploadFolder"].ToString(), name);
            var fileName1 = Path.GetFileName(file.FileName);
            bool isExists = Directory.Exists(pathString);
            if (!isExists)
                Directory.CreateDirectory(pathString);
            var uid = Guid.NewGuid().ToString();
            var path = string.Format("{0}\\{1}", pathString, uid);
            file.SaveAs(path);

            var objDoc = new DocumentoDTO
            {
                Guid = uid,
                Extension = Path.GetExtension(fileName1),
                RutaLocal = path,
                Nombre = fileName1,
                Type = MimeMapping.GetMimeMapping(file.FileName),
                TamanoMB = Convert.ToDecimal(Convert.ToInt64(file.ContentLength).ToSize(Extensions.SizeUnits.MB))
            };

            return objDoc;
        }

        public static DataTable LeerExcel(string ruta)
        {
            return Utilitario.General.LeerExcel(ruta);
        }

    }
}
