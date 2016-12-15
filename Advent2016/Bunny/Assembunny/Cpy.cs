namespace Advent2016.Bunny.Assembunny
{
    public class Cpy : IInstruction
    {
        public Cpy(ISource source, Register target)
        {
            Source = source;
            Target = target;
        }

        public ISource Source { get; }

        public Register Target { get; }

        public override string ToString()
        {
            return $"cpy {Source} {Target}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cpy;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Cpy other)
        {
            return Source.Equals(other.Source) && Target.Equals(other.Target);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Source != null ? Source.GetHashCode() * 397 : 0) ^ (Target != null ? Target.GetHashCode() : 0);
            }
        }
    }
}
