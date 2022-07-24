using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests.Helpers;

public class ObjectPropertySetter
{
    public static void SetPropertyValue(object src, string propName, object value)
    {
        if (src == null) throw new ArgumentException("Value cannot be null.", "src");
        if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

        if (propName.Contains("."))//complex type nested
        {
            var temp = propName.Split(new char[] { '.' }, 2);
            propName = temp.Last();
            src = GetPropertyValue(src, temp.First());
        }

        src.GetType().GetProperty(propName).SetValue(src, value);
    }

    private static object GetPropertyValue(object src, string propName)
    {
        if (src == null) throw new ArgumentException("Value cannot be null.", "src");
        if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

        if (propName.Contains("."))//complex type nested
        {
            var temp = propName.Split(new char[] { '.' }, 2);
            return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
        }
        else
        {
            var prop = src.GetType().GetProperty(propName);
            return prop != null ? prop.GetValue(src, null) : null;
        }
    }
}