using RickAndMorty.Core.Domain.ExternalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Integration.Models
{
    /// <summary>
    /// Response model from the rickandmortyapi.com
    /// </summary>
    public class ResponseModel<T> where T : IExternalClass // Can change to restrict data types to ExternalEntities
    {
        public ResponseInfoModel Info { get; set; } = default!;
        public IList<T> Results { get; set; } = new List<T>();
    }
}
