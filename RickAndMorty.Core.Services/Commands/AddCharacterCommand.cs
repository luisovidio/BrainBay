using MediatR;
using RickAndMorty.Core.Services.Abstraction;
using RickAndMorty.Core.Services.Helpers;
using RickAndMorty.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Commands
{
    public class AddCharacterCommand : IRequest<MediatorResponse<CharacterModel>>, ICacheInvalidation
    {
        public string Name { get; }
        public string Species { get; }
        public string Type { get; }
        public string Gender { get; }
        public string ImageUrl { get; }
        public long? OriginId { get; }
        public long? LocationId { get; }
        public IList<string> CacheKeys { get; } = new List<string>() { CacheKeyHelper.CharacterCacheKey() };

        public AddCharacterCommand(
            string name,
            string species,
            string gender,
            string type,
            string imageUrl,
            long? originId = null,
            long? locationId = null)
        {
            Name = name;
            Species = species ?? string.Empty;
            Type = type ?? string.Empty;
            Gender = gender ?? string.Empty;
            ImageUrl = imageUrl ?? string.Empty;
            OriginId = originId;
            LocationId = locationId;
        }
    }
}
