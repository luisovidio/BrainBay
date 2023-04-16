using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Domain.ExternalEntities
{
    /// <summary>
    /// Character class
    /// </summary>
    public class ExternalCharacter : IExternalClass
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Species { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Image { get; set; } = default!;
        public ExternalSimplifiedLocation Origin { get; set; } = default!;
        public ExternalSimplifiedLocation Location { get; set; } = default!;
        public string Url { get; set; } = default!;
        public DateTime Created { get; set; }
        public IList<string> Episode { get; set; } = new List<string>();
    }
}
