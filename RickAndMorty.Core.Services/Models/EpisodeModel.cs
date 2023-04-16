using AutoMapper;
using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Models
{
    [AutoMap(typeof(Episode))]
    public class EpisodeModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Surrogate key - Id from external API
        /// </summary>
        public long? Sk { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Created at (UTC)
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Air date
        /// </summary>
        public string AirDate { get; set; } = default!;
        /// <summary>
        /// Episode number SxxExx
        /// </summary>
        public string EpisodeNumber { get; set; } = default!;
    }
}
