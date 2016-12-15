namespace Advent2016.Bunny.Assembunny
{
    public class Jnz : IInstruction
    {
        public Jnz(ISource source, int count)
        {
            Source = source;
            Count = count;
        }

        public ISource Source { get; }

        public int Count { get; }

        public override string ToString()
        {
            return $"jnz {Source} {Count}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Jnz;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Jnz other)
        {
            return Source.Equals(other.Source) && Count == other.Count;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Source != null ? Source.GetHashCode() * 397 : 0) ^ Count;
            }
        }
    }
}
