
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
using System.Collections.Generic;

namespace Customer.Framework.Services.Interface
{
    public class LocalGovtService: ILocalGovtService
    {

        private readonly IHttpClientWrapperRespository<LocalGovernment> _IhttpClientWrapperRepository;
        private readonly IRepositoryBase<LocalGovernment> _asyncRepositoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LocalGovtService(IHttpClientWrapperRespository<LocalGovernment> IhttpClientWrapperRepository, IRepositoryBase<LocalGovernment> asyncRepositoryRepository, IMapper mapper, ILogger<LocalGovtService> logger)
        {
            _IhttpClientWrapperRepository = IhttpClientWrapperRepository ?? throw new ArgumentNullException(nameof(IhttpClientWrapperRepository));
            _asyncRepositoryRepository = asyncRepositoryRepository ?? throw new ArgumentNullException(nameof(asyncRepositoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<LocalGovernment>> LocalGovt(long stateid)
        {
            var resultcheck = _asyncRepositoryRepository.GetAsync(x => x.State_id == stateid).Result;

            return await Task.FromResult(resultcheck);
        }

    }
}
