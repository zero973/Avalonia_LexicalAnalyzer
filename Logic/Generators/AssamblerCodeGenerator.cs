using System.Text;

namespace ТЯПиМТ
{
    /// <summary>
    /// Лаба 7. Генертор ассамблерного кода. Числа из двоичной системы счисления конвертируются в десятичную.
    /// </summary>
    /// <example> https://youtu.be/NtAKUEQeTxk
    /// Входной текст: adcbbc := [ 000101110 , 000101110110 ] ; adcccc := [ 000000101110 , 000000101110110 ]
    /// .data Extrn adcbbc:word, adcccc:word
    /// .code
    /// mov bx, 0
    /// mov 000101110, adcbbc[bx]
    /// add bx, 2
    /// mov 000101110, adcbbc[bx]
    /// add bx, 2
    /// mov 000000101110, adcccc[bx]
    /// add bx, 2
    /// mov 000101110, adcccc[bx]
    /// </example>
    public class AssamblerCodeGenerator : ICodeGenerator
    {

        public string Generate(List<Word> words)
        {
            StringBuilder result = new StringBuilder();

            result.Append($"Ассамблер код:{Environment.NewLine}");

            TablesHolder.Tables.TryGetValue(0, out var specialSymbols);
            var countOfSentences = specialSymbols.Count(x => x.Context.Value == ContextValues.DotAndComma) + 1;

            TablesHolder.Tables.TryGetValue(2, out var secondWords);
            result.Append($".data Extrn {string.Join(":word, ", secondWords.Select(x => x.Value))}:word{Environment.NewLine}" +
                $".code{Environment.NewLine}mov bx, 0{Environment.NewLine}");

            for (int i = 0; i < countOfSentences; i++)
            {
                var context = new Context(i, ContextValues.FirstWord);
                var firstWords = words.Where(x => x.Context.Equals(context));
                foreach (var item in firstWords)
                    result.Append($"mov {ConvertToDecimal(item.Value)}d, {secondWords[i]}[bx]{Environment.NewLine}add bx, 2{Environment.NewLine}");
            }

            return result.ToString();
        }

		/// <summary>
		/// Конвертирует число из двоичной системы счисления в десятичную
		/// </summary>
		/// <param name="number">Двоичное число</param>
		/// <returns>Число в 10 СС</returns>
        private int ConvertToDecimal(string number)
        {
            var decimalNumber = 0;
            for (int i = 0; i < number.Length; i++)
            {
                if (number[number.Length - i - 1] == '0') continue;
                decimalNumber += (int)Math.Pow(2, i);
            }
            return decimalNumber;
        }

    }
}