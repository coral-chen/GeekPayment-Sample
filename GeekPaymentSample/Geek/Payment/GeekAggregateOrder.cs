using Newtonsoft.Json.Linq;
using GeekPaymentSample.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    public class GeekAggregateOrder : ToPayAggregateChannelOrder
    {
        private string appId;
        private string notifyUrl;
        private string returnUrl;   
        private GeekEndPoint geekEndPoint;
        private GeekUriComponents uriComponents;
        private HttpResponseParser<ToPayOrderInfo> responseParser;

        public GeekAggregateOrder(GeekEndPoint geekEndPoint, GeekUriComponents uriComponents, HttpResponseParser<ToPayOrderInfo> responseParser, string appId, string returnUrl, string notifyUrl)
        {
            this.geekEndPoint = geekEndPoint;
            this.uriComponents = uriComponents;
            this.responseParser = responseParser;
            this.appId = appId;
            this.notifyUrl = notifyUrl;
            this.returnUrl = returnUrl;
        }

        public ToPayOrderInfo Create(OrderCreateInfo orderCreateInfo, bool direct)
        {
            JObject requestBody = GenerateRequestContent(orderCreateInfo, direct);

            string url = uriComponents.QROrderUri().Expand(appId, orderCreateInfo.MchOrderId).Sign(new JObject(requestBody)).ToUriString();

            JObject responseData = geekEndPoint.Put(url, requestBody);

            return responseParser.Parse(responseData);
        }

        private JObject GenerateRequestContent(OrderCreateInfo orderCreateInfo, bool direct)
        {
            JObject requestContent = new JObject();
            requestContent.Add(new JProperty("title", orderCreateInfo.Title));
            requestContent.Add(new JProperty("price", orderCreateInfo.Price));
            requestContent.Add(new JProperty("expire", orderCreateInfo.Expire));
            requestContent.Add(new JProperty("return_url", returnUrl));
            requestContent.Add(new JProperty("notify_url", notifyUrl));
            requestContent.Add(new JProperty("direct", direct));

            return requestContent;
        }
        
    }
}