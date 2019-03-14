using GeekPaymentSample.Payment;

namespace GeekPaymentSample.App
{
    public class RetailOrderService
    {
        private ChannelOrderQuery query;

        public RetailOrderService(ChannelOrderQuery query) {
            this.query = query;
        }

        public OrderInfo Find(string mchOrderId) {
            return query.Find(mchOrderId);
        }
    }
}