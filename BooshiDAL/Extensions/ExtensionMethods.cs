using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Extensions
{
    public static class ExtensionMethods
    {
            public static void CopyAllPropertiesTo<T>(this T source, T target)
            {
                var type = typeof(T);
                foreach (var sourceProperty in type.GetProperties())
                {
                    var targetProperty = type.GetProperty(sourceProperty.Name);
                    targetProperty.SetValue(target, sourceProperty.GetValue(source, null), null);
                }
                foreach (var sourceField in type.GetFields())
                {
                    var targetField = type.GetField(sourceField.Name);
                    targetField.SetValue(target, sourceField.GetValue(source));
                }
            }
    }
}
