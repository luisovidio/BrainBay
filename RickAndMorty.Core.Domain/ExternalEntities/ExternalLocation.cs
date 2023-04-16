using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Domain.ExternalEntities
{
    public class ExternalLocation : IExternalClass
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public DateTime Created { get; set; }

        public string Type { get; set; } = default!;
        public string Dimension { get; set; } = default!;
        public List<string> Residents { get; set; } = new List<string>();
    }
}
