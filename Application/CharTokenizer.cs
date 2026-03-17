namespace Lib.Tokenization.Application
{
    public class CharTokenizer : ITokenizer
    {
        private readonly Dictionary<char, int> _charToId;
        
        private readonly char[] _idToChar;

        public int VocabSize => _idToChar.Length;

        private CharTokenizer(Dictionary<char, int> charToId, char[] idToChar)
        {
            _charToId = charToId;
            _idToChar = idToChar;
        }

        public static CharTokenizer BuildFromText(string text)
        {
            var charToId = new Dictionary<char, int>();
            var uniqueChars = new List<char>();

            char unkChar = ''; 
            charToId[unkChar] = 0;
            uniqueChars.Add(unkChar);

            int nextId = 1;

            foreach (char c in text)
            {
                if (!charToId.ContainsKey(c))
                {
                    charToId[c] = nextId;
                    uniqueChars.Add(c);
                    nextId++;
                }
            }

            return new CharTokenizer(charToId, uniqueChars.ToArray());
        }

        public int[] Encode(string text)
        {
            if (string.IsNullOrEmpty(text)) return new int[0];

            int[] tokens = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                
                if (_charToId.TryGetValue(c, out int id))
                {
                    tokens[i] = id;
                }
                else
                {
                    tokens[i] = 0;
                }
            }
            return tokens;
        }

        public string Decode(ReadOnlySpan<int> tokens)
        {
            StringBuilder sb = new StringBuilder(tokens.Length);
            
            foreach (int token in tokens)
            {
                if (token >= 0 && token < _idToChar.Length)
                {
                    sb.Append(_idToChar[token]);
                }
                else
                {
                    sb.Append(_idToChar[0]);
                }
            }
            return sb.ToString();
        }

        public object GetPayloadForCheckpoint()
        {
            return new { Chars = _idToChar };
        }
    }
}