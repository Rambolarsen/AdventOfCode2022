using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace AdventOfCode.Days
{
    public class Day04 : BaseDay
    {
        private readonly ICollection<(IEnumerable<int> FirstAssignments, IEnumerable<int> SecondAssignments)> _input;

        public Day04()
        {
            _input = ParseInput().ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            var countContains = 0;
            foreach (var input in _input)
            {
                if (input.FirstAssignments.All(x => input.SecondAssignments.Contains(x)) || input.SecondAssignments.All(x => input.FirstAssignments.Contains(x)))
                {
                    countContains++;
                }
            }
            return new(countContains.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var countContains = 0;
            foreach (var input in _input)
            {
                if (input.FirstAssignments.Any(x => input.SecondAssignments.Contains(x)) || input.SecondAssignments.Any(x => input.FirstAssignments.Contains(x)))
                {
                    countContains++;
                }
            }
            return new(countContains.ToString());
        }

        private IEnumerable<(IEnumerable<int> FirstAssignments, IEnumerable<int> SecondAssignments)> ParseInput()
        {
            var file = new ParsedFile(InputFilePath);
            while (!file.Empty)
            {
                var line = file.NextLine().ToSingleString();
                var splitAssignments = line.Split(',');
                var firstAssignments = GetAssignmentRange(splitAssignments[0]);
                var secondAssignments = GetAssignmentRange(splitAssignments[1]);

                yield return(firstAssignments, secondAssignments);
            }
        }

        private static IEnumerable<int> GetAssignmentRange(string splitAssignment)
        {
            var firstRange = splitAssignment.Split('-');
            var lowNumber = int.Parse(firstRange[0]);
            var highNumber = int.Parse(firstRange[1]);

            for (int i = lowNumber; i <= highNumber; i++)
                yield return i;           
        }
    }
}
