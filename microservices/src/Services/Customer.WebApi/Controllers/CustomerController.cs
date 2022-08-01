namespace Customer.WebApi.Controllers
{
    using Customer.Framework.Domain.Models;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Customer.Framework.Domain;
    using Customer.Framework.Services.Interface;
    using System;

    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOTPService _otpService;
        private readonly ILocalGovtService _localgovtService;
        private readonly IStateService _stateService;



        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IOTPService otpService, ILocalGovtService localgovtService, IStateService stateService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _otpService = otpService ?? throw new ArgumentNullException(nameof(otpService));
            _localgovtService = localgovtService ?? throw new ArgumentNullException(nameof(localgovtService));
            _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }




        [HttpPost("OnboardCustomer")]
        public async Task<IActionResult> OnboardCustomer([FromBody] CustomerModel obj)
        {
            var transactionResult = await _customerService.OnboardCustomers(obj);
            return Ok(transactionResult);
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var transactionResult = await _customerService.GetAllCustomers();
    
            return Ok(transactionResult);
        }


        [HttpPost("GetOTP")]
        public async Task<IActionResult> GetOTP([FromBody] OTPModel obj)
        {
            var transactionResult = await _otpService.GetOTP(obj);
            return Ok(transactionResult);
        }

        [HttpPost("ValidateOtp")]
        public async Task<IActionResult> ValidateOtp([FromBody] VerifyOTPModel obj)
        {
            var transactionResult = await _otpService.validateOTP(obj);
            return Ok(transactionResult);
        }

        [HttpGet("LocalGovt")]
        public async Task<IActionResult> LocalGovt(long stateid)
        {
            var transactionResult = await _localgovtService.LocalGovt(stateid);
            return Ok(transactionResult);
        }
      
        [HttpGet("State")]
        public async Task<IActionResult> State()
        {
            var transactionResult = await _stateService.State();
            return Ok(transactionResult);
        }


        [HttpGet("Getbanks")]
        public async Task<IActionResult> Getbanks()
        {
            var transactionResult = await _customerService.Getbanks();
            return Ok(transactionResult);
        }


    }
}
