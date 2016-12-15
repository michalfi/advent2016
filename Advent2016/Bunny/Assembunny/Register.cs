namespace Advent2016.Bunny.Assembunny
{
    public class Register : ISource
    {
        public Register(char name, int index)
        {
            Name = name;
            Index = index;
        }

        public char Name { get; }

        public int Index { get; }

        public override string ToString()
        {
            return Name.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Register;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Register other)
        {
            return Name == other.Name && Index == other.Index;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode()*397) ^ Index;
            }
        }
    }
}
