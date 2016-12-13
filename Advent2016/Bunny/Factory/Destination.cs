namespace Advent2016.Bunny.Factory
{
    public struct Destination
    {
        public Destination(DestinationKind kind, int id)
        {
            Kind = kind;
            Id = id;
        }

        public DestinationKind Kind { get; }

        public int Id { get; }

        public override string ToString()
        {
            return $"{this.Kind} {this.Id}";
        }
    }
}
