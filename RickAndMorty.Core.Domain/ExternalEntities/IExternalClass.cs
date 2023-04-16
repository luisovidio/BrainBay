using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Domain.ExternalEntities
{
    /// <summary>
    /// Interface for entiies from the external API
    /// </summary>
    public interface IExternalClass
    {
        long Id { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        DateTime Created { get; set; }
    }
}
