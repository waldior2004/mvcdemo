using System;
using System.Reflection;

namespace com.msc.services.dto
{
    public static class AutoMapper
    {
        public static T CreateMap<I, T>(this I source)
        {

            object obj = typeof(T).GetConstructor(System.Type.EmptyTypes).Invoke(null);
            foreach (PropertyInfo property in typeof(I).GetProperties())
            {
                var prop = source.GetType().GetProperty(property.Name);
                var propd = obj.GetType().GetProperty(property.Name);
                if (propd != null)
                {
                    if (propd.PropertyType == typeof(int))
                        propd.SetValue(obj, Convert.ToInt32(prop.GetValue(source)));
                    else if (propd.PropertyType == typeof(string))
                        propd.SetValue(obj, prop.GetValue(source));
                    else if (propd.PropertyType == typeof(DateTime))
                    {
                        if (prop.GetValue(source) != null)
                            propd.SetValue(obj, prop.GetValue(source));
                    }
                    else if (propd.PropertyType == typeof(DateTime?))
                    {
                        propd.SetValue(obj, prop.GetValue(source));
                    }
                    else if (propd.PropertyType == typeof(bool))
                    {
                        propd.SetValue(obj, prop.GetValue(source));
                    }
                    else if (propd.PropertyType == typeof(int?))
                    {
                        if (prop.GetValue(source) != null)
                            propd.SetValue(obj, Convert.ToInt32(prop.GetValue(source)));
                    }
                    else if (propd.PropertyType == typeof(Int16?))
                    {
                        if (prop.GetValue(source) != null)
                            propd.SetValue(obj, Convert.ToInt16(prop.GetValue(source)));
                    }
                    else if (propd.PropertyType == typeof(Int16))
                    {
                        if (prop.GetValue(source) != null)
                            propd.SetValue(obj, Convert.ToInt16(prop.GetValue(source)));
                    }
                    else if (propd.PropertyType == typeof(Byte))
                    {
                        if (prop.GetValue(source) != null)
                            propd.SetValue(obj, Convert.ToByte(prop.GetValue(source)));
                    }
                    else if (propd.PropertyType == typeof(decimal))
                    {
                        if (prop.GetValue(source) != null)
                            propd.SetValue(obj, Convert.ToDecimal(prop.GetValue(source)));
                    }
                }
            }
            return (T)obj;
        }
    }
}
