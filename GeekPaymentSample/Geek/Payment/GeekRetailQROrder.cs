using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using System.Text;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Geek.Utils;
using GeekPaymentSample.Geek.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    /// <summary>
    /// 聚合订单码订单
    /// </summary>
    public class GeekRetailQROrder : ToPayChannelOrder
    {

        private GeekUriComponents uriComponent;
        private HttpResponseParser<ToPayOrderInfo> responseParser;
        private GeekEndPoint geekEndPoint;

        private string notifyUrl;
        private string appId;

        public GeekRetailQROrder(GeekEndPoint geekEndPoint, GeekUriComponents uriComponent, HttpResponseParser<ToPayOrderInfo> responseParser, string notifyUrl, string appId)
        {
            this.uriComponent = uriComponent;
            this.geekEndPoint = geekEndPoint;
            this.responseParser = responseParser;
            this.notifyUrl = notifyUrl;
            this.appId = appId;
        }

        public ToPayOrderInfo Create(OrderCreateInfo orderCreateInfo, string deviceId)
        {
            JObject requestContent = GenerateRequestContent(orderCreateInfo, deviceId);

            string url = uriComponent.Expand(appId, orderCreateInfo.MchOrderId).Sign(new JObject(requestContent)).ToUriString();

            JObject responseContent = geekEndPoint.Put(url, requestContent);

            return responseParser.Parse(responseContent);
        }

        private JObject GenerateRequestContent(OrderCreateInfo orderCreateInfo, string deviceId)
        {
            JObject requestContent = new JObject();
            requestContent.Add(new JProperty("title", orderCreateInfo.Title));
            requestContent.Add(new JProperty("price", orderCreateInfo.Price));
            requestContent.Add(new JProperty("expire", orderCreateInfo.Expire));
            requestContent.Add(new JProperty("device_id", deviceId));
            requestContent.Add(new JProperty("notify_url", notifyUrl));

            return requestContent;
        }
        
    }
}