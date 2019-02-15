using Newtonsoft.Json.Linq;
using GeekPaymentSample.Geek;
using GeekPaymentSample.Refund;

namespace GeekPaymentSample.Geek.Refund
{
    public class RefundInfoResponseParser : HttpResponseParser<RefundInfo>
    {
        public RefundInfo Parse(JObject responseData)
        {
            RefundInfo refundInfo = new RefundInfo();
            refundInfo.MchSerialNo = responseData["mch_serial_no"].ToString();
            refundInfo.RefundId = responseData["refund_id"].ToString();
            refundInfo.MchRefundId = responseData["mch_refund_id"].ToString();
            refundInfo.Amount = int.Parse(responseData["amount"].ToString());
            refundInfo.RefundTime = responseData["refund_time"].ToString();
            refundInfo.RefundStatus = responseData["refund_status"].ToString();

            return refundInfo;
        }
    }
}