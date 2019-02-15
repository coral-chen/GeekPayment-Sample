using System;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Geek.Payment;

namespace GeekPaymentSample.Tests
{
    public class GeekNativeQROrderCreatingTest
    {
        private GeekChannelOrderFactory geekChannelOrderFactory;
        public GeekNativeQROrderCreatingTest() {
            geekChannelOrderFactory = new GeekChannelOrderFactory();
        }

        [Fact]
        public void TestCreateRetailOrder()
        {
            OrderCreateInfo orderCreateInfo = new OrderCreateInfo();
            orderCreateInfo.MchOrderId = String.Format("ORDER{0}", DateTime.Now.Ticks);
            orderCreateInfo.Title = "原生二维码下单订单";
            orderCreateInfo.Price = 12300;
            orderCreateInfo.Expire = "10m";
            
            String channel = "wechat";

            ToPayNativeChannelOrder channelOrder = geekChannelOrderFactory.NativeQROrder();
            ToPayNativeOrderInfo orderInfo = channelOrder.Create(orderCreateInfo, channel);

            Assert.NotEmpty(orderInfo.MchSerialNo);
            Assert.NotEmpty(orderInfo.OrderId);
            Assert.Equal(orderCreateInfo.MchOrderId, orderInfo.MchOrderId);
            Assert.NotEmpty(orderInfo.PayUrl);
            Assert.NotEmpty(orderInfo.ResultCode);
            Assert.NotEmpty(orderInfo.QRCode);

            Console.WriteLine(JsonConvert.SerializeObject(orderInfo));
        }
    }
}
