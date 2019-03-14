using System;
using System.Reflection;

namespace GeekPaymentSample.Geek
{
    public static class EnumExtension
    {
        public static PathComponents GetPath(this Enum item)
        {
            MemberInfo[] mi = item.GetType().GetMember(item.ToString());
            if (mi != null && mi.Length > 0)
            {
                Path attr = Attribute.GetCustomAttribute(mi[0], typeof(Path)) as Path;
                if (attr != null)
                {
                    return attr.PathComponents;
                }
            }
            return null;
        }
        
    }
}