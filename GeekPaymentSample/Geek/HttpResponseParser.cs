using Newtonsoft.Json.Linq;

namespace GeekPaymentSample.Geek
{
    public interface HttpResponseParser<T>
    {
         T Parse(JObject responseData);
    }
}