using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlayCat.DataService.Helpers
{
    public static class TrimStrings
    {
        public static void Trim(object obj)
        {
            if (obj is null)
                return;

            IEnumerable<PropertyInfo> stringProperties = obj.GetType().GetProperties()
                          .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = stringProperty.GetValue(obj, null) as string;
                stringProperty.SetValue(obj, currentValue?.Trim());
            }
        }
    }
}
