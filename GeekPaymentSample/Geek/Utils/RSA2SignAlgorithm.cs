using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using XC.RSAUtil;

namespace GeekPaymentSample.Geek.Utils
{
    public class RSA2SignAlgorithm
    {
        private RsaPkcs8Util rsaUtil;
        public RSA2SignAlgorithm(string privateKey)
        {
            rsaUtil = new RsaPkcs8Util(Encoding.UTF8, null, ClearComment(privateKey));
        }

        public string Sign(string data)
        {
            return rsaUtil.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        private string ClearComment(String privateKey)
        {
            byte[] privateKeyBytes = Encoding.Default.GetBytes(privateKey);
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