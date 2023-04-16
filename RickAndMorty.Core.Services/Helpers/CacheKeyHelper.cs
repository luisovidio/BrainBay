using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Helpers
{
    public static class CacheKeyHelper
    {
        private const string CharacterPrefix = "character";

        public static string CharacterCacheKey() => CharacterPrefix;

        public static string CharacterCacheKey(long originId) => $"{CharacterPrefix}-o-{originId}";
    }
}
