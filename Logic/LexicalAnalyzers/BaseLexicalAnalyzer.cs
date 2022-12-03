namespace ТЯПиМТ
{
    public class BaseLexicalAnalyzer
    {

        public BaseLexicalAnalyzer()
        {

        }

        /// <summary>
        /// Анализ текста
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <returns>Список <see cref="Symbol"/></returns>
        /// <remarks>Базовая реализация (лаба 1) не предполагает каких-то проверок, кроме прнадлежности к алфавиту</remarks>
        /// <exception cref="FormatException"></exception>
        public virtual List<Symbol> Analysis(string text)
        {
            var result = new List<Symbol>();

            foreach (var symbol in text)
                result.Add(AlphabetHolder.GetSymbol(symbol));

            var allowedSymbols = AlphabetHolder.Alphabets.Values.SelectMany(x => x).Select(x => x.Value);
            foreach (var s in result)
                if (s.Type == SymbolTypes.Unknown || !allowedSymbols.Contains(s.Value))
                    throw new FormatException($"Обнаружен неизвестный символ - ({(int)s.Value})'{s.Value}'");

            return result;
        }

        /// <summary>
        /// Удаляет комментарии из текста и заменяет \r и \n на пробелы
        /// </summary>
        protected virtual string PrepareText(string text)
        {
            string result = text;

            result = result.Replace('\n', ' ');
            result = result.Replace('\r', ' ');

            var isStartComExists = text.Contains(AlphabetHolder.StartComment);
            var isEndComExists = text.Contains(AlphabetHolder.EndComment);

            if (!isStartComExists && !isEndComExists)
                return result;

            if (!isStartComExists && isEndComExists)
                throw new FormatException("Найден закрывающий комментарий, но не найден открывающий");

            // Удаляем закомментированный текст
            var comStartIndex = text.IndexOf(AlphabetHolder.StartComment);
            var comEndIndex = text.LastIndexOf(AlphabetHolder.EndComment) + AlphabetHolder.EndComment.Length;

            result = result.Remove(comStartIndex, comEndIndex - comStartIndex);

            return result;
        }

		/// <summary>
		/// Получить слова из текста
		/// </summary>
		/// <param name="text">Текст</param>
		/// <returns>Список слов</returns>
        protected virtual List<Word> GetWordsFromText(string text)
        {
            var result = new List<Word>();
            var words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
                result.Add(new Word(BaseWordStrategy.GetWordStrategy(word).WordStrategyNumber, word, new Context()));
            return result;
        }

    }
}