using System.Linq;
using Advent2016.Bunny.Assembunny;

namespace Advent2016.Puzzle
{
    public class Day12InterpretInitialized : IPuzzle
    {
        public string Solve(string[] input)
        {
            var registers = new RegisterMap(new[] { 'a', 'b', 'c', 'd' });
            var parser = new InstructionParser(registers);
            var interpret = new Interpreter(registers);
            interpret.Registers[registers.RegisterIndex['c']] = 1;
            interpret.Execute(input.Select(i => parser.Parse(i)).ToArray());
            return interpret.Registers[0].ToString();
        }
    }
}
