using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static TestCustomer.Model;

namespace TestCustomer
{
    [TestClass]
    public class UnitTest1
    {
        static readonly string baseUrl = "http://localhost:5021";
        static HttpClient client = new HttpClient();

        [Test]
        public async Task TestMethod1()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var accessToken = await StateAsync();
            Assert.IsNotNull(accessToken);
        }
        [TestMethod]
        static async Task<State> StateAsync()
        {
            var response = await client.GetAsync($"/account/State");
            var Result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<State>(Result);
        }
        [TestMethod]
        static async Task<JsonResponseResult> OnboardCustomerAsync(CustomerModel obj)
        {
            var response = await client.PostAsJsonAsync($"/Customer/OnboardCustomer", obj);
            var Result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JsonResponseResult>(Result);
           
        }
        [TestMethod]
        static async Task<LocalGovernment> OnboardCustomerAsync(long stateid)
        {
            var response = await client.GetAsync($"/account/LocalGovt"+ "/" +stateid);
            var Result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LocalGovernment>(Result);

        }

    }
}
