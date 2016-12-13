namespace Advent2016.Bunny.Factory
{
    public class BotInstruction : IBotInstruction
    {
        public BotInstruction(int bot, Destination lowDestination, Destination highDestination)
        {
            Bot = bot;
            LowDestination = lowDestination;
            HighDestination = highDestination;
        }

        public int Bot { get; }

        public Destination LowDestination { get; }

        public Destination HighDestination { get; }

        public override bool Equals(object obj)
        {
            var other = obj as BotInstruction;
            return other != null && this.Equals(other);
        }

        protected bool Equals(BotInstruction other)
        {
            return Bot == other.Bot && LowDestination.Equals(other.LowDestination) && HighDestination.Equals(other.HighDestination);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Bot;
                hashCode = (hashCode*397) ^ LowDestination.GetHashCode();
                hashCode = (hashCode*397) ^ HighDestination.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"bot {this.Bot} gives low to {this.LowDestination} and high to {this.HighDestination}";
        }
    }
}
