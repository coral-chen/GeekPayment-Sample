namespace GeekPaymentSample.Payment
{
    public class ToPayOrderInfo
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        /// <value></value>
        public string MchSerialNo {set; get;}
        
        /// <summary>
        /// 平台订单号
        /// </summary>
        /// <value></value>
        public string OrderId {set; get;}

        /// <summary>
        /// 商户订单号
        /// </summary>
        /// <value></value>
        public string MchOrderId {set; get;}

        /// <summary>
        /// 订单的支付URL
        /// </summary>
        /// <value></value>
        public string PayUrl {set; get;}

        /// <summary>
        /// 下单结果
        /// </summary>
        /// <value></value>
        public string ResultCode {set; get;}

        /// <summary>
        /// 支付网关
        /// </summary>
        /// <value></value>
        public string GateWay {set; get;}
    }
}