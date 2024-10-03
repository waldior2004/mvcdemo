using com.msc.infraestructure.utils;
using com.msc.services.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

namespace com.msc.frontend.mvc
{
    public class JwtAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                var request = filterContext.HttpContext.Request;
                var token = request.Cookies["jwt"].Value;
                var authenticated = false;

                if (token != null)
                {
                    var userName = Jwt.ValidateToken(token);
                    if (userName == null)
                    {
                        filterContext.Controller.TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.ExpirateSession).Descripcion;
                    }
                    else
                        authenticated = true;
                }
                else
                {
                    filterContext.Controller.TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.ExpirateSession).Descripcion;
                }

                if (authenticated)
                {
                    PerfilDTO sesion = (PerfilDTO)filterContext.HttpContext.Session["Perfil"];
                    if (sesion == null)
                    {
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            switch (GetExpectedReturnType(filterContext).Name)
                            {
                                case "JsonResult":
                                    filterContext.Result = new JsonResult
                                    {
                                        Data = MessagesApp.BackAppMessage(MessageCode.ExpirateSession)
                                    };
                                    break;

                                default:
                                    filterContext.Controller.TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.ExpirateSession).Descripcion;
                                    filterContext.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary(
                                        new
                                        {
                                            controller = "Home",
                                            action = "ErrorJson"
                                        }));
                                    break;
                            }
                        }
                        else
                        {
                            filterContext.Controller.TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.ExpirateSession).Descripcion;
                            filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new
                                {
                                    controller = "Home",
                                    action = "Error"
                                }));
                        }
                    }
                    else
                    {
                        var search = string.Format("/{0}/{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName).Trim();
                        var found = false;
                        var xml = XDocument.Parse(sesion.Permisos);
                        var elements = xml.Descendants("url");
                        foreach (var elem in elements)
                        {
                            var attr = elem.Attribute("value");
                            if (attr != null)
                            {
                                if (attr.Value == search)
                                    found = true;
                            }
                        }
                        if (!found)
                            this.HandleUnauthorizedRequest(filterContext);
                        else
                        {
                            filterContext.Controller.ViewBag.Usuario = (filterContext.HttpContext.Session["Usuario"] as ExternoDTO).Usuario;
                            filterContext.Controller.ViewBag.Menu = new HtmlString((filterContext.HttpContext.Session["Perfil"] as PerfilDTO).MenuSup);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                filterContext.Controller.TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "Error"
                    }));
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                switch (GetExpectedReturnType(filterContext).Name)
                {
                    case "JsonResult":
                        filterContext.Result = new JsonResult
                        {
                            Data = MessagesApp.BackAppMessage(MessageCode.UnAuthorizedRequest)
                        };
                        break;

                    default:
                        filterContext.Controller.TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.UnAuthorizedRequest).Descripcion;
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Home",
                                action = "ErrorJson"
                            }));
                        break;
                }
            }
            else
            {
                filterContext.Controller.TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.UnAuthorizedRequest).Descripcion;
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "Error"
                    }));
            }

        }

        private Type GetExpectedReturnType(AuthorizationContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            Type controllerType = filterContext.Controller.GetType();
            MethodInfo actionMethodInfo = default(MethodInfo);
            try
            {
                actionMethodInfo = controllerType.GetMethod(actionName);
            }
            catch (AmbiguousMatchException ex)
            {
                var actionParams = filterContext.ActionDescriptor.GetParameters();
                List<Type> paramTypes = new List<Type>();
                foreach (var p in actionParams)
                {
                    paramTypes.Add(p.ParameterType);
                }

                actionMethodInfo = controllerType.GetMethod(actionName, paramTypes.ToArray());
            }

            return actionMethodInfo.ReturnType;
        }
    }
}