using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Text;
using GeekPaymentSample.Geek.Utils;

namespace GeekPaymentSample.Geek
{
    public class GeekEndPoint
    {
        private HttpClient httpClient;
        private GeekSign geekSign;

        public GeekEndPoint(HttpClient httpClient, GeekSign geekSign)
        {
            this.httpClient = httpClient;
            this.geekSign = geekSign;
        }

        public JObject Get(string url)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            return Send(requestMessage);
        }

        public JObject Put(string url, JObject requestBody)
        {
            String content = JObjectSort.Sort(requestBody).ToString(Formatting.None);;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");

            return Send(requestMessage);
        }

        public JObject Delete(string url)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            return Send(requestMessage);
        }

        private JObject Send(HttpRequestMessage requestMessage)
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.SendAsync(requestMessage).Result;
                if (responseMessage.IsSuccessStatusCode) {
                    string contentString = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject contentJson = JObject.Parse(contentString);
                    
                    JObject contentData = (JObject)contentJson["data"];
                    string contentSign = contentJson["sign"].ToString();

                    string fullPath = requestMessage.RequestUri.OriginalString.Substring(0, requestMessage.RequestUri.OriginalString.IndexOf("?"));
                    
                    if (contentData["return_code"].ToString().Contains("SUCCESS") && geekSign.CheckSign(new JObject(contentData), contentSign, fullPath))
                    {
                        return contentData;
                    }   
                }
                throw new ApplicationException();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("GeekPayment API {0} Exception", requestMessage.RequestUri.ToString());
                Console.WriteLine("Message: {0}", e.Message);
                throw new ApplicationException();
            }
        }
    }
}