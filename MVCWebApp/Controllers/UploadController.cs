using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace com.msc.frontend.mvc.Controllers
{
    public class UploadController : Controller
    {
        private void SetBitacora(BitacoraActionCode code, string id, string message, string tabla)
        {
            try
            {
                BitacoraDTO objBit = new BitacoraDTO
                {
                    Accion = MessagesApp.BitacoraActionMessage(code),
                    IdInterno = Convert.ToInt32(id),
                    Tabla = tabla,
                    User = (Session["Usuario"] as ExternoDTO).Usuario,
                    Tipo = MessagesApp.BitacoraModeMessage(BitacoraModeCode.Manual),
                    Detalle = message
                };
                var bita = (HttpContext.Application["proxySistema"] as ISistema).EditBitacora(objBit).SetRespuesta();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
            }
        }

        [Authorization]
        public FileResult DownloadTarifaCE(int id)
        {
            var obj = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifaCEDocNombre(id);
            byte[] fileBytes = System.IO.File.ReadAllBytes(obj.RutaLocal);
            string fileName = obj.Nombre;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Authorization]
        public ActionResult TarifaCE(HttpPostedFileBase file, int id)
        {

            bool isSavedSuccessfully = true;
            Respuesta result = new Respuesta();
            try
            {
                foreach (string fileName in Request.Files)
                {
                    file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {

                        var objDoc = DirectoryUtil.SaveFileOnFolder(file, "TarifaCE");

                        result = (HttpContext.Application["proxySistema"] as ISistema).EditDocumento(objDoc).SetRespuesta();

                        if (result.Id == 0)
                        {
                            TarifaCEDocDTO objTar = new TarifaCEDocDTO
                            {
                                IdDocumento = Convert.ToInt32(result.Metodo),
                                IdTarifaCE = id
                            };
                            var resDoc = (HttpContext.Application["proxySistema"] as ISistema).EditTarifaCEDoc(objTar).SetRespuesta();
                            if (resDoc.Id != 0)
                                isSavedSuccessfully = false;
                            else
                            {
                                result.Metodo = resDoc.Metodo;
                                SetBitacora(BitacoraActionCode.Actualizacion, id.ToString(), "Se adjunto el documento " + Path.GetFileName(file.FileName), "TarifaCE");
                            }
                        }
                        else 
                            isSavedSuccessfully = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                LogError.PostErrorMessage(ex, null);
            }

            if (isSavedSuccessfully)
            {
                return Json(new Respuesta { Id = Convert.ToInt32(result.Metodo), Message = "OK" });
            }
            else
            {
                return Json(new Respuesta { Id = -1, Message = result.Descripcion });
            }

        }

        [Authorization]
        public FileResult DownloadCondEspeCli(int id)
        {
            var obj = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCliDocNombre(id);
            byte[] fileBytes = System.IO.File.ReadAllBytes(obj.RutaLocal);
            string fileName = obj.Nombre;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Authorization]
        public ActionResult CondEspeCli(HttpPostedFileBase file, int id)
        {

            bool isSavedSuccessfully = true;
            Respuesta result = new Respuesta();
            try
            {
                foreach (string fileName in Request.Files)
                {
                    file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {

                        var objDoc = DirectoryUtil.SaveFileOnFolder(file, "CondEspeCli");

                        result = (HttpContext.Application["proxySistema"] as ISistema).EditDocumento(objDoc).SetRespuesta();

                        if (result.Id == 0)
                        {
                            CondEspeCliDocDTO objTar = new CondEspeCliDocDTO
                            {
                                IdDocumento = Convert.ToInt32(result.Metodo),
                                IdCondEspeCli = id
                            };
                            var resDoc = (HttpContext.Application["proxySistema"] as ISistema).EditCondEspeCliDoc(objTar).SetRespuesta();
                            if (resDoc.Id != 0)
                                isSavedSuccessfully = false;
                            else
                            {
                                result.Metodo = resDoc.Metodo;
                                SetBitacora(BitacoraActionCode.Actualizacion, id.ToString(), "Se adjunto el documento " + Path.GetFileName(file.FileName), "CondEspeCli");
                            }
                        }
                        else
                            isSavedSuccessfully = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                LogError.PostErrorMessage(ex, null);
            }

            if (isSavedSuccessfully)
            {
                return Json(new Respuesta { Id = Convert.ToInt32(result.Metodo), Message = "OK" });
            }
            else
            {
                return Json(new Respuesta { Id = -1, Message = result.Descripcion });
            }

        }

        [Authorization]
        public ActionResult CargaLiquiC(HttpPostedFileBase file, int id, string idNave, string idViaje, short idTipo, int idPort)
        {

            bool isSavedSuccessfully = true;
            Respuesta result = new Respuesta();
            try
            {
                foreach (string fileName in Request.Files)
                {
                    file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {

                        var objDoc = DirectoryUtil.SaveFileOnFolder(file, "CargaLiquiC");

                        result = (HttpContext.Application["proxySistema"] as ISistema).EditDocumento(objDoc).SetRespuesta();

                        if (result.Id == 0)
                        {
                            CargaLiquiCDTO objCarga = new CargaLiquiCDTO
                            {
                                Id = Convert.ToInt32(result.Metodo),
                                Usuario = (Session["Usuario"] as ExternoDTO).Usuario,
                                Terminal = new TablaDTO { Id = id, Descripcion = "" },
                                Nave = new NaveDTO { Id = idNave },
                                Viaje = new ViajeDTO { Id = idViaje },
                                Puerto = new PuertoDTO { Id = idPort },
                                TipoEnvio = idTipo
                            };

                            (HttpContext.Application["proxySistema"] as ISistema).EditCargaLiquiC(objCarga);
                        }
                        else
                            isSavedSuccessfully = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                LogError.PostErrorMessage(ex, null);
            }

            if (isSavedSuccessfully)
            {
                return Json(new Respuesta { Id = Convert.ToInt32(result.Metodo), Message = "OK" });
            }
            else
            {
                return Json(new Respuesta { Id = -1, Message = result.Descripcion });
            }

        }

        [Authorization]
        public FileResult DownloadCargaLiquiC(int id)
        {
            var obj = (HttpContext.Application["proxySistema"] as ISistema).ObtCargaLiquiCDocNombre(id);
            byte[] fileBytes = System.IO.File.ReadAllBytes(obj.RutaLocal);
            string fileName = obj.Nombre;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


        [Authorization]
        public FileResult DownloadTarifario(int id)
        {
            try
            {
                var obj = (HttpContext.Application["proxySistema"] as ISistema).ObtTarifarioDocNombre(id);
                byte[] fileBytes = System.IO.File.ReadAllBytes(obj.RutaLocal);
                string fileName = obj.Nombre;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        [Authorization]
        public ActionResult Tarifario(HttpPostedFileBase file, int id)
        {

            bool isSavedSuccessfully = true;
            Respuesta result = new Respuesta();
            try
            {
                foreach (string fileName in Request.Files)
                {
                    file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {

                        var objDoc = DirectoryUtil.SaveFileOnFolder(file, "Tarifario");

                        result = (HttpContext.Application["proxySistema"] as ISistema).EditDocumento(objDoc).SetRespuesta();

                        if (result.Id == 0)
                        {
                            TarifarioDocDTO objTar = new TarifarioDocDTO
                            {
                                IdDocumento = Convert.ToInt32(result.Metodo),
                                IdTarifario = id
                            };
                            var resDoc = (HttpContext.Application["proxySistema"] as ISistema).EditTarifarioDoc(objTar).SetRespuesta();
                            if (resDoc.Id != 0)
                                isSavedSuccessfully = false;
                            else
                            {
                                result.Metodo = resDoc.Metodo;
                                SetBitacora(BitacoraActionCode.Actualizacion, id.ToString(), "Se adjunto el documento " + Path.GetFileName(file.FileName), "Tarifario");
                            }
                        }
                        else
                            isSavedSuccessfully = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                LogError.PostErrorMessage(ex, null);
            }

            if (isSavedSuccessfully)
            {
                return Json(new Respuesta { Id = Convert.ToInt32(result.Metodo), Message = "OK" });
            }
            else
            {
                return Json(new Respuesta { Id = -1, Message = result.Descripcion });
            }

        }

        [Authorization]
        public FileResult DownloadOrdenCompra(int id)
        {
            var obj = (HttpContext.Application["proxySistema"] as ISistema).ObtOrdenCompraDocNombre(id);
            byte[] fileBytes = System.IO.File.ReadAllBytes(obj.RutaLocal);
            string fileName = obj.Nombre;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Authorization]
        public ActionResult OrdenCompra(HttpPostedFileBase file, int id)
        {

            bool isSavedSuccessfully = true;
            Respuesta result = new Respuesta();
            try
            {
                foreach (string fileName in Request.Files)
                {
                    file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {

                        var objDoc = DirectoryUtil.SaveFileOnFolder(file, "OrdenCompra");

                        result = (HttpContext.Application["proxySistema"] as ISistema).EditDocumento(objDoc).SetRespuesta();

                        if (result.Id == 0)
                        {
                            OrdenCompraDocDTO objTar = new OrdenCompraDocDTO
                            {
                                IdDocumento = Convert.ToInt32(result.Metodo),
                                IdOrdenCompra = id
                            };
                            var resDoc = (HttpContext.Application["proxySistema"] as ISistema).EditOrdenCompraDoc(objTar).SetRespuesta();
                            if (resDoc.Id != 0)
                                isSavedSuccessfully = false;
                            else
                            {
                                result.Metodo = resDoc.Metodo;
                            }
                        }
                        else
                            isSavedSuccessfully = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                LogError.PostErrorMessage(ex, null);
            }

            if (isSavedSuccessfully)
            {
                return Json(new Respuesta { Id = Convert.ToInt32(result.Metodo), Message = "OK" });
            }
            else
            {
                return Json(new Respuesta { Id = -1, Message = result.Descripcion });
            }

        }


    }
}