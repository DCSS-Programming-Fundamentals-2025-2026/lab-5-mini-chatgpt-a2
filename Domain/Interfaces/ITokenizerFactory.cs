using System;
namespace Lib.Tokenization.Domain.Interfaces;

public interface ITokenizerFactory
{
    ITokenizer Create(string type);
    ITokenizer Create(string type, object payload);
}