using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Abstraction
{
    public interface IMediatorResponse
    {
        bool FromDatabase { get; set; }
        bool Succeeded { get; }
        IList<string> Errors { get; }
    }

    public interface IMediatorResponse<T> : IMediatorResponse
        where T : class
    {
        T? Result { get; }
    }
}
