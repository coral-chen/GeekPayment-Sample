using GeekPaymentSample.Geek.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
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
        private static readonly char[] padding = { '=' };

        public string SignType
        {
            get => signType;
        }

        public GeekSign(string publicKey, string privateKey)
        {
            this.signAlgorithm = new RSA2SignAlgorithm(ClearComment(privateKey));
            this.checkAlgorithm = new RSA2CheckSignAlgorithm(ClearComment(publicKey));
        }

        /// <summary>
        /// 签名  签名字符串base64编码，使用url safe模式
        /// </summary>
        /// <param name="data">待签名数据</param>
        /// <param name="nonceStr">随机数</param>
        /// <param name="url">待签名数据的请求路径</param>
        /// <returns></returns>
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
            
            string toSignStr = JObjectSort.Sort(data).ToString(Formatting.None);
            string signStr = signAlgorithm.Sign(toSignStr);
            return signStr.TrimEnd(padding).Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="data">待验证数据</param>
        /// <param name="sign">签名</param>
        /// <param name="url">待验证数据的请求路径</param>
        /// <returns></returns>
        public bool CheckSign(JObject data, string sign, string url)
        {
            if (data.ContainsKey("url"))
            {
                data.Remove("url");
            }
            data.Add(new JProperty("url", url));

            sign = sign.Replace('_', '/').Replace('-', '+');
            switch(sign.Length % 4) {
                case 2: sign += "=="; break;
                case 3: sign += "="; break;
            }

            return checkAlgorithm.CheckSign(JObjectSort.Sort(data).ToString(Formatting.None), sign);
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