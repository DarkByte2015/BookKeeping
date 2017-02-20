using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace BookKeeping.Models
{
    public static class Parser1C
    {
        // Такое решение было сделано ради расширяемости. Пусть даже это лишь тестовое задание,
        // которое нигде не будет использоваться, но мне кажется - так правильнее.
        // Теоретически можно сюда передать любую структуру которая может встретиться в файле (точнее ее тип).
        public static IEnumerable<object> Parse(Stream stream, IEnumerable<Type> types)
        {
            using (var reader = new StreamReader(stream, Encoding.GetEncoding("windows-1251")))
            {
                var line = reader.ReadLine();

                if (line != "1CClientBankExchange")
                    throw new FormatException("Этот файл не подходит по формату.");

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();

                    foreach (var info in types.Select(GetPropNames))
                    {
                        if (line.Contains((string)info.TypeName, StringComparison.OrdinalIgnoreCase))
                        {
                            yield return Parse(reader, info.Type, info.PropNames);
                            break;
                        }
                    }
                }
            }
        }

        private static object Parse(StreamReader reader, Type type, string[] names)
        {
            var endOfSection = false;
            var result = Activator.CreateInstance(type);
            var culture = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            culture.NumberFormat.NumberDecimalSeparator = ".";

            do
            {
                var line = reader.ReadLine();
                endOfSection = line.StartsWith("Конец");
                var splitted = line.Split('=');
                var key = splitted[0];

                if (!endOfSection && names.Contains(key))
                {
                    var prop = type.GetProperty(key);
                    var value = Convert.ChangeType(splitted[1], prop.PropertyType, culture);
                    prop.SetValue(result, value);
                }
            }
            while (!endOfSection && !reader.EndOfStream);

            return result;
        }

        private static dynamic GetPropNames(Type type)
        {
            var key = type.Name.Replace('_', ' ');
            var value = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToArray();
            return new { Type = type, TypeName = key, PropNames = value };
        }
    }

}