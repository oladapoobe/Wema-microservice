namespace Customer.Framework.Data.Interface
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Customer.Framework.Data.Entities;

    public interface IHttpClientWrapperRespository<T> where T : class
    {
        T GetAsyncItem(string BaseUrl, string EndpointUrl);
        List<T> GetAsync(string BaseUrl, string EndpointUrl);
        T PostAsyncAccount(string BaseUrl, string Endpointurl, object body);
        T PostAsync(string BaseUrl, string Endpointurl, object body);
        Task<T> SendSms(HttpRequestMessage request);

    }
}
