using System.Threading.Tasks;
using Customer.Framework.Domain;
using Customer.Framework.Domain.Models;

namespace Customer.Framework.Services.Interface
{
 
    public interface IOTPService
    {
        Task<JsonResponseResult> GetOTP(OTPModel obj);
        string GenerateRndNumber(int cnt);
        Task<JsonResponseResult> validateOTP(VerifyOTPModel obj);


    }
}
