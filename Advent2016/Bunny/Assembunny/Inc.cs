namespace Advent2016.Bunny.Assembunny
{
    public class Inc : IInstruction
    {
        public Inc(Register register)
        {
            Register = register;
        }

        public Register Register { get; }

        public override string ToString()
        {
            return $"inc {Register}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Inc;
            return other != null && this.Equals(other);
        }

        public bool Equals(Inc other)
        {
            return Register == other.Register;
        }

        public override int GetHashCode()
        {
            return Register.GetHashCode();
        }
    }
}
