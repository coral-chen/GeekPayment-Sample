using System;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Geek.Payment;

namespace GeekPaymentSample.Tests
{
    public class GeekRetailOrderCreatingTest
    {
        private GeekChannelOrderFactory geekChannelOrderFactory;
        public GeekRetailOrderCreatingTest() {
            geekChannelOrderFactory = new GeekChannelOrderFactory();
        }

        [Fact]
        public void TestCreateRetailOrder()
        {
            OrderCreateInfo orderCreateInfo = new OrderCreateInfo();
            orderCreateInfo.MchOrderId = String.Format("ORDER{0}", DateTime.Now.Ticks);
            orderCreateInfo.Title = "付款码下单订单";
            orderCreateInfo.Price = 12300;
            orderCreateInfo.Expire = "10m";
            
            string authCode = "284896288107264920";
            
            string deviceId = "111";

            ChannelOrder channelOrder = geekChannelOrderFactory.RetailOrder();
            OrderInfo orderInfo = channelOrder.Create(orderCreateInfo, authCode, deviceId);

            Assert.NotEmpty(orderInfo.MchSerialNo);
            Assert.NotEmpty(orderInfo.OrderId);
            Assert.Equal(orderCreateInfo.MchOrderId, orderInfo.MchOrderId);
            Assert.Equal(orderCreateInfo.Price, orderInfo.Price);
            Assert.Equal(0, orderInfo.MchDiscount);
            Assert.Equal(0, orderInfo.PlatformDiscount);
            Assert.True(orderInfo.TotalAmount > 0);
            Assert.True(orderInfo.PayAmount > 0);
            Assert.True(orderInfo.ToSettleAmount > 0);
            Assert.NotEmpty(orderInfo.Channel);
            Assert.NotEmpty(orderInfo.CreateTime);
            Assert.NotEmpty(orderInfo.PayTime);
            Assert.NotEmpty(orderInfo.OrderStatus);

            Console.WriteLine(JsonConvert.SerializeObject(orderInfo));
        }
    }
}
