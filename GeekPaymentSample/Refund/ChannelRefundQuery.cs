namespace GeekPaymentSample.Refund
{
    public interface ChannelRefundQuery
    {
        RefundInfo Find(string mchOrderId, string mchRefundId);
    }
}