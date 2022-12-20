using AdventOfCode.Days;
using FluentAssertions;

namespace AdventOfCode.Tests
{
    public class Day07Tests
    {
        public Day07Tests() { }

        [Fact]
        public void Test()
        {
            var manager = new DirectoryManager();

            manager.RunCommand("$ cd /");
            manager.RunCommand("dir plws");
            manager.RunCommand("$ cd plws");
            manager.RunCommand("92461 nbvnzg");
            var directory = manager.GetDirectoriesLessThan(100000);
            manager.GetSizeForDirectory(directory.Last()).Should().Be(92461);
        }
    }
}
