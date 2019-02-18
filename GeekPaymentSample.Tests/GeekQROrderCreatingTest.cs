using Xunit;
using System;
using Newtonsoft.Json;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Geek.Payment;

namespace GeekPaymentSample.Tests
{
    public class GeekQROrderCreatingTest
    {
        [Fact]
        public void Test()
        {
            OrderCreateInfo orderCreateInfo = new OrderCreateInfo();
            orderCreateInfo.MchOrderId = String.Format("ORDER{0}", DateTime.Now.Ticks);
            orderCreateInfo.Title = "聚合下单订单";
            orderCreateInfo.Price = 12300;
            orderCreateInfo.Expire = "10m";

            bool direct = false;
            
            GeekChannelOrderFactory geekChannelOrderFactory = new GeekChannelOrderFactory();
            ToPayAggregateChannelOrder channelOrder = geekChannelOrderFactory.QROrder();
            ToPayOrderInfo orderInfo = channelOrder.Create(orderCreateInfo, direct);

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