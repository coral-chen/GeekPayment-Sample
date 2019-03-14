using System;
using System.Reflection;
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
        private GeekSign geekSign;

        private PathComponents pathComponent;
        private Dictionary<string, string> queryParams;

        public GeekUriComponents(string scheme, string host, GeekSign geekSign)
        {
            this.scheme = scheme;
            this.host = host;
            this.geekSign = geekSign;
        }

        public GeekUriComponents QROrderUri()
        {
            return new GeekUriComponents(this.scheme, this.host, GeekPath.QR_ORDER_PATH.GetPath(), this.geekSign);
        }

        public GeekUriComponents NativeQROrderUri()
        {
            return new GeekUriComponents(this.scheme, this.host, GeekPath.NATIVE_QR_ORDER_PATH.GetPath(), this.geekSign);
        }

        public GeekUriComponents RetailQROrderUri()
        {
            return new GeekUriComponents(this.scheme, this.host, GeekPath.RETAIL_QR_ORDER_PATH.GetPath(), this.geekSign);
        }

        public GeekUriComponents RetailOrderUri()
        {
            return new GeekUriComponents(this.scheme, this.host, GeekPath.RETAIL_ORDER_PATH.GetPath(), this.geekSign);
        }

        public GeekUriComponents OrderCloseUri()
        {
            return new GeekUriComponents(this.scheme, this.host, GeekPath.ORDER_CLOSE_PATH.GetPath(), this.geekSign);
        }

        public GeekUriComponents OrderQueryUri() 
        {
            return new GeekUriComponents(this.scheme, this.host, GeekPath.ORDER_QUERY_PATH.GetPath(), this.geekSign);
        }

        public GeekUriComponents RefundQueryUri() 
        {
            return new GeekUriComponents(this.scheme, this.host, GeekPath.REFUND_QUERY_PATH.GetPath(), this.geekSign);
        }

        public GeekUriComponents RefundUri() 
        {
            return new GeekUriComponents(this.scheme, this.host, GeekPath.REFUND_PATH.GetPath(), this.geekSign);
        }

        private GeekUriComponents(string scheme, string host, PathComponents pathComponent, GeekSign geekSign) 
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

        internal enum GeekPath 
        {
            [Path("/apps/{0}/retail_orders/{1}")]
            RETAIL_ORDER_PATH,
            [Path("/apps/{0}/retail_qr_orders/{1}")]
            RETAIL_QR_ORDER_PATH,
            [Path("apps/{0}/qr_orders/{1}")]
            QR_ORDER_PATH,
            [Path("/apps/{0}/native_qr_orders/{1}")]
            NATIVE_QR_ORDER_PATH,

            [Path("/apps/{0}/orders/{1}")]
            ORDER_QUERY_PATH,
            [Path("/apps/{0}/orders/{1}")]
            ORDER_CLOSE_PATH,
            [Path("/apps/{0}/refunds")]
            REFUND_PATH,
            [Path("/apps/{0}/orders/{1}/refunds/{2}")]
            REFUND_QUERY_PATH
        }
    }
}