using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using GeekPaymentSample.Geek.Utils;

namespace GeekPaymentSample.Geek
{
    public class GeekUriComponents
    {
        private string scheme;
        private string host;
        private PathComponents pathComponent;
        private GeekSign geekSign;

        private Dictionary<string, string> queryParams;

        public GeekUriComponents(string scheme, string host, PathComponents pathComponent, GeekSign geekSign) 
        {
            this.scheme = scheme;
            this.host = host;
            this.pathComponent = pathComponent;
            this.geekSign = geekSign;
        }

        private GeekUriComponents InstanceOf(PathComponents pathComponent, Dictionary<string, string> queryParams)
        {
            GeekUriComponents uriComponents = new GeekUriComponents(this.scheme, this.host, pathComponent, this.geekSign);
            uriComponents.queryParams = queryParams;

            return uriComponents;
        }

        public GeekUriComponents Expand(params string[] uriVariableValues)
        {
            PathComponents pathTo = pathComponent.Expand(uriVariableValues);
            return InstanceOf(pathTo, queryParams);
        }

        public GeekUriComponents Sign(JObject data)
        {
            string nonceStr = RandomStringUtils.Random(10);

            string sign = geekSign.Sign(data, nonceStr, GetFullPath());

            this.queryParams = new Dictionary<string, string>();
            queryParams.Add("sign_type", geekSign.SignType);
            queryParams.Add("nonce_str", nonceStr);
            queryParams.Add("sign", sign);

            return this;
        }

        private string GetFullPath()
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

            return uriBuilder.ToString();
        }

        public string ToUriString()
        {
            StringBuilder uriBuilder = new StringBuilder(GetFullPath());

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
            if(this.queryParams == null)
            {
                return null;
            }

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