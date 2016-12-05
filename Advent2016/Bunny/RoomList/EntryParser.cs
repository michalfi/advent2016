using System.Text.RegularExpressions;

namespace Advent2016.Bunny.RoomList
{
    public class EntryParser
    {
        private static Regex entryRe = new Regex(@"([-a-z]+)-([0-9]+)\[([a-z]+)\]", RegexOptions.Compiled);

        public Entry Parse(string listEntry)
        {
            var matchGroups = entryRe.Match(listEntry).Groups;
            return new Entry(matchGroups[1].Value, int.Parse(matchGroups[2].Value), matchGroups[3].Value);
        }
    }
}
