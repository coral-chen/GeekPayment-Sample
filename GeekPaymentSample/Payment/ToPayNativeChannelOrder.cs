namespace GeekPaymentSample.Payment
{
    public interface ToPayNativeChannelOrder
    {
         ToPayNativeOrderInfo Create(OrderCreateInfo orderCreateInfo, string channel);
    }
}