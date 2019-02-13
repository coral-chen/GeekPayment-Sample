using GeekPaymentSample.Payment;

namespace GeekPaymentSample.Geek.Payment
{
    public class GeekChannelOrderFactory
    {
        /// <summary>
        /// 示例化付款码订单
        /// </summary>
        /// <param name="orderCreateInfo">下单信息</param>
        /// <param name="authCode">付款码</param>
        /// <returns></returns>
        public ChannelOrder channelOrder(OrderCreateInfo orderCreateInfo, string authCode)
        {
            
            return null;
        }
        
        // OrderInfo Create(OrderCreateInfo orderCreateInfo, string notifyUrl, string deviceId, string authCode);

        // OrderPayInfo Create(OrderCreateInfo orderCreateInfo, string notifyUrl, string deviceId);
    }
}