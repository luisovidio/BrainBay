using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Abstraction
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
        TimeSpan SlidingExpiration { get; }
    }
}
