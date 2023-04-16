using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Data.Abstractions
{
    public interface ILocationRepository
    {
        Task BatchInsertAsync(IList<Location> locations, CancellationToken cancellationToken);
        Task DeleteAll(CancellationToken cancellationToken);
        Task<IList<Location>> GetAllAsync(CancellationToken cancellationToken);
        Task<Location?> GetSingleAsync(Expression<Func<Location, bool>> filter, CancellationToken cancellationToken);
    }
}
