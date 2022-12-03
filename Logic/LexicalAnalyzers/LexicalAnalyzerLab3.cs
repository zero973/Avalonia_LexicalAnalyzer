using System;
using System.Collections.Generic;

namespace ТЯПиМТ
{
    /// <summary>
    /// Анализатор третьей лабы. Удаление комментариев и проверка на соответствие слов регулярной граммтике.
    /// Пример текста: "adcbbc := [ 000101110 , 000101110 , ... ] ; adcccc := [ 000000101110 , 000101110 ]"
    /// </summary>
    public class LexicalAnalyzerLab3 : BaseLexicalAnalyzer
    {

        private int CurrentIndex;
        private int CurSentenceNum;

        public override List<Symbol> Analysis(string text)
        {
            text = PrepareText(text);
            var words = GetWordsFromText(text);

            FirstBlock:

            var currentWordStrategy = BaseWordStrategy.GetWordStrategy(words[CurrentIndex].WordStrategyNumber);
            CheckAndIncIndex(2, currentWordStrategy, ContextValues.SecondWord, words[CurrentIndex]);

            currentWordStrategy = BaseWordStrategy.GetWordStrategy(words[CurrentIndex].WordStrategyNumber);
            CheckAndIncIndex(0, currentWordStrategy, ContextValues.TwoDotsAndEqual, words[CurrentIndex], ":=");

            currentWordStrategy = BaseWordStrategy.GetWordStrategy(words[CurrentIndex].WordStrategyNumber);
            CheckAndIncIndex(0, currentWordStrategy, ContextValues.OpenBracket, words[CurrentIndex], "[");

            SecondBlock:

            currentWordStrategy = BaseWordStrategy.GetWordStrategy(words[CurrentIndex].WordStrategyNumber);
            CheckAndIncIndex(1, currentWordStrategy, ContextValues.FirstWord, words[CurrentIndex]);

            if (words[CurrentIndex].Value == ",")
            {
                words[CurrentIndex].Context = new Context(CurSentenceNum, ContextValues.Comma);
                TablesHolder.AddWord(words[CurrentIndex]);
                CurrentIndex++;
                goto SecondBlock;
            }
            else if (words[CurrentIndex].Value == "]")
            {
                words[CurrentIndex].Context = new Context(CurSentenceNum, ContextValues.CloseBracket);
                TablesHolder.AddWord(words[CurrentIndex]);
                CurrentIndex++;
            }

            if (CurrentIndex + 1 < words.Count)
            {
                if (words[CurrentIndex].Value == ";")
                {
                    words[CurrentIndex].Context = new Context(CurSentenceNum, ContextValues.DotAndComma);
                    TablesHolder.AddWord(words[CurrentIndex]);
                    CurrentIndex++;
                    CurSentenceNum++;
                    goto FirstBlock;
                }
                else
                    throw new FormatException("Ожидался символ: ';'");
            }

            return base.Analysis(text);
        }

        /// <summary>
        /// Сравнивает номера <paramref name="curWordStarategy"/> и <paramref name="requiredWordStarategy"/>. 
        /// И если они сходятся, то выполняет проверку правильности слова данной стратегии.
        /// И сравнивает <paramref name="curWord"/> и <paramref name="curWordStarategy"/>
        /// </summary>
        /// <param name="requiredWordStarategy">Требуемая стратегия</param>
        /// <param name="curWordStarategy">Найденная стратегия</param>
        /// <param name="requiredWord">Требуемое слово</param>
        /// <param name="curWord">Текущее слово</param>
        protected virtual void CheckAndIncIndex(int requiredWordStarategy, BaseWordStrategy curWordStarategy, 
            ContextValues context, Word curWord, string requiredWord = null)
        {
            curWord.Context = new Context(CurSentenceNum, context);

            if (requiredWordStarategy == curWordStarategy.WordStrategyNumber)
                curWordStarategy.CheckWord(curWord);
            else
                throw new FormatException($"Ожидался символ: '{requiredWord}'");

            if (requiredWord != null && requiredWord != curWord.Value)
                throw new FormatException($"Ожидался символ: '{requiredWord}'");

            CurrentIndex++;
        }

    }
}