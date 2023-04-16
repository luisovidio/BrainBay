using RickAndMorty.Core.Domain.ExternalEntities;
using RickAndMorty.Core.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Integration.Abstraction
{
    public interface IRickAndMortyHttpClient
    {
        Task<ResponseModel<T>> GetPageAsync<T>(string controller, int page = 1, CancellationToken cancellationToken = default)
            where T : IExternalClass;
    }
}
