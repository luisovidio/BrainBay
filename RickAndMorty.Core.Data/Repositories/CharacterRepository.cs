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
    public class CharacterRepository : ICharacterRepository
    {
        private readonly RickAndMortyContext _context;

        public CharacterRepository(RickAndMortyContext context)
        {
            _context = context;
        }

        public async Task<Character> AddAsync(Character character, CancellationToken cancellationToken)
        {
            var addedCharacter = await _context.Characters.AddAsync(character, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return addedCharacter.Entity;
        }

        public async Task BatchInsertAsync(IList<Character> characters, CancellationToken cancellationToken)
        {
            await _context.Characters.AddRangeAsync(characters, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAll(CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [Characters]", cancellationToken);
        }

        public async Task<IList<Character>> GetListAsync(Expression<Func<Character, bool>>? filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Characters
                .Include(c => c.Origin)
                .Include(c => c.Location)
                .Include(c => c.Episodes);

            return filter != null
                ? await query.Where(filter).ToListAsync(cancellationToken)
                : await query.ToListAsync(cancellationToken);
        }
    }
}
