using System.Net.Http;
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
    /// 付款码渠道订单
    /// </summary>
    public class GeekChannelRetailOrder : ChannelOrder
    {
        private GeekUriComponents uriComponent;
        private HttpClient httpClient;
        private GeekSign geekSign;
        private string notifyUrl;
        private string appId;

        public GeekChannelRetailOrder(HttpClient httpClient, GeekUriComponents uriComponent, GeekSign geekSign, string notifyUrl, string appId)
        {
            this.uriComponent = uriComponent;
            this.httpClient = httpClient;
            this.geekSign = geekSign;
            this.notifyUrl = notifyUrl;
            this.appId = appId;
        }

        public OrderInfo Create(OrderCreateInfo orderCreateInfo, string authCode, string deviceId)
        {
            try 
            {
                JObject requestContent = GenerateRequestContent(orderCreateInfo, authCode, deviceId);
                uriComponent = uriComponent.Expand(orderCreateInfo.MchOrderId);
                String nonceStr = RandomStringUtils.Random(10);

                String fullPath = uriComponent.ToUriString();
                String sign = geekSign.Sign(requestContent, nonceStr, fullPath);

                String url = uriComponent.QueryParams(nonceStr, sign, GeekSign.SignType).ToUriString();

                String serializeRequestContent = requestContent.ToString();
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
                requestMessage.Content = new StringContent(serializeRequestContent, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = httpClient.SendAsync(requestMessage).Result;
                
                return DeserializeRepsonseContent(responseMessage.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("GeekChannelRetailOrder Create Excepiton");
                Console.WriteLine("Message: {0}", e.Message);
                throw new ApplicationException();
            }
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

        private OrderInfo DeserializeRepsonseContent(String content)
        {
            JObject contentJson = JObject.Parse(content);
            JObject orderInfoJson = (JObject)contentJson["data"];

            OrderInfo orderInfo = new OrderInfo();
            orderInfo.MchSerialNo = orderInfoJson["mch_serial_no"].ToString();
            orderInfo.OrderId = orderInfoJson["order_id"].ToString();
            orderInfo.MchOrderId = orderInfoJson["mch_order_id"].ToString();
            orderInfo.Price = int.Parse(orderInfoJson["price"].ToString());
            orderInfo.TotalAmount = int.Parse(orderInfoJson["total_amount"].ToString());
            orderInfo.MchDiscount = int.Parse(orderInfoJson["mch_discount"].ToString());
            orderInfo.PlatformDiscount = int.Parse(orderInfoJson["plateform_discount"].ToString());
            orderInfo.PayAmount = int.Parse(orderInfoJson["pay_amount"].ToString());
            orderInfo.ToSettleAmount = int.Parse(orderInfoJson["to_settle_amount"].ToString());
            orderInfo.Channel = orderInfoJson["channel"].ToString();
            orderInfo.CreateTime = orderInfoJson["create_time"].ToString();
            orderInfo.PayTime = orderInfoJson["pay_time"].ToString();
            orderInfo.OrderStatus = orderInfoJson["order_status"].ToString();

            return orderInfo;
        }
    }
}