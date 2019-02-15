using Newtonsoft.Json.Linq;
using GeekPaymentSample.Geek;
using GeekPaymentSample.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    public class ToPayOrderInfoResponseParser : HttpResponseParser<ToPayOrderInfo>
    {
        public ToPayOrderInfo Parse(JObject contentData)
        {
            ToPayOrderInfo orderInfo = new ToPayOrderInfo();
            orderInfo.MchSerialNo = contentData["mch_serial_no"].ToString();
            orderInfo.OrderId = contentData["order_id"].ToString();
            orderInfo.MchOrderId = contentData["mch_order_id"].ToString();
            orderInfo.PayUrl = contentData["pay_url"].ToString();
            orderInfo.ResultCode = contentData["result_code"].ToString();
            orderInfo.GateWay = contentData["gateway"].ToString();

            return orderInfo;
        }
    }
}