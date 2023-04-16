using AutoMapper;
using MediatR;
using RickAndMorty.Core.Data.Abstractions;
using RickAndMorty.Core.Domain.Entities;
using RickAndMorty.Core.Services.Commands;
using RickAndMorty.Core.Services.Commands.Messages;
using RickAndMorty.Core.Services.Helpers;
using RickAndMorty.Core.Services.Models;
using RickAndMorty.Core.Services.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Handlers
{
    public class CharacterHandlers
        : IRequestHandler<GetCharactersQuery, MediatorResponse<IList<CharacterModel>>>
        , IRequestHandler<AddCharacterCommand, MediatorResponse<CharacterModel>>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public CharacterHandlers(
            ICharacterRepository characterRepository,
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _characterRepository = characterRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<MediatorResponse<IList<CharacterModel>>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Character, bool>>? filter = request.OriginId.HasValue
                ? (Expression<Func<Character, bool>>)(c => c.OriginId == request.OriginId.Value)
                : null;

            var characters = await _characterRepository.GetListAsync(filter, cancellationToken);

            var characterModels = _mapper.Map<IList<CharacterModel>>(characters);

            return new MediatorResponse<IList<CharacterModel>>(characterModels);
        }

        public async Task<MediatorResponse<CharacterModel>> Handle(AddCharacterCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = new List<string>();
            
            if (string.IsNullOrEmpty(request.Name))
                validationErrors.Add(CharacterCommandErrors.InvalidName);

            var origin = request.OriginId.HasValue
                ? await _locationRepository.GetSingleAsync(l => l.Id == request.OriginId.Value, cancellationToken)
                : null;

            var location = request.LocationId.HasValue
                ? await _locationRepository.GetSingleAsync(l => l.Id == request.LocationId.Value, cancellationToken)
                : null;

            if (request.OriginId.HasValue && origin == null)
            {
                validationErrors.Add(CharacterCommandErrors.OriginNotFound);
            }

            if (request.LocationId.HasValue && location == null)
            {
                validationErrors.Add(CharacterCommandErrors.LocationNotFound);
            }

            if (validationErrors.Any())
                return new MediatorResponse<CharacterModel>(validationErrors);

            if (request.OriginId.HasValue)
            {
                request.CacheKeys.Add(CacheKeyHelper.CharacterCacheKey(request.OriginId.Value));
            }

            var character = new Character
            {
                Name = request.Name,
                Gender = request.Gender,
                Type = request.Type,
                Image = request.ImageUrl,
                Species = request.Species,
                Origin = origin,
                Location = location,
                Created = DateTime.UtcNow,
                Status = "Alive"
            };

            var addedCharacter = await _characterRepository.AddAsync(character, cancellationToken);

            return new MediatorResponse<CharacterModel>(_mapper.Map<CharacterModel>(addedCharacter));
        }
    }
}
