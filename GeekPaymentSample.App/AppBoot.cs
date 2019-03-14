using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GeekPaymentSample.Payment;
using GeekPaymentSample.Geek.Payment;
using GeekPaymentSample.Geek;

namespace GeekPaymentSample.App
{
    public class AppBoot 
    {
        public static void Main(string[] args)
        {
            GeekSign geekSign = new GeekSign(AppProperties.GeekPublicKey, AppProperties.PrivateKey);

            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<ChannelOrderQuery>(new GeekOrderQuery(new GeekUriComponents(GeekPaymentProperties.Scheme, GeekPaymentProperties.Host, geekSign),
                        new GeekEndPoint(new HttpClient(), geekSign),new OrderInfoResponseParser(),AppProperties.AppID))
                .AddSingleton(typeof(RetailOrderService))
                .BuildServiceProvider();

            serviceProvider.GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            ILogger<AppBoot> logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<AppBoot>();
            
            logger.LogDebug("Start application");

            RetailOrderService retailOrderService = serviceProvider.GetService<RetailOrderService>();
            OrderInfo orderInfo = retailOrderService.Find("ORDER636881837081923670");

            logger.LogDebug("end application");
            
        }
    }
}
