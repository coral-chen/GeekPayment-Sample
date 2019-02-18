namespace GeekPaymentSample.Payment
{
    public interface ToPayAggregateChannelOrder
    {
         ToPayOrderInfo Create(OrderCreateInfo orderCreateInfo, bool direct);
    }
}