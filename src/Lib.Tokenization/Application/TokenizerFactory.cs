using System.Text.Json;
using Lib.Tokenization.Domain.Entities;
using Lib.Tokenization.Domain.Interfaces;

namespace Lib.Tokenization.Application
{
    public class TokenizerFactory : ITokenizerFactory
    {
        public ITokenizer Create(string type)
        {
            switch (type.ToLower())
            {
                case "char":
					return CharTokenizer.BuildFromText("");
                case "word":
					 return WordTokenizer.BuildFromText("");
                default: 
					throw new ArgumentException($"Unknown tokenizer type: {type}");
            }
        }

        public ITokenizer Create(string type, object payload)
        {
            JsonElement json = (JsonElement)payload;

            if (type.ToLower() == "char")
            {
                char[] restored = json.GetProperty("Chars").Deserialize<char[]>();
                var vocab = new Vocabulary<char>();
                
                for (int i = 1; i < restored.Length; i++)
                {
                    vocab.Add(restored[i]); 
                }
                return new CharTokenizer(vocab);
            }
            else if (type.ToLower() == "word")
            {
                string[] restored = json.GetProperty("Words").Deserialize<string[]>();
                var vocab = new WordVocabulary();
                
                for (int i = 1; i < restored.Length; i++)
                {
                    if (restored[i] != null) vocab.Add(restored[i]);
                }
                return new WordTokenizer(vocab);
            }

            throw new ArgumentException($"Unknown tokenizer type: {type}");
        }
    }
}