using Xunit;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Refund;
using GeekPaymentSample.Geek.Payment;
using GeekPaymentSample.Geek.Refund;
using GeekPaymentSample.Geek;


namespace GeekPaymentSample.Tests
{
    public class GeekRefundCreatingTest
    {

        [Fact]
        public void TestRefund()
        {
            OrderInfo orderInfo = CreateOrder(); 

            GeekRefund geekRefund = GetRefund();

            string mchRefundId = string.Format("REFUND{0}", DateTime.Now.Ticks);
            int amount = 10000;

            RefundInfo refundInfo = geekRefund.Create(orderInfo.MchOrderId, mchRefundId, amount);
            
            Assert.NotEmpty(refundInfo.MchSerialNo);
            Assert.NotEmpty(refundInfo.RefundId);
            Assert.Equal(refundInfo.MchRefundId, mchRefundId);
            Assert.Equal(refundInfo.Amount, amount); 
            Assert.NotEmpty(refundInfo.RefundTime);
            Assert.NotEmpty(refundInfo.RefundStatus);

            Console.WriteLine(JsonConvert.SerializeObject(refundInfo));
        }

        private GeekRefund GetRefund()
        {
            GeekSign geekSign = new GeekSign(AppProperties.GeekPublicKey, AppProperties.PrivateKey);

            HttpClient httpClient = new HttpClient();
            GeekEndPoint geekEndPoint = new GeekEndPoint(httpClient, geekSign);

            PathComponents pathComponents = new GeekPathComponents("/apps/{0}/orders/{1}/refunds/{2}");
            GeekUriComponents uriComponents = new GeekUriComponents(GeekPaymentProperties.Scheme, GeekPaymentProperties.Host, pathComponents, geekSign);

            HttpResponseParser<RefundInfo> responseParser = new RefundInfoResponseParser();

            return new GeekRefund(geekEndPoint, uriComponents, responseParser, AppProperties.AppID);
        }

        private OrderInfo CreateOrder()
        {
            GeekChannelOrderFactory geekChannelOrderFactory = new GeekChannelOrderFactory();
            OrderCreateInfo orderCreateInfo = new OrderCreateInfo();
            orderCreateInfo.MchOrderId = string.Format("ORDER{0}", DateTime.Now.Ticks);
            orderCreateInfo.Title = "付款码下单订单";
            orderCreateInfo.Price = 12300;
            orderCreateInfo.Expire = "10m";
            
            string authCode = "284896288107264920";
            
            string deviceId = "111";

            ChannelOrder channelOrder = geekChannelOrderFactory.RetailOrder();
            return channelOrder.Create(orderCreateInfo, authCode, deviceId);
        }
    }
}