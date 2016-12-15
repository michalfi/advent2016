using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.Assembunny
{
    public class RegisterMap
    {
        public RegisterMap(IEnumerable<char> registerCodes)
        {
            this.RegisterIndex = registerCodes.Select((code, index) => new {code, index})
                .ToDictionary(x => x.code, x => x.index);
        }

        public Dictionary<char, int> RegisterIndex { get; }
    }
}
