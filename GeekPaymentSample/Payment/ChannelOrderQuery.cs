namespace GeekPaymentSample.Payment
{
    public interface ChannelOrderQuery
    {
         OrderInfo Find(string mchOrderId);
    }
}