using System.Text.Json;
using Lib.Tokenization.Domain.Interfaces;
using Lib.Tokenization.Application;

namespace Lib.Tokenization.Infrastructure.Serialization
{
    public static class TokenizerPayloadSerializer
    {
        public static ITokenizer RestoreTokenizer(string tokenizerKind, JsonElement payload)
        {
            var factory = new TokenizerFactory();
            return factory.Create(tokenizerKind, payload);
        }
    }
}