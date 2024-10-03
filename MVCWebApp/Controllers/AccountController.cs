using System.Web.Mvc;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.DirectoryServices.AccountManagement;
using com.msc.infraestructure.entities;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.infraestructure.utils;
using com.msc.infraestructure.entities.mvc;
using com.msc.services.interfaces;
using System.Configuration;
using System.Web.Helpers;
using System.Web;

namespace com.msc.frontend.mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        private ExternoDTO MapUsuarioToExternoDTO(UsuarioDTO obj)
        {
            ExternoDTO user = new ExternoDTO {
             Id = obj.Id,
             Usuario = obj.Login,
             Perfiles = obj.Perfiles
            };
            return user;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SetProfile(int id)
        {
            Respuesta result = new Respuesta();
            try
            {
                var user = (ExternoDTO)Session["Usuario"];
                Session["Perfil"] = (from p in user.Perfiles
                                     where p.Id == id
                                     select p).FirstOrDefault();
                result =  MessagesApp.BackAppMessage(MessageCode.AuthenticateOK);

                return Json(result);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return Json(result);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorization]
        public JsonResult ChangeP(string Usuario, string Clave, string Reingrese)
        {
            Respuesta result = new Respuesta();
            try
            {
                if (Clave == string.Empty || Reingrese == string.Empty)
                {
                    result = MessagesApp.BackAppMessage(MessageCode.AuthenticateBadPassword);
                }
                else if (Clave != Reingrese)
                {
                    result = MessagesApp.BackAppMessage(MessageCode.PasswordNotMatching);
                }
                else {
                    result = (HttpContext.Application["proxySeguridad"] as ISeguridad).ChangePassword(Usuario, Clave).SetRespuesta();
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return Json(result);
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult Login(Login model)
        {
            Respuesta result = new Respuesta();
            try
            {

                if (!ModelState.IsValid)
                {
                    result = MessagesApp.BackAppMessage(MessageCode.InvalidFields, ViewData.ModelState);
                }
                else
                {

                    if (model.UserExternal == true)
                    {
                        result = (HttpContext.Application["proxySeguridad"] as ISeguridad).AuthenticateExterno(model.GetLoginDTO()).SetRespuesta();

                        if (result.Id == 0)
                        {
                            var user = (HttpContext.Application["proxySeguridad"] as ISeguridad).GetUserExternalAuthenticated(model.Usuario);

                            var jwtToken = Jwt.GenerateJWTAuthetication(model.Usuario, "role");
                            var validUserName = Jwt.ValidateToken(jwtToken);

                            switch (user.Perfiles.Count)
                            {
                                case 0:
                                    result = MessagesApp.BackAppMessage(MessageCode.NotProfileFound);
                                    break;
                                case 1:
                                    Session["Tipo"] = "E";
                                    Session["Perfil"] = user.Perfiles[0];
                                    Session["Usuario"] = user;
                                    result.PilaError = jwtToken;

                                    break;
                                default:
                                    result.Metodo = JsonConvert.SerializeObject(user.Perfiles);
                                    Session["Perfil"] = user.Perfiles[0];
                                    Session["Tipo"] = "E";
                                    Session["Usuario"] = user;
                                    result.PilaError = jwtToken;

                                    break;
                            }
                        }
                    }
                    else
                    {
                        using (PrincipalContext pc = new PrincipalContext(ContextType.Machine, ConfigurationManager.AppSettings["CurrentDomain"]))
                        {

                            bool isValid = pc.ValidateCredentials(model.Usuario, model.Password);

                            if (isValid)
                            {
                                var user = (HttpContext.Application["proxySeguridad"] as ISeguridad).GetUserAuthenticated(model.Usuario);

                                var jwtToken = Jwt.GenerateJWTAuthetication(model.Usuario, "role");
                                var validUserName = Jwt.ValidateToken(jwtToken);

                                switch (user.Perfiles.Count)
                                {
                                    case 0:
                                        result = MessagesApp.BackAppMessage(MessageCode.NotProfileFound);
                                        break;
                                    case 1:
                                        Session["Tipo"] = "I";
                                        Session["Perfil"] = user.Perfiles[0];
                                        Session["Usuario"] = MapUsuarioToExternoDTO(user);
                                        result.PilaError = jwtToken;

                                        break;
                                    default:
                                        result.Metodo = JsonConvert.SerializeObject(user.Perfiles);
                                        Session["Perfil"] = user.Perfiles[0];
                                        Session["Tipo"] = "I";
                                        Session["Usuario"] = MapUsuarioToExternoDTO(user);
                                        result.PilaError = jwtToken;
                                        break;
                                }
                            }
                            else
                            {
                                result = MessagesApp.BackAppMessage(MessageCode.AuthenticationBadDomain);
                            }
                        }
                    }
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, model);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return Json(result);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session["Usuario"] = null;
            Session["Perfil"] = null;
            return RedirectToAction("Login");
        }
    }
}