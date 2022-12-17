using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace AdventOfCode.Days
{
    public class Day03 : BaseDay
    {
        private readonly ICollection<(IEnumerable<int> FirstCompartment, IEnumerable<int> SecondCompartment)> _input;

        public Day03()
        {
            _input = ParseInput().ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            var duplicates = new List<int>();
            foreach (var input in _input)
            {
                var current = new List<int>();
                foreach (var firstCompartment  in input.FirstCompartment)
                {
                    if (input.SecondCompartment.Contains(firstCompartment) && !current.Contains(firstCompartment))
                    {
                        duplicates.Add(firstCompartment);
                        current.Add(firstCompartment);
                    }                    
                }
            }
            return new(duplicates.Sum().ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var bagdeCollection = new List<int>();
            for (int i = 3; i <= _input.Count; i = i + 3)
            {
                var list = _input.Take(i).Skip(i-3).ToList();
                var firstRugsack = list[0].FirstCompartment.Concat(list[0].SecondCompartment).ToList();
                var secondRugsack = list[1].FirstCompartment.Concat(list[1].SecondCompartment).ToList();
                var thirdRugsack = list[2].FirstCompartment.Concat(list[2].SecondCompartment).ToList();

                foreach (var priority in firstRugsack)
                {
                    if(secondRugsack.Contains(priority) && thirdRugsack.Contains(priority))
                    {
                        bagdeCollection.Add(priority); 
                        break;
                    }
                }
            }
            return new(bagdeCollection.Sum().ToString());
        }

        private IEnumerable<(IEnumerable<int> FirstCompartment, IEnumerable<int> SecondCompartment)> ParseInput()
        {
            var file = new ParsedFile(InputFilePath);
            var priority = new Priorities();

            while (!file.Empty)
            {
                var line = file.NextLine().ToSingleString();
                var middle = line.Length / 2;
                var firstCompartment = new List<int>();
                for (int i = 0; i < middle; i++)
                {
                    var letter = line[i]; 
                    firstCompartment.Add(priority.GetPriority(letter));
                }
                var secondCompartment = new List<int>();
                for (int i = middle; i < line.Length; i++)
                {
                    var letter = line[i];
                    secondCompartment.Add(priority.GetPriority(letter));
                }
                yield return (firstCompartment, secondCompartment);
                
            }
        }

        public class Priorities
        {
            private Dictionary<char,int> Priority { get; set; }

            public Priorities() 
            {
                Priority = new Dictionary<char,int>();
                Priority.Add('a', 1);
                Priority.Add('b', 2);
                Priority.Add('c', 3);
                Priority.Add('d', 4);
                Priority.Add('e', 5);
                Priority.Add('f', 6);
                Priority.Add('g', 7);
                Priority.Add('h', 8);
                Priority.Add('i', 9);
                Priority.Add('j', 10);
                Priority.Add('k', 11);
                Priority.Add('l', 12);
                Priority.Add('m', 13);
                Priority.Add('n', 14);
                Priority.Add('o', 15);
                Priority.Add('p', 16);
                Priority.Add('q', 17);
                Priority.Add('r', 18);
                Priority.Add('s', 19);
                Priority.Add('t', 20);
                Priority.Add('u', 21);
                Priority.Add('v', 22);
                Priority.Add('w', 23);
                Priority.Add('x', 24);
                Priority.Add('y', 25);
                Priority.Add('z', 26);
                Priority.Add('A', 27);
                Priority.Add('B', 28);
                Priority.Add('C', 29);
                Priority.Add('D', 30);
                Priority.Add('E', 31);
                Priority.Add('F', 32);
                Priority.Add('G', 33);
                Priority.Add('H', 34);
                Priority.Add('I', 35);
                Priority.Add('J', 36);
                Priority.Add('K', 37);
                Priority.Add('L', 38);
                Priority.Add('M', 39);
                Priority.Add('N', 40);
                Priority.Add('O', 41);
                Priority.Add('P', 42);
                Priority.Add('Q', 43);
                Priority.Add('R', 44);
                Priority.Add('S', 45);
                Priority.Add('T', 46);
                Priority.Add('U', 47);
                Priority.Add('V', 48);
                Priority.Add('W', 49);
                Priority.Add('X', 50);
                Priority.Add('Y', 51);
                Priority.Add('Z', 52);

            }

            public int GetPriority(char @char) => 
                Priority.First(x => x.Key == @char).Value;
        } 
    }
}
