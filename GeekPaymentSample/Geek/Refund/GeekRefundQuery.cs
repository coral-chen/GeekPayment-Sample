using Newtonsoft.Json.Linq;
using GeekPaymentSample.Refund;

namespace GeekPaymentSample.Geek.Refund
{
    public class GeekRefundQuery : ChannelRefundQuery
    {

        private HttpResponseParser<RefundInfo> responseParser;
        private GeekEndPoint geekEndPoint;
        private GeekUriComponents uriComponents;
        private string appId;

        public GeekRefundQuery(GeekEndPoint geekEndPoint, GeekUriComponents uriComponents, HttpResponseParser<RefundInfo> responseParser, string appId)
        {
            this.responseParser = responseParser;
            this.geekEndPoint = geekEndPoint;
            this.uriComponents = uriComponents;
            this.appId = appId;
        }

        public RefundInfo Find(string mchOrderId, string mchRefundId)
        {
            string url = uriComponents.Expand(appId, mchOrderId, mchRefundId).Sign(new JObject()).ToUriString();

            JObject responseData = geekEndPoint.Get(url);

            return responseParser.Parse(responseData);
        }
    }
}