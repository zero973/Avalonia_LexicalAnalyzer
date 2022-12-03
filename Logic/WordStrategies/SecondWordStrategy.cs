namespace ТЯПиМТ
{
    /// <summary>
    /// Стратегия для слова (a|b|c|d)+, первые два символа всегда ad
    /// </summary>
    public class SecondWordStrategy : BaseWordStrategy
    {

        public override int WordStrategyNumber => 2;

        public override bool CheckWord(Word word)
        {
            CurrentIndex = 0;

            // Первые два символа всегда ad
            CheckAndIncIndex('a', word.Value[CurrentIndex]);
            CheckAndIncIndex('d', word.Value[CurrentIndex]);

            while (CurrentIndex != word.Value.Length)
            {
                CheckAndIncIndex(new char[] { 'a', 'b', 'c', 'd' }, word.Value[CurrentIndex]);
            }

            base.CheckWord(word);
            return true;
        }

    }
}