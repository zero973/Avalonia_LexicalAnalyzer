namespace ТЯПиМТ
{
    /// <summary>
    /// Анализатор 6 лабы. То же самое, что и 3 лаба, но добавлена проверка на одинаковые числа в тексте.
    /// </summary>
    public class LexicalAnalyzerLab6 : LexicalAnalyzerLab3
    {

        public override List<Symbol> Analysis(string text)
        {
            var result = base.Analysis(text);

            TablesHolder.Tables.TryGetValue(1, out var values);
            // Если есть одинаковые цифры в тексте
            if (values.GroupBy(x => x.Value).Any(g => g.Count() > 1))
                throw new FormatException("Обнаружены дублирующиеся цифры в тексте");

            return result;
        }

    }
}