using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Ip7
{
    public class AddressParser
    {
        private Regex SequenceRe { get; } = new Regex(@"\[?[a-z]+\]?");

        public Ip7Address Parse(string address)
        {
            var sequences = this.SequenceRe.Matches(address).Cast<Match>().Select(m => this.ParseSequence(m.Value));
            return new Ip7Address(sequences.ToArray());
        }

        private Ip7Sequence ParseSequence(string sequence)
        {
            bool isHypernet = sequence.StartsWith("[");
            string chars = isHypernet ? sequence.Substring(1, sequence.Length - 2) : sequence;
            return new Ip7Sequence(chars, isHypernet);
        }
    }
}
