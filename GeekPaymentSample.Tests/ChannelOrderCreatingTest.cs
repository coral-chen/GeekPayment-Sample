using System;
using Xunit;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Geek.Payment;

namespace GeekPaymentSample.Tests
{
    public class ChannelOrderCreatingTest
    {
        private GeekChannelOrderFactory geekChannelOrderFactory;

        public ChannelOrderCreatingTest() {
            geekChannelOrderFactory = new GeekChannelOrderFactory();
        }

        [Fact]
        public void TestCreateChannelOrder()
        {
            OrderCreateInfo orderCreateInfo = new OrderCreateInfo();
            String authCode = "";

            ChannelOrder channelOrder = geekChannelOrderFactory.channelOrder(orderCreateInfo, authCode);
            OrderInfo orderInfo = channelOrder.Create();

            Assert.NotEmpty(orderInfo.MchSerialNo);
            Assert.Equal(23, 23);
        }

        // [Fact]
        // public void Test2()
        // {
            
        // }
    }
}
