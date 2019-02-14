using GeekPaymentSample.Payment;
using System.Net.Http;

namespace GeekPaymentSample.Geek.Payment
{
    public class GeekChannelOrderFactory
    {
        private static readonly HttpClient httpClient;
        private static readonly GeekSign geekSign;

        static GeekChannelOrderFactory()
        {
            httpClient = new HttpClient();
            geekSign = new GeekSign(AppProperties.GeekPublicKey, AppProperties.PrivateKey);
        }

        /// <summary>
        /// 实例化付款码订单
        /// </summary>
        /// <returns></returns>
        public ChannelOrder channelOrder()
        {
            PathComponents pathComponent = new RetailPathComponents();
            GeekUriComponents uriComponent = new GeekUriComponents(GeekPaymentProperties.Scheme, GeekPaymentProperties.Host, pathComponent);
            
            return new GeekChannelRetailOrder(httpClient, uriComponent, geekSign, AppProperties.NotifyUrl, AppProperties.AppID);
        }
    }
}