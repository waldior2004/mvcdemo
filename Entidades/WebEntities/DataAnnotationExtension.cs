using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace com.msc.infraestructure.entities.dataannotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class MyRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var required = new RequiredAttribute();
            return value != null && required.IsValid(value.ToString().Trim()) && value.ToString() != "0";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class MyDateTimeAttribute : RegularExpressionAttribute
    {
        public MyDateTimeAttribute()
            : base(@"^((((31\/(0?[13578]|1[02]))|((29|30)\/(0?[1,3-9]|1[0-2])))\/(1[6-9]|[2-9]\d)?\d{2})|(29\/0?2\/(((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))|(0?[1-9]|1\d|2[0-8])\/((0?[1-9])|(1[0-2]))\/((1[6-9]|[2-9]\d)?\d{2}))$")
        {
            ErrorMessage = "La Fecha debe de estar en formato (dd/mm/yyyy)";
        }
    }

}