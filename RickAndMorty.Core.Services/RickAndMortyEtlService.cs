using RickAndMorty.Core.Data.Abstractions;
using RickAndMorty.Core.Domain.Entities;
using RickAndMorty.Core.Domain.ExternalEntities;
using RickAndMorty.Core.Integration.Abstraction;
using RickAndMorty.Core.Integration.Models;
using RickAndMorty.Core.Services.Abstraction;
using RickAndMorty.Core.Services.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services
{
    public class RickAndMortyEtlService : IRickAndMortyEtlService
    {
        private IRickAndMortyHttpClient _httpClient;
        private ILocationRepository _locationRepository;
        private IEpisodeRepository _episodeRepository;
        private ICharacterRepository _characterRepository;

        public RickAndMortyEtlService(
            IRickAndMortyHttpClient httpClient,
            ILocationRepository locationRepository,
            IEpisodeRepository episodeRepository,
            ICharacterRepository characterRepository)
        {
            _httpClient = httpClient;
            _locationRepository = locationRepository;
            _episodeRepository = episodeRepository;
            _characterRepository = characterRepository;
        }

        public async Task ResetDatabaseAsync(CancellationToken cancellationToken = default)
        {
            await _characterRepository.DeleteAll(cancellationToken);
            await _locationRepository.DeleteAll(cancellationToken);
            await _episodeRepository.DeleteAll(cancellationToken);
        }

        public async Task EtlDataAsync(CancellationToken cancellationToken = default)
        {
            await EtlLocationAsync(cancellationToken);
            await EtlEpisodeAsync(cancellationToken);
            await EtlCharacterAsync(cancellationToken);
        }

        private async Task EtlCharacterAsync(CancellationToken cancellationToken)
        {
            var externalCharacters = await LoadAllPagesAsync<ExternalCharacter>(RickAndMortyApiControllers.Character, cancellationToken);

            externalCharacters = externalCharacters.Where(c => c.Status == "Alive").ToList();

            var locations = await _locationRepository.GetAllAsync(cancellationToken);
            var locationDictionary = locations
                .ToDictionary(l => l.Url);

            var episodes = await _episodeRepository.GetAllAsync(cancellationToken);
            var episodeDictionary = episodes
                .ToDictionary(e => e.Url);

            var characters = externalCharacters
                .Select(c => new Character
                {
                    Sk = c.Id,
                    Gender = c.Gender,
                    Url = c.Url,
                    Image = c.Image,
                    Name = c.Name,
                    Created = c.Created,
                    Species = c.Species,
                    Status = c.Status,
                    Type = c.Type,
                    Location = locationDictionary.ContainsKey(c.Location.Url) 
                        ? locationDictionary[c.Location.Url]
                        : null,
                    Origin = locationDictionary.ContainsKey(c.Origin.Url)
                        ? locationDictionary[c.Origin.Url]
                        : null,
                    Episodes = c.Episode
                        .Where(e => episodeDictionary.ContainsKey(e))
                        .Select(e => episodeDictionary[e])
                        .ToList()
                }).ToList();

            await _characterRepository.BatchInsertAsync(characters, cancellationToken);
        }

        private async Task EtlEpisodeAsync(CancellationToken cancellationToken)
        {
            var externalEpisodes = await LoadAllPagesAsync<ExternalEpisode>(RickAndMortyApiControllers.Episode, cancellationToken);

            var episodes = externalEpisodes.Select(e => new Episode
            {
                Sk = e.Id,
                AirDate = e.AirDate,
                Created = e.Created,
                EpisodeNumber = e.Episode,
                Name = e.Name,
                Url = e.Url
            }).ToList();

            await _episodeRepository.BatchInsertAsync(episodes, cancellationToken);
        }

        private async Task EtlLocationAsync(CancellationToken cancellationToken)
        {
            var externalLocations = await LoadAllPagesAsync<ExternalLocation>(RickAndMortyApiControllers.Location, cancellationToken);

            var locations = externalLocations.Select(l => new Location
            {
                Sk = l.Id,
                Created = l.Created,
                Dimension = l.Dimension,
                Name = l.Name,
                Type = l.Type,
                Url = l.Url
            }).ToList();

            await _locationRepository.BatchInsertAsync(locations, cancellationToken);
        }

        /// <summary>
        /// Load all pages in memory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<IList<T>> LoadAllPagesAsync<T>(string controller, CancellationToken cancellationToken)
            where T : IExternalClass
        {
            var allPages = new List<ResponseModel<T>>();
            var firstPage = await _httpClient.GetPageAsync<T>(controller, 1, cancellationToken);

            allPages.Add(firstPage);
            var loadTasks = new List<Task<ResponseModel<T>>>();
            for (int i = 2; i <= firstPage.Info.Pages; ++i)
            {
                loadTasks.Add(_httpClient.GetPageAsync<T>(controller, i, cancellationToken));
            }

            allPages.AddRange(await Task.WhenAll(loadTasks));

            return allPages.SelectMany(p => p.Results).ToList();
        }
    }
}
