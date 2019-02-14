using System.Text;
using System;

namespace GeekPaymentSample.Geek.Utils
{
    public class RandomStringUtils
    {
        private static string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string Random(int length)
        {
            Random random = new Random();
            
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++) 
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            return result.ToString();
        }
    }
}