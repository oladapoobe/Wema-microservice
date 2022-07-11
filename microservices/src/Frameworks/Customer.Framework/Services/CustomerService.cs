namespace Customer.Framework.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;
    using Customer.Framework.Data.Entities;
    using Customer.Framework.Data.Interface;
    using Customer.Framework.Domain;
    using Customer.Framework.Services.Interface;
    using Customer.Framework.Extensions;
    using Newtonsoft.Json;
    using Customer.Framework.Domain.Models;

    public class CustomerService : ICustomerService
    {
        
        private readonly IHttpClientWrapperRespository<Customer> _IhttpClientWrapperRepository;
        private readonly IRepositoryBase<Customer> _asyncRepositoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CustomerService(IHttpClientWrapperRespository<Customer> IhttpClientWrapperRepository, IRepositoryBase<Customer> asyncRepositoryRepository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _IhttpClientWrapperRepository = IhttpClientWrapperRepository ?? throw new ArgumentNullException(nameof(IhttpClientWrapperRepository));
            _asyncRepositoryRepository = asyncRepositoryRepository ?? throw new ArgumentNullException(nameof(asyncRepositoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

   

        public async Task<JsonResponseResult> OnboardCustomers(CustomerModel obj)
        {

            var accountTransactionEntity = _mapper.Map<Customer>(obj);
            var res = new JsonResponseResult { IsSuccessful = false, Message = "OTP not sent" };

            var resultcheck = _asyncRepositoryRepository.AddAsync(accountTransactionEntity).Result;
            if (resultcheck != null)
            {
                _ = _asyncRepositoryRepository.SaveAsync();
                res = new JsonResponseResult { IsSuccessful = true, Message = "customer onboard successfull" };

            }

            return await Task.FromResult(res);
        }



        //public async Task<JsonResponseResult> Getbanks()
        //{
        //    var accountSummary = await GetAccountSummary(accountNumber);
        //    await accountSummary.Validate(accountNumber);
        //    return _mapper.Map<TransactionResult>(accountSummary);
        //}



    }
}
