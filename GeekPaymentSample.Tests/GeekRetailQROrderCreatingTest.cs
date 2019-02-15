using System;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Geek.Payment;

namespace GeekPaymentSample.Tests
{
    public class GeekRetailQROrderCreatingTest
    {
        private GeekChannelOrderFactory geekChannelOrderFactory;
        public GeekRetailQROrderCreatingTest() {
            geekChannelOrderFactory = new GeekChannelOrderFactory();
        }

        [Fact]
        public void TestCreateRetailOrder()
        {
            OrderCreateInfo orderCreateInfo = new OrderCreateInfo();
            orderCreateInfo.MchOrderId = String.Format("ORDER{0}", DateTime.Now.Ticks);
            orderCreateInfo.Title = "聚合订单码下单订单";
            orderCreateInfo.Price = 12300;
            orderCreateInfo.Expire = "10m";
            
            String deviceId = "111";

            ToPayChannelOrder channelOrder = geekChannelOrderFactory.RetailQROrder();
            ToPayOrderInfo orderInfo = channelOrder.Create(orderCreateInfo, deviceId);

            Assert.NotEmpty(orderInfo.MchSerialNo);
            Assert.NotEmpty(orderInfo.OrderId);
            Assert.Equal(orderCreateInfo.MchOrderId, orderInfo.MchOrderId);
            Assert.NotEmpty(orderInfo.PayUrl);
            Assert.NotEmpty(orderInfo.ResultCode);
            Assert.NotEmpty(orderInfo.GateWay);

            Console.WriteLine(JsonConvert.SerializeObject(orderInfo));
        }
    }
}
