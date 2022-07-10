using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using Customer.Framework.Data.Interface;
using System.Threading.Tasks;

namespace Customer
{
    public class HttpClientWrapperRespository<T> : IHttpClientWrapperRespository<T> where T : class
    {

        public T GetAsyncItem(string BaseUrl, string EndpointUrl)
        {
            try
            {

                using (var _client = new HttpClient())
                {
                    _client.BaseAddress = new Uri(BaseUrl);
                    var response = _client.GetAsync(EndpointUrl).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    var Item = JsonConvert.DeserializeObject<T>(content);
                    return Item;
                    //}

                }


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public List<T> GetAsync(string BaseUrl, string EndpointUrl)
        {
            using (var _client = new HttpClient())
            {
                _client.BaseAddress = new Uri(BaseUrl);
                var response = _client.GetAsync(EndpointUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var Item = JsonConvert.DeserializeObject<List<T>>(content);
                    return Item;
                }
                throw new Exception(response.ReasonPhrase);
            }
        }
        public T PostAsyncAccount(string BaseUrl, string Endpointurl, object body)
        {

            using (var _client = new HttpClient())
            {
                _client.BaseAddress = new Uri(BaseUrl);
                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = _client.PostAsync(Endpointurl, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Content = response.Content.ReadAsStringAsync().Result;
                    var jsonResult = JsonConvert.DeserializeObject(Content).ToString();
                    var Item = JsonConvert.DeserializeObject<T>(jsonResult);
                    return Item;
                }
                throw new Exception(response.ReasonPhrase);
            }
        }
        public T PostAsync(string BaseUrl, string Endpointurl, object body)
        {

            using (var _client = new HttpClient())
            {
                _client.BaseAddress = new Uri(BaseUrl);
                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = _client.PostAsync(Endpointurl, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Content = response.Content.ReadAsStringAsync().Result;
                    var Item = JsonConvert.DeserializeObject<T>(Content);
                    return Item;
                }
                throw new Exception(response.ReasonPhrase);
            }
        }



        public async Task<T> SendSms(HttpRequestMessage request)
        {
            var client = new HttpClient();
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var Content = response.Content.ReadAsStringAsync().Result;
                var jsonResult = JsonConvert.DeserializeObject(Content).ToString();
                var Item = JsonConvert.DeserializeObject<T>(jsonResult);
                return Item;
            }


        }


    }

}
