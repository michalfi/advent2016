using System.Linq;

namespace Advent2016.Bunny.SecurityDoor
{
    public class PositionalCracker : PasswordCracker
    {
        public override string Crack(string doorId)
        {
            int index = 0;
            var password = new char[PasswordLength];
            var positionsFound = new bool[PasswordLength];

            while (positionsFound.Any(p => !p))
            {
                string hash = this.NextSignificant(doorId, ref index);
                int position;
                if (int.TryParse(hash.Substring(5, 1), out position) && position < PasswordLength &&
                    !positionsFound[position])
                {
                    password[position] = hash[6];
                    positionsFound[position] = true;
                }
            }
            return string.Join("", password).ToLowerInvariant();
        }

        private string NextSignificant(string doorId, ref int index)
        {
            while (true)
            {
                string hashStart = this.HashStart(doorId + index.ToString());
                index += 1;
                if (hashStart.StartsWith(SignificantHashStart))
                    return hashStart;
            }
        }
    }
}
