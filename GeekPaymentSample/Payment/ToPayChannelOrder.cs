namespace GeekPaymentSample.Payment
{
    public interface ToPayChannelOrder
    {
         ToPayOrderInfo Create(OrderCreateInfo orderCreateInfo, string deviceId);
    }
}