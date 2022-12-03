namespace ТЯПиМТ
{
    /// <summary>
    /// Анализатор второй лабы. Удаление комментариев и проверка на соответствие слов регулярной граммтике.
    /// Пример текста: "000101110 adcbbc adcbbc 000101110 ..."
    /// </summary>
    public class LexicalAnalyzerLab2 : BaseLexicalAnalyzer
    {
        
        public override List<Symbol> Analysis(string text)
        {
            text = PrepareText(text);
            foreach (var word in GetWordsFromText(text))
            {
                var currentWordStrategy = BaseWordStrategy.GetWordStrategy(word.WordStrategyNumber);
                currentWordStrategy.CheckWord(word);
            }

            return base.Analysis(text).Where(x => x.Type == SymbolTypes.Letter || x.Type == SymbolTypes.Digit).ToList();
        }

    }
}