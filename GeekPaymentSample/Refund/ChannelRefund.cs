namespace GeekPaymentSample.Refund
{
    public interface ChannelRefund
    {
        RefundInfo Create(string mchOrderId, string mchRefundId, int amount);
    }
}