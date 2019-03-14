using Newtonsoft.Json.Linq;
using GeekPaymentSample.Refund;

namespace GeekPaymentSample.Geek.Refund
{
    public class GeekRefund : ChannelRefund
    {
        private HttpResponseParser<RefundInfo> responseParser;

        private GeekEndPoint geekEndPoint;

        private GeekUriComponents uriComponents;

        private string appId;

        public GeekRefund(GeekEndPoint geekEndPoint, GeekUriComponents uriComponents, HttpResponseParser<RefundInfo> responseParser, string appId)
        {
            this.geekEndPoint = geekEndPoint;
            this.uriComponents = uriComponents;
            this.responseParser = responseParser;
            this.appId = appId;
        }

        public RefundInfo Create(string mchOrderId, string mchRefundId, int amount)
        {
            JObject requestBody = new JObject();
            requestBody.Add(new JProperty("amount", amount));

            string url = uriComponents.RefundUri().Expand(appId, mchOrderId, mchRefundId).Sign(requestBody).ToUriString();

            JObject responseData = geekEndPoint.Put(url, requestBody);

            return responseParser.Parse(responseData);
        }
    }
}