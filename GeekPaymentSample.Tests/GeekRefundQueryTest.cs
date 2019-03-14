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
    public class GeekRefundQueryTest
    {
        private GeekEndPoint geekEndPoint;
        private GeekUriComponents uriComponents;
        private HttpResponseParser<RefundInfo> responseParser;

        public GeekRefundQueryTest()
        {
            GeekSign geekSign = new GeekSign(AppProperties.GeekPublicKey, AppProperties.PrivateKey);

            HttpClient httpClient = new HttpClient();
            geekEndPoint = new GeekEndPoint(httpClient, geekSign);

            uriComponents = new GeekUriComponents(GeekPaymentProperties.Scheme, GeekPaymentProperties.Host, geekSign);

            responseParser = new RefundInfoResponseParser();
        }

        [Fact]
        public void TestRefund()
        {
            OrderInfo orderInfo = CreateOrder(); 

            GeekRefund geekRefund = GetRefund();

            string mchRefundId = string.Format("REFUND{0}", DateTime.Now.Ticks);
            int amount = 10000;

            RefundInfo refundInfo = geekRefund.Create(orderInfo.MchOrderId, mchRefundId, amount);
            
            GeekRefundQuery refundQuery = GetQuery();
            RefundInfo existRefundInfo = refundQuery.Find(orderInfo.MchOrderId, mchRefundId);
            
            Assert.Equal(refundInfo.MchSerialNo, existRefundInfo.MchSerialNo);
            Assert.Equal(refundInfo.RefundId, existRefundInfo.RefundId);
            Assert.Equal(refundInfo.MchRefundId, existRefundInfo.MchRefundId);
            Assert.Equal(refundInfo.Amount, existRefundInfo.Amount); 
            Assert.NotEmpty(existRefundInfo.RefundTime);
            Assert.NotEmpty(existRefundInfo.RefundStatus);

            Console.WriteLine(JsonConvert.SerializeObject(existRefundInfo));
        }

        private GeekRefund GetRefund()
        {
            return new GeekRefund(geekEndPoint, uriComponents, responseParser, AppProperties.AppID);
        }

        private GeekRefundQuery GetQuery()
        {
            return new GeekRefundQuery(geekEndPoint, uriComponents, responseParser, AppProperties.AppID);
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