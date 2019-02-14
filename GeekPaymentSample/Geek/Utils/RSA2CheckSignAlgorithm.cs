using XC.RSAUtil;
using System.Security.Cryptography;
using System.Text;

namespace GeekPaymentSample.Geek.Utils
{
    public class RSA2CheckSignAlgorithm
    {
        private RsaPkcs8Util rsaUtil;

        public RSA2CheckSignAlgorithm(string publicKey)
        {
            rsaUtil = new RsaPkcs8Util(Encoding.UTF8, publicKey);
        }

        public bool CheckSign(string data, string sign)
        {
            return rsaUtil.VerifyData(data, sign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
        
    }
}