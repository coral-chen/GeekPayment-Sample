namespace GeekPaymentSample.Payment
{
    public class OrderInfo
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
        /// 下单金额
        /// </summary>
        /// <value></value>
        public int Price {set; get;}
        
        /// <summary>
        /// 订单总金额
        /// </summary>
        /// <value></value>
        public int TotalAmount {set; get;}

        /// <summary>
        /// 商户优惠金额
        /// </summary>
        /// <value></value>
        public int MchDiscount {set; get;}

        /// <summary>
        /// 平台优惠金额
        /// </summary>
        /// <value></value>
        public int PlatformDiscount {set; get;}

        /// <summary>
        /// 支付金额
        /// </summary>
        /// <value></value>
        public int PayAmount {set; get;}

        /// <summary>
        /// 待清算金额
        /// </summary>
        /// <value></value>
        public int ToSettleAmount {set; get;}
        
        /// <summary>
        /// 支付渠道
        /// </summary>
        /// <value></value>
        public string Channel {set; get;}

        /// <summary>
        /// 订单创建时间
        /// </summary>
        /// <value></value>
        public string CreateTime {set; get;}

        /// <summary>
        /// 订单支付时间
        /// </summary>
        /// <value></value>
        public string PayTime {set; get;}
        
        /// <summary>
        /// 订单状态
        /// </summary>
        /// <value></value>
        public string OrderStatus {set; get;}

    }
}