using AutoMapper;
using Moq;
using RickAndMorty.Core.Data.Abstractions;
using RickAndMorty.Core.Domain.Entities;
using RickAndMorty.Core.Services.Commands;
using RickAndMorty.Core.Services.Commands.Messages;
using RickAndMorty.Core.Services.Handlers;
using RickAndMorty.Core.Services.Helpers;
using RickAndMorty.Core.Services.Queries;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using Xunit;

namespace RickAndMorty.Tests
{
    public class CharacterHandlersTests
    {
        private readonly CharacterHandlers _handler;
        private readonly Mock<ICharacterRepository> _characterRepository = new Mock<ICharacterRepository>();
        private readonly Mock<ILocationRepository> _locationRepository = new Mock<ILocationRepository>();

        private AddCharacterCommand _addCharacterCommand;
        private GetCharactersQuery _getCharactersQuery;
        private CancellationTokenSource _ctSource = new CancellationTokenSource(); 

        public CharacterHandlersTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddMaps("RickAndMorty.Core.Services"));

            _handler = new CharacterHandlers(
                _characterRepository.Object,
                _locationRepository.Object,
                new Mapper(mapperConfiguration));

            _addCharacterCommand = new AddCharacterCommand("AnyName", "AnySpecies", "AnyGender", "AnyType", "AnyImage");

            _getCharactersQuery = new GetCharactersQuery();

            _characterRepository.Setup(r => r.AddAsync(It.IsAny<Character>(), _ctSource.Token))
                .ReturnsAsync(new Character());
        }

        #region Add Character
        [Fact]
        public async void AddCharacter_should_succeed()
        {
            var response = await _handler.Handle(_addCharacterCommand, _ctSource.Token);

            Assert.NotNull(response);
            Assert.True(response.Succeeded);
        }

        [Fact]
        public async void AddCharacter_should_succeed_and_invalidate_cache_for_given_origin()
        {
            _locationRepository.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<Location, bool>>>(), _ctSource.Token))
                .ReturnsAsync(new Location());

            _addCharacterCommand = new AddCharacterCommand("AnyName", "AnySpecies", "AnyGender", "AnyType", "AnyImage", 0);

            var response = await _handler.Handle(_addCharacterCommand, _ctSource.Token);

            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.Contains(CacheKeyHelper.CharacterCacheKey(_addCharacterCommand.OriginId!.Value), _addCharacterCommand.CacheKeys);
        }

        [Fact]
        public async void AddCharacter_should_fail_when_name_is_invalid()
        {
            _addCharacterCommand = new AddCharacterCommand("", "AnySpecies", "AnyGender", "AnyType", "AnyImage");

            var response = await _handler.Handle(_addCharacterCommand, _ctSource.Token);

            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Contains(CharacterCommandErrors.InvalidName, response.Errors);
        }

        [Fact]
        public async void AddCharacter_should_fail_when_origin_is_not_found()
        {
            _locationRepository.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<Location, bool>>>(), _ctSource.Token))
                .ReturnsAsync((Location?)null);

            _addCharacterCommand = new AddCharacterCommand("AnyName", "AnySpecies", "AnyGender", "AnyType", "AnyImage", 0);

            var response = await _handler.Handle(_addCharacterCommand, _ctSource.Token);

            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Contains(CharacterCommandErrors.OriginNotFound, response.Errors);
        }

        [Fact]
        public async void AddCharacter_should_fail_when_location_is_not_found()
        {
            _locationRepository.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<Location, bool>>>(), _ctSource.Token))
                .ReturnsAsync((Location?)null);

            _addCharacterCommand = new AddCharacterCommand("AnyName", "AnySpecies", "AnyGender", "AnyType", "AnyImage", null, 0);

            var response = await _handler.Handle(_addCharacterCommand, _ctSource.Token);

            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Contains(CharacterCommandErrors.LocationNotFound, response.Errors);
        }
        #endregion

        #region Get Characters
        [Fact]
        public async void GetCharacters_should_succeed_without_filter()
        {
            var response = await _handler.Handle(_getCharactersQuery, _ctSource.Token);

            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            _characterRepository.Verify(r => r.GetListAsync(null, _ctSource.Token), Times.Once);
        }

        [Fact]
        public async void GetCharacters_should_succeed_with_filter()
        {
            _getCharactersQuery = new GetCharactersQuery(1);

            var response = await _handler.Handle(_getCharactersQuery, _ctSource.Token);

            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            _characterRepository.Verify(r => r.GetListAsync(It.IsAny<Expression<Func<Character, bool>>>(), _ctSource.Token), Times.Once);
        }
        #endregion
    }
}