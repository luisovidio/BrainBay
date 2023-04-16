using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Domain.ExternalEntities
{
    /// <summary>
    /// Simplified record for location
    /// </summary>
    /// <param name="Name">Location name</param>
    /// <param name="Url">Location Url</param>
    public record ExternalSimplifiedLocation(
        string Name,
        string Url);
}
