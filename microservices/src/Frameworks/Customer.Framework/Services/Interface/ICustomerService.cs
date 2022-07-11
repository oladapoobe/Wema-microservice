namespace Customer.Framework.Services.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Customer.Framework.Domain;
    using Customer.Framework.Domain.Models;
    using Customer.Framework.Data.Entities;

    public interface ICustomerService
    {
        Task<JsonResponseResult> OnboardCustomers(CustomerModel obj);
        Task<IReadOnlyList<Customer>> GetAllCustomers();
    }
}
