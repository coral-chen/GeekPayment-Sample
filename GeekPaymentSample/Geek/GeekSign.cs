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

        public GeekSign(string publicKey, string privateKey)
        {
            this.signAlgorithm = new RSA2SignAlgorithm(ClearComment(privateKey));
            this.checkAlgorithm = new RSA2CheckSignAlgorithm(ClearComment(publicKey));
        }

        public string Sign(JObject data, string nonceStr, string url)
        {
            data.Add(new JProperty("sign_type", "RSA2"));
            data.Add(new JProperty("url", url));
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