using Microsoft.EntityFrameworkCore;
using RickAndMorty.Core.Data.Abstractions;
using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly RickAndMortyContext _context;

        public LocationRepository(RickAndMortyContext context)
        {
            _context = context;
        }

        public async Task BatchInsertAsync(IList<Location> locations, CancellationToken cancellationToken)
        {
            await _context.AddRangeAsync(locations, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAll(CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [Locations]", cancellationToken);
        }

        public async Task<IList<Location>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Locations.ToListAsync(cancellationToken);
        }

        public async Task<Location?> GetSingleAsync(Expression<Func<Location, bool>> filter, CancellationToken cancellationToken)
        {
            return await _context.Locations
                .FirstOrDefaultAsync(filter, cancellationToken);
        }
    }
}
