namespace Customer.Framework.Services.Interface
{
    using System.Threading.Tasks;
    using Customer.Framework.Domain;
    using Customer.Framework.Domain.Models;

    public interface ICustomerService
    {
        Task<JsonResponseResult> OnboardCustomers(CustomerModel obj);
    }
}
