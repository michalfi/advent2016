using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent2016.Bunny.Comms
{
    public class RepeatedMessageCleaner
    {
        public RepeatedMessageCleaner(Protocol protocol)
        {
            this.ProtocolModifier = protocol == Protocol.Simple ? 1 : -1;
        }

        private int ProtocolModifier { get; }

        public enum Protocol
        {
            Simple,
            Modified
        }

        public string CleanMessage(string[] receivedMessageVersions)
        {
            Debug.Assert(receivedMessageVersions.Select(msg => msg.Length).Distinct().Count() == 1,
                "All message versions must have the same length");
            Debug.Assert(receivedMessageVersions.Length > 0);

            return string.Join("",
                Enumerable.Range(0, receivedMessageVersions[0].Length)
                    .Select(
                        i =>
                            receivedMessageVersions.Select(msg => msg[i])
                                .GroupBy(c => c)
                                .OrderByDescending(g => this.ProtocolModifier * g.Count())
                                .First()
                                .Key));
        }
    }
}
