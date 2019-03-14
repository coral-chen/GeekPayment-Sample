using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Geek.Utils;

namespace GeekPaymentSample.Geek.Payment
{
    /// <summary>
    /// 付款码订单
    /// </summary>
    public class GeekRetailOrder : ChannelOrder
    {
        private GeekUriComponents uriComponent;
        private GeekEndPoint geekEndPoint;
        private HttpResponseParser<OrderInfo> responseParser;
        private string notifyUrl;
        private string appId;

        public GeekRetailOrder(GeekEndPoint geekEndPoint, GeekUriComponents uriComponent, HttpResponseParser<OrderInfo> responseParser, string notifyUrl, string appId)
        {
            this.uriComponent = uriComponent;
            this.geekEndPoint = geekEndPoint;
            this.responseParser = responseParser;
            this.notifyUrl = notifyUrl;
            this.appId = appId;
        }

        public OrderInfo Create(OrderCreateInfo orderCreateInfo, string authCode, string deviceId)
        {
            JObject requestContent = GenerateRequestContent(orderCreateInfo, authCode, deviceId);
            
            String url = uriComponent.RetailOrderUri().Expand(appId, orderCreateInfo.MchOrderId).Sign(new JObject(requestContent)).ToUriString();

            JObject contentData = geekEndPoint.Put(url, requestContent);
        
            return responseParser.Parse(contentData);
        }

        private JObject GenerateRequestContent(OrderCreateInfo orderCreateInfo, string authCode, string deviceId)
        {
            JObject requestContent = new JObject();
            requestContent.Add(new JProperty("title", orderCreateInfo.Title));
            requestContent.Add(new JProperty("price", orderCreateInfo.Price));
            requestContent.Add(new JProperty("expire", orderCreateInfo.Expire));
            requestContent.Add(new JProperty("auth_code", authCode));
            requestContent.Add(new JProperty("device_id", deviceId));
            requestContent.Add(new JProperty("notify_url", notifyUrl));

            return requestContent;
        }
    }
}