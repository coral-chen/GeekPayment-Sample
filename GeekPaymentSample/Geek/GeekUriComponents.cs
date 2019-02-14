using System.Text;
using System.Collections.Generic;

namespace GeekPaymentSample.Geek
{
    public class GeekUriComponents
    {
        private string scheme;
        private string host;
        private PathComponents pathComponent;

        private Dictionary<string, string> queryParams;

        public GeekUriComponents(string scheme, string host, PathComponents pathComponent) 
        {
            this.scheme = scheme;
            this.host = host;
            this.pathComponent = pathComponent;
        }

        private GeekUriComponents(string scheme, string host, PathComponents pathComponent, Dictionary<string, string> queryParams)
        {
            this.scheme = scheme;
            this.host = host;
            this.pathComponent = pathComponent;
            this.queryParams = queryParams;
        }

        public GeekUriComponents Expand(params string[] uriVariableValues)
        {
            PathComponents pathTo = pathComponent.Expand(uriVariableValues);
            return new GeekUriComponents(scheme, host, pathTo, queryParams);
        }

        public GeekUriComponents QueryParams(string nonceStr, string sign, string signType)
        {
            this.queryParams = new Dictionary<string, string>();
            queryParams.Add("sign_type", signType);
            queryParams.Add("nonce_str", nonceStr);
            queryParams.Add("sign", sign);

            return this;
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

            string path = pathComponent.GetPath();
            if (path != null)
            {
                if (!path.StartsWith("/")) 
                {
                    uriBuilder.Append("/");
                }
                uriBuilder.Append(path);
            }

            string query = getQuery();
            if (query != null)
            {
                uriBuilder.Append("?");
                uriBuilder.Append(query);
            }

            return uriBuilder.ToString();
        }

        private string getQuery()
        {
            StringBuilder queryBuilder = new StringBuilder();
            foreach (var item in queryParams)
            {
                if (queryBuilder.Length > 0)
                {
                    queryBuilder.Append("&");
                }
                queryBuilder.Append(item.Key);
                queryBuilder.Append("=");
                queryBuilder.Append(item.Value);
            }
            
            return queryBuilder.ToString();
        }
    }
}