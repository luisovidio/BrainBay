using AutoMapper;
using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Models
{
    [AutoMap(typeof(Character))]
    public class CharacterModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Surrogate key - identifier from external API
        /// </summary>
        public long? Sk { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = default!;
        /// <summary>
        /// Species
        /// </summary>
        public string Species { get; set; } = default!;
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = default!;
        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; } = default!;
        /// <summary>
        /// Image Url
        /// </summary>
        public string Image { get; set; } = default!;
        /// <summary>
        /// Created at (UTC)
        /// </summary>
        public DateTime Created { get; set; }

        #region Navigation Properties
        /// <summary>
        /// Episodes that this character appears
        /// </summary>
        public virtual IList<EpisodeModel> Episodes { get; set; } = new List<EpisodeModel>();
        /// <summary>
        /// Origin
        /// </summary>
        public virtual LocationModel? Origin { get; set; }
        /// <summary>
        /// Current location
        /// </summary>
        public virtual LocationModel? Location { get; set; }
        #endregion
    }
}
