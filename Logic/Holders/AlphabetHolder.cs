using System;
using System.Collections.Generic;
using System.Linq;

namespace ТЯПиМТ
{
    /// <summary>
    /// Инкапсулирует взаимодействие с алфавитами
    /// </summary>
    public static class AlphabetHolder
    {

        /// <summary>
        /// Словарь алфавитов. Ключ - номер стратегии проверки слова <see cref="BaseWordStrategy.WordStrategyNumber"/>; Значение - алфавит слова
        /// </summary>
        /// <remarks>Первым алфавитом всегда идёт алфавит спец. символов и комментариев</remarks>
        public static Dictionary<int, List<Symbol>> Alphabets { get; private set; }

        /// <summary>
        /// Строка, означающая начало комментария
        /// </summary>
        public static string StartComment;

        /// <summary>
        /// Строка, означающая конец комментария
        /// </summary>
        public static string EndComment;

        /// <summary>
        /// Добавляет комментариии в словарь алфавитов. Добавляет символы из слов в словарь алфавитов
        /// </summary>
        /// <param name="comments">Символы комментариев разделённые пробелом. Пример: "/* */"</param>
        /// <param name="alphabets">Символы алфавита слов разделённые пробелом "(символы 1 слова) (символы 2 слова)". Пример: "01 abcde"</param>
        public static void InitAlphabet(string comments, string alphabets)
        {
            Alphabets = new Dictionary<int, List<Symbol>>();
            Alphabets.Add(0, GetSpecialAlphabet());

            var data = comments.Split(' ');
            StartComment = data[0];
            EndComment = data[1];

            if (StartComment == EndComment)
                throw new FormatException("Открывающий и закрывающий комментарии должны быть разными !");

            Alphabets.TryGetValue(0, out var alphabet);
            alphabet.AddRange(data[0].Select(x => GetSymbol(x)).ToList());
            alphabet.AddRange(data[1].Select(x => GetSymbol(x)).ToList());

            data = alphabets.Split(' ');
            for (int i = 0; i < data.Length; i++)
                Alphabets.Add(i + 1, data[i].Select(x => GetSymbol(x)).ToList());
        }

        /// <summary>
        /// Возвращает <see cref="Symbol"/> с соответствующим ему типом <see cref="SymbolTypes"/>
        /// </summary>
        public static Symbol GetSymbol(char symbol)
        {
            var result = new Symbol();
            result.Value = symbol;

            if (char.IsLetter(symbol))
                result.Type = SymbolTypes.Letter;
            else if (char.IsDigit(symbol))
                result.Type = SymbolTypes.Digit;
            else if (char.IsSymbol(symbol) || char.IsSeparator(symbol) || char.IsPunctuation(symbol))
                result.Type = SymbolTypes.SpecialSymbol;
            else
                result.Type = SymbolTypes.Unknown;

            return result;
        }

        /// <summary>
        /// Возвращает алфавит со специальными символами
        /// </summary>
        private static List<Symbol> GetSpecialAlphabet()
        {
            return new List<char>() { '\n', '\r', ' ', ':', '=', ';', '[', ']', ',' }.Select(x => GetSymbol(x)).ToList();
        }

    }
}