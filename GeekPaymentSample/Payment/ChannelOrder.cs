namespace GeekPaymentSample.Payment
{
    public interface ChannelOrder
    {
         OrderInfo Create(OrderCreateInfo orderCreateInfo, string authCode, string deviceId);
    }
}