using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Services.Commands.Messages
{
    public static class CharacterCommandErrors
    {
        public const string InvalidName = "Name is required";
        public const string OriginNotFound = "Origin not found";
        public const string LocationNotFound = "Location not found";
    }
}
