using ScrapeTheRepo.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using static ScrapeTheRepo.Extensions.Attributes;

namespace ScrapeTheRepo.Extensions
{
    public static class EnumExtensions
    {
        public static T GetEnumByStringValueAttribute<T>(string value) where T : Enum
        {
            Type enumType = typeof(T);
            foreach (Enum val in Enum.GetValues(enumType))
            {
                FieldInfo fi = enumType.GetField(val.ToString())!;
                StringValueAttribute[] attributes = (StringValueAttribute[])fi.GetCustomAttributes(
                    typeof(StringValueAttribute), false);
                StringValueAttribute attr = attributes[0];
                if (attr.Value == value)
                {
                    return (T)val;
                }
            }
            return default!;
        }
    }
}
