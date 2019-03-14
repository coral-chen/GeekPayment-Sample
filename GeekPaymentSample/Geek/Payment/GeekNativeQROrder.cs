using Newtonsoft.Json.Linq;
using GeekPaymentSample.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    public class GeekNativeQROrder : ToPayNativeChannelOrder
    {
        private GeekUriComponents uriComponents;
        private GeekEndPoint geekEndPoint;
        private HttpResponseParser<ToPayNativeOrderInfo> responseParser;
        private string appId;
        private string returnUrl;
        private string notifyUrl;
        
        public GeekNativeQROrder(GeekEndPoint geekEndPoint, GeekUriComponents uriComponents, HttpResponseParser<ToPayNativeOrderInfo> responseParser, string returnUrl, string notifyUrl, string appId)
        {
            this.geekEndPoint = geekEndPoint;
            this.responseParser = responseParser;
            this.uriComponents = uriComponents;
            this.appId = appId;
            this.returnUrl = returnUrl;
            this.notifyUrl = notifyUrl;
        }

        public ToPayNativeOrderInfo Create(OrderCreateInfo orderCreateInfo, string channel)
        {
            JObject requestBody = GenerateRequestContent(orderCreateInfo, channel);

            string url = uriComponents.NativeQROrderUri().Expand(appId, orderCreateInfo.MchOrderId).Sign(new JObject(requestBody)).ToUriString();

            JObject contentData = geekEndPoint.Put(url, requestBody);

            return responseParser.Parse(contentData);
        }

        private JObject GenerateRequestContent(OrderCreateInfo orderCreateInfo, string channel)
        {
            JObject requestContent = new JObject();
            requestContent.Add(new JProperty("title", orderCreateInfo.Title));
            requestContent.Add(new JProperty("price", orderCreateInfo.Price));
            requestContent.Add(new JProperty("expire", orderCreateInfo.Expire));
            requestContent.Add(new JProperty("channel", channel));
            requestContent.Add(new JProperty("return_url", returnUrl));
            requestContent.Add(new JProperty("notify_url", notifyUrl));

            return requestContent;
        }
    }
}