using Newtonsoft.Json.Linq;
using GeekPaymentSample.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    public class GeekChannelOrderQuery : ChannelOrderQuery
    {
        private GeekUriComponents uriComponents;
        private GeekEndPoint geekEndPoint;
        private HttpResponseParser<OrderInfo> responseParser;
        private string appId;

        public GeekChannelOrderQuery(GeekUriComponents uriComponents, GeekEndPoint geekEndPoint, HttpResponseParser<OrderInfo> responseParser, string appId)
        {
            this.uriComponents = uriComponents;
            this.geekEndPoint = geekEndPoint;
            this.responseParser = responseParser;
            this.appId = appId;
        }

        public OrderInfo Find(string mchOrderId)
        {
            string url = uriComponents.Expand(appId, mchOrderId).Sign(new JObject()).ToUriString();

            JObject responseData = geekEndPoint.Get(url);

            return responseParser.Parse(responseData);
        }
    }
}