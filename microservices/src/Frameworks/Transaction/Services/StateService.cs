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
    public class StateService: IStateService
    {
        private readonly IHttpClientWrapperRespository<State> _IhttpClientWrapperRepository;
        private readonly IRepositoryBase<State> _asyncRepositoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public StateService(IHttpClientWrapperRespository<State> IhttpClientWrapperRepository, IRepositoryBase<State> asyncRepositoryRepository, IMapper mapper, ILogger<StateService> logger)
        {
            _IhttpClientWrapperRepository = IhttpClientWrapperRepository ?? throw new ArgumentNullException(nameof(IhttpClientWrapperRepository));
            _asyncRepositoryRepository = asyncRepositoryRepository ?? throw new ArgumentNullException(nameof(asyncRepositoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<State>> State()
        {
            var resultcheck = _asyncRepositoryRepository.GetAllAsync().Result;

            return await Task.FromResult(resultcheck);
        }
    }
}
