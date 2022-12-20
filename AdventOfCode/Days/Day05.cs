using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Days
{
    public class Day05 : BaseDay
    {

        private Stack<string>[] _stacks;
        private List<(int Containers,int From,int To)> _commands;

        public Day05()
        {
            (_stacks, _commands) = ParseInput();
        }

        private (Stack<string>[] Stacks, List<(int Container, int From, int To)> Commands) ParseInput()
        {
            var stacks = new Stack<string>[]{
                    new Stack<string>(),
                    new Stack<string>(),
                    new Stack<string>(),
                    new Stack<string>(),
                    new Stack<string>(),
                    new Stack<string>(),
                    new Stack<string>(),
                    new Stack<string>(),
                    new Stack<string>()
                };


            var commands = new List<(int Container, int From, int To)>();

            using (TextReader rdr = new StreamReader(InputFilePath))
            {
                string? line;
                var stacklines = new List<List<string>>();
                var i = 0;
                while ((line = rdr.ReadLine()) != null)
                {
                    if(i < 8)
                    {
                        var splitLine = line.Split(new[] {"    "}, StringSplitOptions.None);
                        var stackLine = new List<string>();
                        foreach (var item in splitLine)
                        {
                            if (!string.IsNullOrWhiteSpace(item))
                            {
                                var letters = item.Split(" ");
                                foreach (var letter in letters)
                                {
                                    stackLine.Add(letter);
                                }
                            }
                            else
                            {
                                stackLine.Add(item);
                            }
                        }
                        stacklines.Add(stackLine);
                    }

                    if(i > 9)
                    {
                        var commandsInput = line.Split(' ');
                        var command = (int.Parse(commandsInput[1]), int.Parse(commandsInput[3]), int.Parse(commandsInput[5]));
                        commands.Add(command);
                    }

                    i++;
                }
                stacklines.Reverse();
                foreach (var stackline in stacklines)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (!string.IsNullOrWhiteSpace(stackline[j]))
                            stacks[j].Push(stackline[j]);
                    }
                }
            }
            return (stacks, commands);

        }

        public override ValueTask<string> Solve_1()
        {
            foreach (var command in _commands)
            {
                for (int i = 0; i < command.Containers; i++)
                {
                    var container = _stacks[command.From-1].Pop();
                    _stacks[command.To-1].Push(container);
                }
            }
            var solutionCollection = new List<string>();
            foreach (var stack in _stacks)
                solutionCollection.Add(stack.First());
            
            var solution = string.Join("", solutionCollection);
            solution = solution.Replace("[", "");
            solution = solution.Replace("]", "");
            return new(solution);
        }

        public override ValueTask<string> Solve_2()
        {
            (_stacks, _commands) = ParseInput();
            foreach (var command in _commands)
            {
                var containersToMove = new List<string>();
                for (int i = 0; i < command.Containers; i++)
                    containersToMove.Add(_stacks[command.From - 1].Pop());

                containersToMove.Reverse();

                foreach (var containerToMove in containersToMove)
                    _stacks[command.To - 1].Push(containerToMove);
            }
            var solutionCollection = new List<string>();
            foreach (var stack in _stacks)
                solutionCollection.Add(stack.First());

            var solution = string.Join("", solutionCollection);
            solution = solution.Replace("[", "");
            solution = solution.Replace("]", "");
            return new(solution);
        }
    }
}
