using GeekPaymentSample.Payment;
using System.Net.Http;

namespace GeekPaymentSample.Geek.Payment
{
    public class GeekChannelOrderFactory
    {
        private static readonly HttpClient httpClient;
        private static readonly GeekSign geekSign;

        private static readonly GeekEndPoint geekEndPoint;

        static GeekChannelOrderFactory()
        {
            httpClient = new HttpClient();
            geekSign = new GeekSign(AppProperties.GeekPublicKey, AppProperties.PrivateKey);
            geekEndPoint = new GeekEndPoint(httpClient, geekSign);
        }

        /// <summary>
        /// 实例化付款码订单
        /// </summary>
        /// <returns></returns>
        public ChannelOrder RetailOrder()
        {
            PathComponents pathComponent = new GeekPathComponents("/apps/{0}/retail_orders/{1}");
            HttpResponseParser<OrderInfo> responseParser = new OrderInfoResponseParser();
            
            return new GeekRetailOrder(geekEndPoint, GeekUriComponents(pathComponent, geekSign), responseParser, AppProperties.NotifyUrl, AppProperties.AppID);
        }

        /// <summary>
        /// 实例化聚合订单码订单
        /// </summary>
        /// <returns></returns>
        public ToPayChannelOrder RetailQROrder()
        {
            PathComponents pathComponents = new GeekPathComponents("/apps/{0}/retail_qr_orders/{1}");
            HttpResponseParser<ToPayOrderInfo> responseParser = new ToPayOrderInfoResponseParser();

            return new GeekRetailQROrder(geekEndPoint, GeekUriComponents(pathComponents, geekSign), responseParser, AppProperties.NotifyUrl, AppProperties.AppID);
        }

        /// <summary>
        /// 实例化原生二维码订单
        /// </summary>
        /// <returns></returns>
        public ToPayNativeChannelOrder NativeQROrder()
        {
            PathComponents pathComponents = new GeekPathComponents("/apps/{0}/native_qr_orders/{1}");
            HttpResponseParser<ToPayNativeOrderInfo> responseParser = new ToPayNativeOrderInfoResponseParser();

            return new GeekNativeQROrder(geekEndPoint, GeekUriComponents(pathComponents, geekSign), responseParser, AppProperties.ReturnUrl, AppProperties.NotifyUrl, AppProperties.AppID);
        }


        private GeekUriComponents GeekUriComponents(PathComponents pathComponents, GeekSign geekSign)
        {
            return new GeekUriComponents(GeekPaymentProperties.Scheme, GeekPaymentProperties.Host, pathComponents, geekSign);
        }
    }
}