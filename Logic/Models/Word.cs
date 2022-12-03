namespace ТЯПиМТ
{
    public class Word
    {

        public int WordStrategyNumber { get; set; }

        public string Value { get; set; }

        public Context Context { get; set; }

        public int Hash { get; set; }

        public Word(int wordStrategyNumber, string value, Context context)
        {
            WordStrategyNumber = wordStrategyNumber;
            Value = value;
            Context = context;
            Hash = GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            var t = obj as Word;
            return Hash.Equals(t?.Hash);
        }

        public override int GetHashCode()
        {
            int result = 0;

            for (int i = 0; i < Value.Length; i++)
            {
                switch (i % 3)
                {
                    case 0: result += (Value[i] + 1) * 453; break;
                    case 1: result += (Value[i] + 1) * 987; break;
                    case 2: result += (Value[i] + 1) * 123; break;
                }
            }

            return result;
        }

    }
}