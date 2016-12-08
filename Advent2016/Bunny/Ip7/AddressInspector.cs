using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Ip7
{
    public class AddressInspector
    {
        private Regex AbbaRe { get; } = new Regex(@"(.)(.)\2\1");

        private Regex AbaRe { get; } = new Regex(@"(?=((.)(.)\2))");

        public bool DetectTls(Ip7Address address)
        {
            bool abbaInNormal = false, abbaInHypernet = false;
            foreach (var seq in address.Parts)
            {
                if (seq.IsHypernet)
                    abbaInHypernet = abbaInHypernet || this.ContainsAbba(seq.Characters);
                else
                    abbaInNormal = abbaInNormal || this.ContainsAbba(seq.Characters);
            }
            return abbaInNormal && !abbaInHypernet;
        }

        private bool ContainsAbba(string sequence)
        {
            return this.AbbaRe.Matches(sequence).Cast<Match>().Any(match => match.Value[0] != match.Value[1]);
        }

        public bool DetectSsl(Ip7Address address)
        {
            var abas = address.Parts.Where(part => !part.IsHypernet).SelectMany(part => this.FindAbas(part.Characters));
            var babs = address.Parts.Where(part => part.IsHypernet).SelectMany(part => this.FindAbas(part.Characters));
            var desiredBabs = abas.Select(aba => $"{aba[1]}{aba[0]}{aba[1]}");
            return babs.Intersect(desiredBabs).Any();
        }

        private IEnumerable<string> FindAbas(string sequence)
        {
            return this.AbaRe.Matches(sequence).Cast<Match>().Select(match => match.Groups[1].Value).Where(s => s[0] != s[1]);
        }
    }
}
