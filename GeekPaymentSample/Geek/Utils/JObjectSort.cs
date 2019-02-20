using System.Linq;
using Newtonsoft.Json.Linq;

namespace GeekPaymentSample.Geek.Utils
{
    public class JObjectSort
    {
        public static JObject Sort(JObject data)
        {
            var props = data.Properties().ToList();
            foreach (var prop in props)
            {
                prop.Remove();
            }

            foreach (var prop in props.OrderBy(p=>p.Name))
            {
                data.Add(prop);
            }

            return data;
        }
    }
}