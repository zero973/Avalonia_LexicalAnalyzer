namespace ТЯПиМТ
{
    /// <summary>
    /// Лаба 4. Холдер для таблиц слов
    /// </summary>
    public static class TablesHolder
    {

        /// <summary>
        /// Таблицы слов. Ключ - номер стратегии проверки слова <see cref="BaseWordStrategy.WordStrategyNumber"/>; Значение - List хэшей слов
        /// </summary>
        public static Dictionary<int, List<Word>> Tables;

        static TablesHolder()
        {
            Tables = new Dictionary<int, List<Word>>();
        }

        public static void AddWord(Word word)
        {
            var wordStrategy = word.WordStrategyNumber;

            if (Tables.TryGetValue(wordStrategy, out _))
                Tables[wordStrategy].Add(word);
            else
                Tables.Add(wordStrategy, new List<Word>() { word });
        }

    }
}