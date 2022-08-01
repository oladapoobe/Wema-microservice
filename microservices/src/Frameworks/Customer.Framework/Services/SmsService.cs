using Customer.Framework.Data.Interface;
using Customer.Framework.Domain.Models;
using Customer.Framework.Services.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Customer.Framework.Services.Interface
{
    public class SmsService : ISmsService
    {

        public ILogger<SmsService> _logger { get; }
        private readonly IHttpClientWrapperRespository<HttpRequestMessage> _IhttpClientWrapperRepository;
        public Settings _emailSettings { get; }

        public SmsService(IOptions<Settings> mailSettings, ILogger<SmsService> logger, IHttpClientWrapperRespository<HttpRequestMessage> IhttpClientWrapperRepository)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;
            _IhttpClientWrapperRepository = IhttpClientWrapperRepository;

        }

        public async Task<HttpRequestMessage> SendSMS(SMS obj)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_emailSettings.RequestUri),
                Headers =
    {
        { "X-RapidAPI-Key", _emailSettings.XRapidAPIKey  },
        { "X-RapidAPI-Host", _emailSettings.XRapidAPIHost },
    },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "user", _emailSettings.user },
        { "from", _emailSettings.from },
        { "to", _emailSettings.to },
        { "sms", _emailSettings.sms },
        { "method", _emailSettings.method },
        { "class", _emailSettings.classvalue},
        { "password", _emailSettings.password },
    }),
            };

            var res = _IhttpClientWrapperRepository.SendSms(request).Result;
            return await Task.FromResult(res);
            ;
        }


    }
}
