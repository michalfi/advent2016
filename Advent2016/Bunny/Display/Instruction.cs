namespace Advent2016.Bunny.Display
{
    public class Instruction
    {
        public Instruction(Operation op, byte paramA, byte paramB)
        {
            Op = op;
            ParamA = paramA;
            ParamB = paramB;
        }

        public enum Operation { Rectangle, RowRotation, ColRotation }

        public Operation Op { get; }

        public byte ParamA { get; }

        public byte ParamB { get; }

        public override bool Equals(object obj)
        {
            var other = obj as Instruction;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Instruction other)
        {
            return Op == other.Op && ParamA == other.ParamA && ParamB == other.ParamB;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Op;
                hashCode = (hashCode*397) ^ ParamA;
                hashCode = (hashCode*397) ^ ParamB;
                return hashCode;
            }
        }
    }
}
