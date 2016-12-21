using System;
using System.Linq;
using Advent2016.Bunny.Comms;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Bunny.UnitTests.Comms
{
    public class NletFinderTests
    {
        [Theory]
        [InlineData(3, '7', "354a84d667773e4")]
        [InlineData(5, '7', "a354a84d6677777")]
        [InlineData(3, 'd', "ddd777")]
        public void NletIsFoundWhenPresent(int n, char? c, string data)
        {
            var finder = new NletFinder(n);
            Assert.Equal(c, finder.Find(data));
        }

        [Theory]
        [InlineData(3, "")]
        [InlineData(3, "3544148e65ddad488")]
        [InlineData(5, "3544448e65ddad48888aaaa")]
        public void NullIsReturnedWhenNoNletIsPresent(int n, string data)
        {
            var finder = new NletFinder(n);
            Assert.Null(finder.Find(data));
        }

        [Property]
        public Property TripletIsPresentWhenTripletIsFound()
        {
            var chars = Enumerable.Range('0', 10).Concat(Enumerable.Range('a', 6)).Select(c => (char) c);
            var dataGen = Gen.Elements(chars).ArrayOf().Select(cs => new string(cs));
            var finder = new NletFinder(3);
            var findings = dataGen.Select(data => new {data, found = finder.Find(data)}).Where(f => f.found.HasValue);
            return Prop.ForAll(findings.ToArbitrary(),
                finding => finding.data.Contains(string.Join("", Enumerable.Repeat(finding.found.Value, 3))));
        }
    }
}
