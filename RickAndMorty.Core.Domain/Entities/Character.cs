using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Domain.Entities
{
    public class Character
    {
        public long Id { get; set; }
        /// <summary>
        /// Surrogate key
        /// </summary>
        public long? Sk { get; set; }
        public string Name { get; set; } = default!;
        public string Status { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public long? LocationId { get; set; }
        public long? OriginId { get; set; }
        public string Url { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        #region Navigation Properties
        public virtual IList<Episode> Episodes { get; set; } = new List<Episode>();
        public virtual Location? Origin { get; set; }
        public virtual Location? Location { get; set; }
        #endregion
    }
}
