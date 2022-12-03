namespace ТЯПиМТ
{
    /// <summary>
    /// Стратегия для слов, состоящих только из спец. символов
    /// </summary>
    public class SpecialSymbolWordStrategy : BaseWordStrategy
    {

        public override int WordStrategyNumber => 0;

        public override bool CheckWord(Word word)
        {
            CurrentIndex = 0;

            // Есть ли в переданном слове какой-то символ, который не относится к группе спец. символов (например буква или цифра)
            var result = word.Value.Any(x => AlphabetHolder.GetSymbol(x).Type != SymbolTypes.SpecialSymbol);

            if (!result)
                base.CheckWord(word);

            return false;
        }

    }
}