namespace ТЯПиМТ
{
    public interface ICodeGenerator
    {

		/// <summary>
		/// Сгенерировать код
		/// </summary>
		/// <param name="words">Слова, по которым будет осуществляться генерация</param>
		/// <returns>Сгенерированный код</returns>
        string Generate(List<Word> words);

    }
}