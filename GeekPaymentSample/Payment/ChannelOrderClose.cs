namespace GeekPaymentSample.Payment
{
    public interface ChannelOrderClose
    {
        OrderInfo Close(string mchOrderId);
    }
}