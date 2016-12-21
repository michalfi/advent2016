using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Advent2016.Bunny.Comms
{
    public class DataGenerator
    {
        public DataGenerator(string salt, int stretchCount = 0)
        {
            this.Salt = salt;
            this.StretchCount = stretchCount;
            this.Hasher = MD5.Create();
        }

        private string Salt { get; }

        private int StretchCount { get; }

        private MD5 Hasher { get; }

        public string Get(int index)
        {
            var data = $"{Salt}{index}";
            for (int i = 0; i <= StretchCount; i++)
            {
                var hash = this.Hasher.ComputeHash(Encoding.ASCII.GetBytes(data));
                data = string.Join("", hash.Select(b => b.ToString("x2")));
            }
            return data;
        }
    }
}
