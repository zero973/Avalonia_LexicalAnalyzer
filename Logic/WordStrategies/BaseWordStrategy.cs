namespace ТЯПиМТ
{
    /// <summary>
    /// Базовый класс для стратегий проверки слов
    /// </summary>
    public class BaseWordStrategy
    {

        /// <summary>
        /// Номер стратегии
        /// </summary>
        public virtual int WordStrategyNumber => -1;

        /// <summary>
        /// Текущий индекс буквы в слове
        /// </summary>
        /// <remarks>Используется в методах <see cref="CheckWord(Word)"/> и <see cref="CheckAndIncIndex(char, char)"/></remarks>
        protected int CurrentIndex;

        /// <summary>
        /// Список всех стратегий проверок слов
        /// </summary>
        protected static readonly List<BaseWordStrategy> WordStrategies;

        static BaseWordStrategy()
        {
            WordStrategies = new List<BaseWordStrategy>()
            {
                new SpecialSymbolWordStrategy(),
                new FirstWordStrategy(),
                new SecondWordStrategy()
            };
        }

        /// <summary>
        /// Проверка слова на соответствие заданному правилу и добавление его в <see cref="TablesHolder"/>
        /// </summary>
        /// <returns>true - если слово успешно прошло проверку, иначе - Exception</returns>
        /// <exception cref="FormatException"/>
        public virtual bool CheckWord(Word word)
        {
            TablesHolder.AddWord(word);
            return true;
        }

        /// <summary>
        /// Определяет тип стратегии слова по переданному <paramref name="word"/>
        /// </summary>
        /// <param name="word">Слово</param>
        /// <returns>Возвращает найденную стратегию, иначе <see cref="Exception"/></returns>
        /// <exception cref="Exception"></exception>
        public static BaseWordStrategy GetWordStrategy(string word)
        {
            int strategyNum = -1;
            foreach (var alphabet in AlphabetHolder.Alphabets)
            {
                if (alphabet.Value.Any(symbol => word.Contains(symbol.Value)))
                {
                    strategyNum = alphabet.Key;
                    break;
                }
            }

            if (strategyNum == -1)
                throw new Exception("Не удалось определить тип стратегии слова.");

            return WordStrategies.Single(x => x.WordStrategyNumber == strategyNum);
        }

        public static BaseWordStrategy GetWordStrategy(int wordStrategyNumber)
        {
            return WordStrategies.Single(x => x.WordStrategyNumber == wordStrategyNumber);
        }

        /// <summary>
        /// Проверяет букву 
        /// </summary>
        /// <param name="letter">Буква, которая должна быть</param>
        /// <param name="curSym">Текущая буква</param>
        /// <exception cref="FormatException"></exception>
        protected virtual void CheckAndIncIndex(char letter, char curSym)
        {
            if (letter != curSym)
                throw new FormatException($"Ожидался символ: '{letter}'");
            CurrentIndex++;
        }

        /// <summary>
        /// Проверяет, что <paramref name="curSym"/> находится в <paramref name="letters"/>
        /// </summary>
        /// <param name="letters">Одна из ожидаемых букв</param>
        /// <param name="curSym">Текущая буква</param>
        /// <exception cref="FormatException"></exception>
        protected virtual void CheckAndIncIndex(char[] letters, char curSym)
        {
            if (!letters.Contains(curSym))
                throw new FormatException($"Ожидался один из следующих символов: {string.Join(", ", letters)}");
            CurrentIndex++;
        }

    }
}