using System.Linq;

namespace Advent2016.Bunny.Ip7
{
    public class Ip7Address
    {
        public Ip7Address(Ip7Sequence[] parts)
        {
            Parts = parts;
        }

        public Ip7Sequence[] Parts { get; }

        public override bool Equals(object obj)
        {
            var other = obj as Ip7Address;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Ip7Address other)
        {
            return this.Parts.SequenceEqual(other.Parts);
        }

        public override int GetHashCode()
        {
            return (Parts != null ? Parts.GetHashCode() : 0);
        }
    }
}
