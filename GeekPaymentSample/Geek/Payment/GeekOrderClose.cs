using Newtonsoft.Json.Linq;
using GeekPaymentSample.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    public class GeekOrderClose : ChannelOrderClose
    {
        private GeekUriComponents uriComponents;
        private GeekEndPoint geekEndPoint;
        private HttpResponseParser<OrderInfo> responseParser;
        private string appId;

        public GeekOrderClose(GeekUriComponents uriComponents, GeekEndPoint geekEndPoint, HttpResponseParser<OrderInfo> responseParser, string appId)
        {
            this.uriComponents = uriComponents;
            this.geekEndPoint = geekEndPoint;
            this.responseParser = responseParser;
            this.appId = appId;
        }

        public OrderInfo Close(string mchOrderId)
        {
            string url = uriComponents.OrderCloseUri().Expand(appId, mchOrderId).Sign(new JObject()).ToUriString();

            JObject responseData = geekEndPoint.Delete(url);

            return responseParser.Parse(responseData);
        }
    }
}