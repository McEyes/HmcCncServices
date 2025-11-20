using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum argEnum)
        {
            FieldInfo fieldInfo = argEnum.GetType().GetField(argEnum.ToString());
            if (fieldInfo == null) return string.Empty;
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? argEnum.ToString() : attribute.Description;
        }

        public static string ToIntString(this Enum argEnum)
        {
            return Convert.ToInt32(argEnum).ToString();
        }
        public static string ToUIntString(this Enum argEnum)
        {
            return Convert.ToUInt32(argEnum).ToString();
        }
        public static int IntValue(this Enum argEnum)
        {
            return Convert.ToInt32(argEnum);
        }
        public static uint UIntValue(this Enum argEnum)
        {
            return Convert.ToUInt32(argEnum);
        }
        public static long LongValue(this Enum argEnum)
        {
            return Convert.ToInt64(argEnum);
        }
    }
}
