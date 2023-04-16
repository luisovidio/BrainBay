using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RickAndMorty.Core.Domain.ExternalEntities
{
    public class ExternalEpisode : IExternalClass
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public DateTime Created { get; set; }

        [JsonPropertyName("air_date")]
        public string AirDate { get; set; } = default!;
        public string Episode { get; set; } = default!;
        public IList<string> Characters { get; set; } = new List<string>();
    }
}
