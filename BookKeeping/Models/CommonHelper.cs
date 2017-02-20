using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace BookKeeping.Models
{
    public static class CommonHelper
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(anonymousObject))
            {
                var obj = propertyDescriptor.GetValue(anonymousObject);
                expando.Add(propertyDescriptor.Name, obj);
            }

            return (ExpandoObject)expando;
        }

        public static bool Contains(this string source, string value, StringComparison comp)
        {
            return source != null && value != null && source.IndexOf(value, comp) >= 0;
        }

        public static bool ContainsKey(this IDynamicMetaObjectProvider dyn, string key)
        {
            return ((IDictionary<string, object>)dyn).ContainsKey(key);
        }

        public static IEnumerable<KeyValuePair<string, object>> GetProperties(this object obj)
        {
            return obj
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(p => new KeyValuePair<string, object>(p.Name, p.GetValue(obj)));
        }

        public static IEnumerable<object> AsEnumerable(this NameValueCollection collection, Type type, CultureInfo culture)
        {
            var dict = collection.AllKeys.Select(key =>
            {
                var prop = type.GetProperty(key);
                var values = collection[key].Split(',').Select(strval => Convert.ChangeType(strval, prop.PropertyType, culture));
                return new KeyValuePair<PropertyInfo, object[]>(prop, values.ToArray());
            });

            var count = dict.Select(pair => pair.Value.Length).Min();
            var objects = Enumerable.Range(0, count).Select(index => dict.ToDictionary(pair => pair.Key, pair => pair.Value[index]));
            
            return objects.Select(props => {
                var obj = Activator.CreateInstance(type);

                foreach (var p in props)
                {
                    p.Key.SetValue(obj, p.Value);
                }

                return obj;
            });
        }
    }
}