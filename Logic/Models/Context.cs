namespace ТЯПиМТ
{
    /// <summary>
    /// Контекст слова
    /// </summary>
    public struct Context
    {

        /// <summary>
        /// Номер предложения
        /// </summary>
        public int SentenceNumber { get; set; }

        /// <summary>
        /// "Значение" контекста
        /// </summary>
        /// <example>Для разных групп слов можно указывать разные значения контекста. 
        /// Например: adcbbc - 1; 000101110 - 2; ":=" - 3 и т.д.</example>
        public ContextValues Value { get; set; }

        public Context(int sentenceNumber, ContextValues value)
        {
            SentenceNumber = sentenceNumber;
            Value = value;
        }

        public override string ToString()
        {
            return $"SN:{SentenceNumber} V:{Value}";
        }

    }
}
