namespace Advent2016.Bunny.Ip7
{
    public class Ip7Sequence
    {
        public Ip7Sequence(string characters, bool isHypernet)
        {
            Characters = characters;
            IsHypernet = isHypernet;
        }

        public string Characters { get; }

        public bool IsHypernet { get; }

        public override bool Equals(object obj)
        {
            var other = obj as Ip7Sequence;
            return (other != null && this.Equals(other));
        }

        protected bool Equals(Ip7Sequence other)
        {
            return string.Equals(Characters, other.Characters) && IsHypernet == other.IsHypernet;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Characters?.GetHashCode() ?? 0)*397) ^ IsHypernet.GetHashCode();
            }
        }
    }
}
