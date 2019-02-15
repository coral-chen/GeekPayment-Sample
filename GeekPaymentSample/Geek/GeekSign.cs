using GeekPaymentSample.Geek.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using XC.RSAUtil;

namespace GeekPaymentSample.Geek
{
    /// <summary>
    /// SHA256WithRSA签名算法
    /// </summary>
    public class GeekSign
    {
        private RSA2CheckSignAlgorithm checkAlgorithm;
        private RSA2SignAlgorithm signAlgorithm;

        private const string signType = "RSA2";

        public string SignType
        {
            get => signType;
        }

        public GeekSign(string publicKey, string privateKey)
        {
            this.signAlgorithm = new RSA2SignAlgorithm(ClearComment(privateKey));
            this.checkAlgorithm = new RSA2CheckSignAlgorithm(ClearComment(publicKey));
        }

        public string Sign(JObject data, string nonceStr, string url)
        {
            if (data.ContainsKey("sign_type"))
            {
                data.Remove("sign_type");
            }
            data.Add(new JProperty("sign_type", signType));
            
            if (data.ContainsKey("url"))
            {
                data.Remove("url");
            }
            data.Add(new JProperty("url", url));

            if (data.ContainsKey("nonce_str"))
            {
                data.Remove("nonce_str");
            }
            data.Add(new JProperty("nonce_str",  nonceStr));
            
            return signAlgorithm.Sign(data.ToString());
        }

        public bool CheckSign(JObject data, string sign)
        {
            return checkAlgorithm.CheckSign(data.ToString(), sign);
        }

        private string ClearComment(string key)
        {
            byte[] privateKeyBytes = Encoding.Default.GetBytes(key);
            Stream stream = new MemoryStream(privateKeyBytes);
            StreamReader streamReader = new StreamReader(stream);
            StringBuilder resultTemp = new StringBuilder();
            string line;
            while((line = streamReader.ReadLine()) != null)
            {
                if (line.StartsWith("-")) {
                    continue;
                }
                resultTemp.Append(line);
            }
            return resultTemp.ToString();
        }
    }
}