using RickAndMorty.Core.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Models
{
    public class MediatorResponse<T> : IMediatorResponse<T> 
        where T : class
    {
        public MediatorResponse(T result)
        {
            Result = result;
        }

        public MediatorResponse(IList<string> errors)
        {
            Succeeded = false;
            Errors = errors;
        }

        public bool FromDatabase { get; set; } = true;
        public T? Result { get; }
        public bool Succeeded { get; } = true;
        public IList<string> Errors { get; } = new List<string>();
    }
}
