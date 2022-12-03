namespace ТЯПиМТ
{
    public class Symbol
    {

		/// <summary>
		/// Значение
		/// </summary>
        public char Value { get; set; }

		/// <summary>
		/// Тип
		/// </summary>
        public SymbolTypes Type { get; set; }

        public Symbol()
        {

        }

        public Symbol(char value, SymbolTypes type)
        {
            Value = value;
            Type = type;
        }

        public override string ToString()
        {
            return $"{Type} {Value}";
        }

    }
}