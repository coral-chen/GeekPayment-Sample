using Xunit;
using GeekPaymentSample;
using GeekPaymentSample.Refund;
using Newtonsoft.Json;
using System;

namespace GeekPaymentSample.Tests
{
    public class RefundCreatingTest
    {

        // private ChannelRefundCreatingService refundService;

        // private String mchOrderId;

        // public RefundCreatingTest()
        // {

        //     RefundCreatingService refundCreatingService = new RefundCreatingService();
        //     refundService = new RefundService(refundCreatingService);
        // }

        // [Fact]
        // public void TestRefund() {
        //     string mchRefundId = String.Format("REFUND{0}", DateTime.Now.Ticks);
        //     int amount = 10000;

        //     RefundInfo refundInfo = refundService.CreateRefund(mchRefundId, mchOrderId, amount);
            
        //     Assert.NotEmpty(refundInfo.MchSerialNo);
        //     Assert.NotEmpty(refundInfo.RefundId);
        //     Assert.Equal(refundInfo.MchRefundId, mchRefundId);
        //     Assert.Equal(refundInfo.Amount, amount); 
        //     Assert.NotEmpty(refundInfo.RefundTime);
        //     Assert.NotEmpty(refundInfo.RefundStatus);

        //     string refundInfoJsonStr = JsonConvert.SerializeObject(refundInfo);
        //     Console.WriteLine(refundInfo);
        // }
    }
}