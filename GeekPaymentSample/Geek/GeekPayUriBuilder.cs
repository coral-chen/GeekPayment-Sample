namespace GeekPaymentSample.Geek
{
    public class GeekPaymentUriBuilder
    {
        private string scheme;
        private string host;
        private string context;

        public string Scheme {
            set => scheme = value;
        }

        public string Host {
            set => host = value;
        }

        public string Context {
            set => context = value;
        }
    }
}