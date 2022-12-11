namespace AdventOfCode.Days
{
    public class Day01 : BaseDay
    {
        private readonly List<int> _input;

        public Day01()
        {
            _input = ParsedFile.ReadAllGroupsOfLines<int>(InputFilePath)
                .ConvertAll(group => group.Sum());
        }

        public override ValueTask<string> Solve_1() => new($"{_input.Max()}");

        public override ValueTask<string> Solve_2() => new($"{_input.OrderDescending().Take(3).Sum()}");
    }
}
