using MediatR;
using RickAndMorty.Core.Services.Abstraction;
using RickAndMorty.Core.Services.Constants;
using RickAndMorty.Core.Services.Helpers;
using RickAndMorty.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Queries
{
    public class GetCharactersQuery 
        : IRequest<MediatorResponse<IList<CharacterModel>>>
        , ICacheableQuery
    {
        public long? OriginId { get; }

        public string CacheKey { get; }

        public TimeSpan SlidingExpiration { get; }

        public GetCharactersQuery(long? originId = null)
        {
            OriginId = originId;

            CacheKey = originId.HasValue
                ? CacheKeyHelper.CharacterCacheKey(originId.Value)
                : CacheKeyHelper.CharacterCacheKey();

            SlidingExpiration = TimeSpan.FromMinutes(5);
        }
    }
}
