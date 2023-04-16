using Microsoft.EntityFrameworkCore;
using RickAndMorty.Core.Data.Abstractions;
using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Data.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private RickAndMortyContext _context;

        public EpisodeRepository(RickAndMortyContext context)
        {
            _context = context;
        }

        public async Task BatchInsertAsync(IList<Episode> episodes, CancellationToken cancellationToken)
        {
            await _context.AddRangeAsync(episodes, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAll(CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [Episodes]", cancellationToken);
        }

        public async Task<IList<Episode>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Episodes.ToListAsync(cancellationToken);
        }
    }
}
