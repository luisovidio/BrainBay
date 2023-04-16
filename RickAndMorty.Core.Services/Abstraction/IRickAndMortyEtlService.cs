using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Abstraction
{
    public interface IRickAndMortyEtlService
    {
        /// <summary>
        /// Reset the database
        /// </summary>
        /// <returns></returns>
        Task ResetDatabaseAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Extract, transform and load data from remote API
        /// </summary>
        /// <returns></returns>
        Task EtlDataAsync(CancellationToken cancellationToken = default);
    }
}
