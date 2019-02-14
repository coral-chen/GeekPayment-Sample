namespace GeekPaymentSample.Payment
{
    public class OrderCreateInfo
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        /// <value></value>
        public string MchOrderId {set; get;}

        /// <summary>
        /// 订单标题
        /// </summary>
        /// <value></value>
        public string Title {set; get;}

        /// <summary>
        /// 订单金额
        /// </summary>
        /// <value></value>
        public int Price {set; get;}

        /// <summary>
        /// 订单有效时长
        /// </summary>
        /// <value></value>
        public string Expire {set; get;}
    }
}