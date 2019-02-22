using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace GeekPaymentSample.Geek.Utils
{
    public class ApplicationLogging
    {
        private static ILoggerFactory loggerFactory;

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (loggerFactory == null)
                {
                    loggerFactory = new LoggerFactory();
                    loggerFactory.AddProvider(new ConsoleLoggerProvider((text, logLevel) => logLevel >= LogLevel.Debug, true));
                }

                return loggerFactory;
            }
        }
    }
}