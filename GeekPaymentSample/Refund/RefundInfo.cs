namespace GeekPaymentSample.Refund
{
    public class RefundInfo
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        /// <value></value>
        public string MchSerialNo {get; set;}
        
        /// <summary>
        /// 平台退款单号
        /// </summary>
        /// <value></value>
        public string RefundId {get; set;}
        
        /// <summary>
        /// 商户退款单号
        /// </summary>
        /// <value></value>
        public string MchRefundId {get; set;}
        
        /// <summary>
        /// 退款金额
        /// </summary>
        /// <value></value>
        public int Amount {get; set;}
        
        /// <summary>
        /// 退款时间
        /// </summary>
        /// <value></value>
        public string RefundTime {get; set;}
        
        /// <summary>
        /// 退款状态
        /// </summary>
        /// <value></value>
        public string RefundStatus {get; set;}
    }
}