using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Data.Abstractions
{
    public interface IEpisodeRepository
    {
        Task BatchInsertAsync(IList<Episode> episodes, CancellationToken cancellationToken);
        Task DeleteAll(CancellationToken cancellationToken);
        Task<IList<Episode>> GetAllAsync(CancellationToken cancellationToken);
    }
}
