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

namespace Customer.Framework.Services.Interface
{
    internal class OTPService : IOTPService
    {

        private readonly IHttpClientWrapperRespository<OtpLog> _IhttpClientWrapperRepository;
        private readonly IRepositoryBase<OtpLog> _asyncRepositoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISmsService _smsService;

        public OTPService(IHttpClientWrapperRespository<OtpLog> IhttpClientWrapperRepository, IRepositoryBase<OtpLog> asyncRepositoryRepository, IMapper mapper, ILogger<OTPService> logger, ISmsService smsService)
        {
            _IhttpClientWrapperRepository = IhttpClientWrapperRepository ?? throw new ArgumentNullException(nameof(IhttpClientWrapperRepository));
            _asyncRepositoryRepository = asyncRepositoryRepository ?? throw new ArgumentNullException(nameof(asyncRepositoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
        }

        public string GenerateRndNumber(int cnt)
        {
            string[] key2 = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random rand1 = new Random();
            string txt = "";
            for (int j = 0; j < cnt; j++)
                txt += key2[rand1.Next(0, 9)];
            return txt;
        }

        public async Task<JsonResponseResult> GetOTP(OTPModel obj)
        {
            string PhoneNumber = obj.PhoneNumber;
            var res = new JsonResponseResult { IsSuccessful = false, Message = "OTP not sent" };

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                res = new JsonResponseResult { IsSuccessful = false, Message = "invalid phone number" };
                return await Task.FromResult(res);
            }

            //check if previous OTP is expired before generating another .....
            var resultcheck = _asyncRepositoryRepository.GetAsync(x => x.DateExpired == null && x.PhoneNumber == obj.PhoneNumber).Result;
            foreach (var a in resultcheck)
            {
                a.DateExpired = DateTime.Now;
                var updateresult = _asyncRepositoryRepository.UpdateAsync(a).Result;
                if (updateresult)
                {
                    await _asyncRepositoryRepository.SaveAsync();
                }
            }

            var Otp = GenerateRndNumber(7);
            var accountTransactionEntity = _mapper.Map<OtpLog>(obj);
            accountTransactionEntity.DateCreated = DateTime.Now;
            accountTransactionEntity.DateExpired = DateTime.Now.AddMinutes(10);
            accountTransactionEntity.Otp = Otp;
            var result = _asyncRepositoryRepository.AddAsync(accountTransactionEntity).Result;


            if (result != null)
            {
                await _asyncRepositoryRepository.SaveAsync();
                string Message = "Dear Customer Please use" + " " + Otp + " " + "to complete this process on the portal. This code will expire in 10 Minutes";
                var Smsobj = new SMS { Body = Message, PhoneNumber = PhoneNumber };
                var sendTextMessage = _smsService.SendSMS(Smsobj).Result;
                if (sendTextMessage != null)
                {
                    _logger.LogInformation($"Message Sent to [{PhoneNumber}]");
                    res = new JsonResponseResult { IsSuccessful = false, Message = "invalid phone number" };

                    return await Task.FromResult(res);

                }
                _logger.LogInformation($"Message not Sent to [{PhoneNumber}]");
            }

            return await Task.FromResult(res);

        }

        public async Task<JsonResponseResult> validateOTP(VerifyOTPModel obj)
        {

            _logger.LogInformation($"The request passed for validateOTP is [{JsonConvert.SerializeObject(obj)}]");

            var result = _asyncRepositoryRepository.Findsync(x => x.Otp == obj.OTP && x.PhoneNumber == obj.PhoneNumber).Result;
            if (result == null)
            {
                _logger.LogInformation($"Wrong OTP");
                return await Task.FromResult(new JsonResponseResult { IsSuccessful = false, Message = "Wrong OTP" });

            }

            if (DateTime.Now > result.DateExpired)
            {
                _logger.LogInformation($"OTP Expired");
                return await Task.FromResult(new JsonResponseResult { IsSuccessful = false, Message = "OTP Expired" });
            }
            else
            {
                result.DateExpired = DateTime.Now;
                var updateresult = _asyncRepositoryRepository.UpdateAsync(result).Result;
                if (updateresult)
                {
                    await _asyncRepositoryRepository.SaveAsync();
                }
               
                return await Task.FromResult(new JsonResponseResult { IsSuccessful = false, Message = "OTP Validated successfully" });

            }



        }

    }
}
