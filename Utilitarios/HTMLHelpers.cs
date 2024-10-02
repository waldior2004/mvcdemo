using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using com.msc.infraestructure.entities.mvc;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.utils
{
    public static class MyHtmlHelpers
    {
        public static string BuildPermisos(List<PerfilControl> lst)
        {
            StringBuilder strList = new StringBuilder();
            strList.Append("<root><url value='/Home/Index' />");
            foreach (var item in lst) {
                strList.Append("<url value='" + item.Control.Url + "' />");
            }
            strList.Append("</root>");
            return strList.ToString();
        }
    }
}