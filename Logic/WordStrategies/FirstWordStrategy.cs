namespace ТЯПиМТ
{
    /// <summary>
    /// Стратегия для слова (000)*101(110)*
    /// </summary>
    public class FirstWordStrategy : BaseWordStrategy
    {

        public override int WordStrategyNumber => 1;

        public override bool CheckWord(Word word)
        {
            CurrentIndex = 0;

            FirstBlock:

            for (int i = 0; i < word.Value.Length; i++)
                System.Diagnostics.Trace.WriteLine($"{i}: {word.Value[i]}");

            CheckAndIncIndex('0', word.Value[CurrentIndex]);
            CheckAndIncIndex('0', word.Value[CurrentIndex]);
            CheckAndIncIndex('0', word.Value[CurrentIndex]);

            if (word.Value[CurrentIndex] == '0')
                goto FirstBlock;
            else
                goto SecondBlock;

            SecondBlock:

            CheckAndIncIndex('1', word.Value[CurrentIndex]);
            CheckAndIncIndex('0', word.Value[CurrentIndex]);
            CheckAndIncIndex('1', word.Value[CurrentIndex]);

            ThirdBlock:

            CheckAndIncIndex('1', word.Value[CurrentIndex]);
            CheckAndIncIndex('1', word.Value[CurrentIndex]);
            CheckAndIncIndex('0', word.Value[CurrentIndex]);

            if (CurrentIndex == word.Value.Length)
            {
                base.CheckWord(word);
                return true;
            }

            if (word.Value[CurrentIndex] == '1')
                goto ThirdBlock;
            else
                throw new FormatException("Ожидался символ: '0'");
        }

    }
}