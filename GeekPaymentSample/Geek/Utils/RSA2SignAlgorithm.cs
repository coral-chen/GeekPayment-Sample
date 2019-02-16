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
            rsaUtil = new RsaPkcs8Util(Encoding.UTF8, null, privateKey);
        }

        public string Sign(string data)
        {
            return rsaUtil.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}