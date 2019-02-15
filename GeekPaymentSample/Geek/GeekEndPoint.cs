using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using System.Text;

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
            return send(requestMessage);
        }

        public JObject Put(string url, JObject requestBody)
        {
            String content = requestBody.ToString();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "applicatiion/json");

            return send(requestMessage);
        }

        private JObject send(HttpRequestMessage requestMessage)
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.SendAsync(requestMessage).Result;
                if (responseMessage.IsSuccessStatusCode) {
                    string contentString = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject contentJson = JObject.Parse(contentString);
                    
                    JObject contentData = (JObject)contentJson["data"];
                    string contentSign = contentJson["sign"].ToString();

                    if (contentData["return_code"].ToString().Contains("SUCCESS") && geekSign.CheckSign(contentData, contentSign))
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