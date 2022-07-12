using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using static TestGetway.Model;

namespace TestGetway
{
    public class UnitTest1
    {
        
        static readonly string baseUrl = "http://localhost:5021";
        static HttpClient client = new HttpClient();

        [Fact]
        public async Task TestMethod1()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var states = await StateAsync();
            Assert.NotNull(states);
        }
    
        static async Task<State> StateAsync()
        {
            var response = await client.GetAsync($"/account/State");
            var Result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<State>(Result);
        }
       
        static async Task<JsonResponseResult> OnboardCustomerAsync(CustomerModel obj)
        {
            var response = await client.PostAsJsonAsync($"/Customer/OnboardCustomer", obj);
            var Result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JsonResponseResult>(Result);

        }
   
        static async Task<LocalGovernment> OnboardCustomerAsync(long stateid)
        {
            var response = await client.GetAsync($"/account/LocalGovt" + "/" + stateid);
            var Result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LocalGovernment>(Result);

        }

        static async Task<Customer> GetAllCustomer()
        {
            var response = await client.GetAsync($"/account/GetAllCustomer");
            var Result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Customer>(Result);
        }
    }
}
