using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace SyncStyle.EnumUtil
{
    public static class EnumUtil
    {
        public static TEnum Parse<TEnum>(string v) where TEnum : struct, IConvertible
        {
            return (TEnum)Enum.Parse(typeof(TEnum), v, true);
        }
        
    }
}