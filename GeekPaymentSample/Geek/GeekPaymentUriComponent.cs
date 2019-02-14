using System.Text;

namespace GeekPaymentSample.Geek
{
    public class GeekPaymentUriComponent
    {
        private string scheme;
        private string host;

        private string path;

        private string query;

        public GeekPaymentUriComponent(string scheme, string host, string path) 
        {
            this.scheme = scheme;
            this.host = host;
            this.path = path;
        }

        public string ToUriString()
        {
            StringBuilder uriBuilder = new StringBuilder();

            if (scheme != null)
            {
                uriBuilder.Append(scheme).Append(":");
            }

            if (host != null)
            {
                uriBuilder.Append("//");
                uriBuilder.Append(host);
            }

            if (path != null)
            {
                if (!path.StartsWith("/")) 
                {
                    uriBuilder.Append("/");
                }
                uriBuilder.Append(path);
            }

            if (query != null)
            {
                uriBuilder.Append("?");
                uriBuilder.Append(query);
            }

            return uriBuilder.ToString();
        }
    }
}