using Newtonsoft.Json.Linq;
using GeekPaymentSample.Geek;
using GeekPaymentSample.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    public class OrderInfoResponseParser : HttpResponseParser<OrderInfo>
    {
        public OrderInfo Parse(JObject responseData)
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.MchSerialNo = responseData["mch_serial_no"].ToString();
            orderInfo.OrderId = responseData["order_id"].ToString();
            orderInfo.MchOrderId = responseData["mch_order_id"].ToString();
            orderInfo.Price = int.Parse(responseData["price"].ToString());
            orderInfo.TotalAmount = int.Parse(responseData["total_amount"].ToString());
            orderInfo.MchDiscount = int.Parse(responseData["mch_discount"].ToString());
            orderInfo.PlatformDiscount = int.Parse(responseData["plateform_discount"].ToString());
            orderInfo.PayAmount = int.Parse(responseData["pay_amount"].ToString());
            orderInfo.ToSettleAmount = int.Parse(responseData["to_settle_amount"].ToString());
            orderInfo.Channel = responseData["channel"].ToString();
            orderInfo.CreateTime = responseData["create_time"].ToString();
            orderInfo.PayTime = responseData["pay_time"].ToString();
            orderInfo.OrderStatus = responseData["order_status"].ToString();

            return orderInfo;
        }
    }
}