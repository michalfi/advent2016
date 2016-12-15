namespace Advent2016.Bunny.Assembunny
{
    public class Dec : IInstruction
    {
        public Dec(Register register)
        {
            Register = register;
        }

        public Register Register { get; }

        public override string ToString()
        {
            return $"dec {Register}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Dec;
            return other != null && this.Equals(other);
        }

        public bool Equals(Dec other)
        {
            return Register == other.Register;
        }

        public override int GetHashCode()
        {
            return Register.GetHashCode();
        }
    }
}
