namespace ТЯПиМТ
{
    public class Symbol
    {

        public char Value { get; set; }

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