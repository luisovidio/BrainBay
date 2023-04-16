using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RickAndMorty.Core.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.PipelineBehaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull, IMediatorResponse
    {
        private readonly IMemoryCache _memoryCache;
        public CachingBehavior(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICacheableQuery cacheableQuery)
            {
                var result = _memoryCache.Get<TResponse>(cacheableQuery.CacheKey);

                if (result != null)
                {
                    result.FromDatabase = false;
                    return result;
                }

                var response = await next();

                if (response.Succeeded)
                {
                    _memoryCache.Set(cacheableQuery.CacheKey, response, cacheableQuery.SlidingExpiration);
                }

                return response;
            }

            if (request is ICacheInvalidation cacheInvalidation)
            {
                var response = await next();

                if(response.Succeeded)
                {
                    // Invalidate cache from keys
                    foreach(var key in cacheInvalidation.CacheKeys)
                    {
                        _memoryCache.Remove(key);
                    }
                }

                return response;
            }

            return await next();
        }
    }
}
