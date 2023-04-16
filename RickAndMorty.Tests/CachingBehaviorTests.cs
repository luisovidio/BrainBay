using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using RickAndMorty.Core.Services.Commands;
using RickAndMorty.Core.Services.Models;
using RickAndMorty.Core.Services.PipelineBehaviors;
using RickAndMorty.Core.Services.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RickAndMorty.Tests
{
    public class CachingBehaviorTests
    {
        private readonly Mock<IMemoryCache> _memoryCache = new Mock<IMemoryCache>();

        private readonly CachingBehavior<GetCharactersQuery, MediatorResponse<IList<CharacterModel>>> _getCharactersQueryBehavior;
        private readonly CachingBehavior<AddCharacterCommand, MediatorResponse<CharacterModel>> _addCharacterCommandBehavior;

        private readonly GetCharactersQuery _getCharactersQuery = new GetCharactersQuery();
        private readonly AddCharacterCommand _addCharacterCommand = new AddCharacterCommand("AnyName", "AnySpecies", "AnyGender", "AnyType", "AnyImage");

        private MediatorResponse<IList<CharacterModel>> _getCharactersQueryResponse = new MediatorResponse<IList<CharacterModel>>(new List<CharacterModel>());
        private MediatorResponse<CharacterModel> _addCharacterCommandResponse = new MediatorResponse<CharacterModel>(new CharacterModel());

        private RequestHandlerDelegate<MediatorResponse<IList<CharacterModel>>> _getCharactersDelegate;
        private RequestHandlerDelegate<MediatorResponse<CharacterModel>> _addCharacterDelegate;

        private readonly CancellationTokenSource _ctSource = new CancellationTokenSource();

        public CachingBehaviorTests()
        {
            _getCharactersQueryBehavior = new CachingBehavior<GetCharactersQuery, MediatorResponse<IList<CharacterModel>>>(_memoryCache.Object);
            _addCharacterCommandBehavior = new CachingBehavior<AddCharacterCommand, MediatorResponse<CharacterModel>>(_memoryCache.Object);

            _getCharactersDelegate = () => Task.FromResult(_getCharactersQueryResponse);
            _addCharacterDelegate = () => Task.FromResult(_addCharacterCommandResponse);
        }

        [Fact]
        public async Task GetCharacters_should_return_from_cache()
        {
            _memoryCache.Setup(c => c.Get<MediatorResponse<IList<CharacterModel>>>(_getCharactersQuery.CacheKey))
                .Returns(_getCharactersQueryResponse);

            var response = await _getCharactersQueryBehavior.Handle(_getCharactersQuery, _getCharactersDelegate, _ctSource.Token);

            Assert.NotNull(response);
            Assert.False(response.FromDatabase);
        }

        [Fact]
        public async Task GetCharacters_should_return_from_database_and_add_to_cache()
        {
            var response = await _getCharactersQueryBehavior.Handle(_getCharactersQuery, _getCharactersDelegate, _ctSource.Token);

            Assert.NotNull(response);
            Assert.True(response.FromDatabase);
            _memoryCache.Verify(c => c.Set(_getCharactersQuery.CacheKey, It.IsAny<MediatorResponse<IList<CharacterModel>>>()), Times.Once);
        }
    }
}
