namespace Lib.Tokenization.Domain.Interfaces;

public interface ITokenizerFactory
{
    ITokenizer BuildFromText(string text);
    ITokenizer FromPayload(object payload);
}