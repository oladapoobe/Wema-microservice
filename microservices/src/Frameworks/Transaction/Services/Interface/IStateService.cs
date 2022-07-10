
using Customer.Framework.Data.Entities;
using Customer.Framework.Domain.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Customer.Framework.Services.Interface
{
    public interface IStateService
    {
        Task<IReadOnlyList<State>> State();
    }
}