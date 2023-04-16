using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Data.Abstractions
{
    public interface ICharacterRepository
    {
        Task BatchInsertAsync(IList<Character> characters, CancellationToken cancellationToken);
        Task DeleteAll(CancellationToken cancellationToken);
        Task<IList<Character>> GetListAsync(Expression<Func<Character,bool>>? filter, CancellationToken cancellationToken = default);
        Task<Character> AddAsync(Character character, CancellationToken cancellationToken);
    }
}
