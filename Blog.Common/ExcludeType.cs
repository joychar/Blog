using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common
{
    public class ExcludeType
    {
        static List<string> Types = new List<string>() { "chinaedu.oms.model.entity", "icollection" };

        public static bool IsExclude(PropertyInfo prop)
        {
            var re = true;
            foreach (var tin in Types)
            {
                if (prop.PropertyType.FullName.ToLower().Contains(tin))
                    return false;
            }
            return re;
        }
    }
}
