using Newtonsoft.Json.Linq;
using GeekPaymentSample.Geek;
using GeekPaymentSample.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    public class ToPayNativeOrderInfoResponseParser : HttpResponseParser<ToPayNativeOrderInfo>
    {
        public ToPayNativeOrderInfo Parse(JObject contentData)
        {
            ToPayNativeOrderInfo orderInfo = new ToPayNativeOrderInfo();
            orderInfo.MchSerialNo = contentData["mch_serial_no"].ToString();
            orderInfo.OrderId = contentData["order_id"].ToString();
            orderInfo.MchOrderId = contentData["mch_order_id"].ToString();
            orderInfo.PayUrl = contentData["pay_url"].ToString();
            orderInfo.ResultCode = contentData["result_code"].ToString();
            orderInfo.QRCode = contentData["qr_code"].ToString();
            orderInfo.CodeImgUrl = contentData["code_img_url"].ToString();

            return orderInfo;
        }
    }
}