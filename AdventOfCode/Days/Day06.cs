using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day06 : BaseDay
    {
        private char[] _input;

        public Day06()
        {
            _input = ParseInput().ToArray();
        }
        public override ValueTask<string> Solve_1()
        {
            return GetStartOfMarker(4);
        }

        private ValueTask<string> GetStartOfMarker(int uniqueCharacters)
        {
            for (int i = 0; i < _input.Length; i++)
            {
                var searcher = new List<char>();
                for (int j = i; j < _input.Length; j++)
                {
                    if (searcher.Contains(_input[j]))
                        break;

                    searcher.Add(_input[j]);
                    if (searcher.Count == uniqueCharacters)
                        return new((i + searcher.Count).ToString());
                }
            }
            return new("");
        }

        public override ValueTask<string> Solve_2()
        {
            return GetStartOfMarker(14);
        }

        private IEnumerable<char> ParseInput()
        {
            var file = new ParsedFile(InputFilePath);

            var input = Array.Empty<char>();
            while (!file.Empty)
            {
                var line = file.NextLine().ToSingleString();
                input = line.ToCharArray();
            }

            return input;
        }
    }
}
