
using Customer.Framework.Domain.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Customer.Framework.Services.Interface
{
    public interface ISmsService
    {
        Task<HttpRequestMessage> SendSMS(SMS obj);
    }
}
