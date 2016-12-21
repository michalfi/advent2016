using System.Linq;

namespace Advent2016.Bunny.DiskWiping
{
    public class Checksum
    {
        public Checksum(string data)
        {
            while (data.Length%2 == 0)
            {
                int len = data.Length/2;
                data = new string(Enumerable.Range(0, len).Select(i => data[i*2] == data[i*2 + 1] ? '1' : '0').ToArray());
            }
            Value = data;
        }

        public string Value { get; }
    }
}
