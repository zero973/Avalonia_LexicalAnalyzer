using System.Collections.Generic;

namespace ТЯПиМТ
{
    public interface ICodeGenerator
    {

        string Generate(List<Word> words);

    }
}