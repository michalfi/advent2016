using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Comms
{
    public class NletFinder
    {
        public NletFinder(int n)
        {
            Debug.Assert(n > 1);
            this.NletRe = new Regex($"([a-f0-9])\\1{{{n - 1},}}");
        }

        private Regex NletRe { get; }

        public char? Find(string data)
        {
            var m = this.NletRe.Matches(data).Cast<Match>().FirstOrDefault();
            return m?.Groups[0].Value[0];
        }
    }
}
