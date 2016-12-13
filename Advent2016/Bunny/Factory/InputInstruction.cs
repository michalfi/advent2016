namespace Advent2016.Bunny.Factory
{
    public class InputInstruction : IBotInstruction
    {
        public InputInstruction(int value, int bot)
        {
            Value = value;
            Bot = bot;
        }

        public int Value { get; }

        public int Bot { get; }

        public override bool Equals(object obj)
        {
            var other = obj as InputInstruction;
            return other != null && this.Equals(other);
        }

        protected bool Equals(InputInstruction other)
        {
            return Value == other.Value && Bot == other.Bot;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value*397) ^ Bot;
            }
        }

        public override string ToString()
        {
            return $"value {this.Value} goes to bot {this.Bot}";
        }
    }
}
