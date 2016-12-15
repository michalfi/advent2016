namespace Advent2016.Bunny.Assembunny
{
    public class Literal : ISource
    {
        public Literal(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Literal;
            return other != null && this.Equals(other);
        }

        public bool Equals(Literal other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}
