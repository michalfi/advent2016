using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Advent2016.Bunny.SecurityDoor
{
    public class PasswordCracker
    {
        protected const int PasswordLength = 8;

        protected const string SignificantHashStart = "00000";

        protected MD5 Hasher { get; } = MD5.Create();

        public virtual string Crack(string doorId)
        {
            int index = 0;
            var password = new char[PasswordLength];
            for (int digit = 0; digit < PasswordLength; digit++)
                password[digit] = NextDigit(doorId, ref index);
            return string.Join("", password).ToLowerInvariant();
        }

        private char NextDigit(string doorId, ref int index)
        {
            while (true)
            {
                string hashStart = this.HashStart(doorId + index.ToString());
                index += 1;
                if (hashStart.StartsWith(SignificantHashStart))
                    return hashStart[5];
            }
        }

        protected string HashStart(string data)
        {
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = Hasher.ComputeHash(dataBytes);
            return string.Join("", hashBytes.Take(4).Select(x => x.ToString("X2")));
        }
    }
}
