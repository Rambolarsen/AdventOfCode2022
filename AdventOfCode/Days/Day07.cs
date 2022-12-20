using AoCHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Days
{
    public class Day07 : BaseDay
    {
        private ICollection<string> _input;

        public Day07()
        {
            _input = ParseInput().ToList();
        }

        private IEnumerable<string> ParseInput()
        {
            var file = new ParsedFile(InputFilePath);        
            while (!file.Empty)
            {
                yield return file.NextLine().ToSingleString();                
            }
        }

        public override ValueTask<string> Solve_1()
        {
            var manager = new DirectoryManager();
            foreach (var input in _input)
                manager.RunCommand(input);

            var directories = manager.GetDirectoriesLessThan(100000).OrderBy(x => x.GetTotalFileSizeForDirectory()).ToList();
            var sum = 0;
            foreach (var directory in directories) 
                sum += manager.GetSizeForDirectory(directory, true);
            
            return new(sum.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var manager = new DirectoryManager();
            foreach (var input in _input)
                manager.RunCommand(input);
                    
            return new(manager.GetSpaceToBeDeleted().ToString());
        }
    }

    public class Directory
    {
        public Directory()
        {
            ParentDirectory = null;
            CurrentLevel = 0;
            Name = "/";
            Path = "/";
        }


        public Directory(string directoryName, Directory parentDirectory)
        {
            Name = directoryName;
            ParentDirectory = parentDirectory;
            CurrentLevel = parentDirectory.CurrentLevel + 1;
            Path= parentDirectory.Path + "/" + Name;
        }

        public string Path { get; set; }

        public string Name { get; set; }

        public Directory? ParentDirectory { get; set; }

        public ICollection<(int Size, string Name)> Files { get; set; } = new HashSet<(int Size, string Name)> ();

        public int CurrentLevel { get; set; }

        public void AddFile(string command)
        {
            var fileInfo = command.Split(' ');
            Files.Add((int.Parse(fileInfo[0]), fileInfo[1]));
        }

        public int GetTotalFileSizeForDirectory() => Files.Sum(x => x.Size);

        public override string ToString()
        {
            return $"{nameof(CurrentLevel)}: {CurrentLevel}, " +
                $"{nameof(Name)}: {Name}, " +
                $"{nameof(Path)}: {Path}, " + 
                $"TotalFileSize: {GetTotalFileSizeForDirectory()}, " + 
                $"Parent: {ParentDirectory?.Name} - {ParentDirectory?.CurrentLevel} ";
        }
    }

    public class DirectoryManager
    {
        private Directory _baseDirectory;
        private Dictionary<string, Directory> _directories;
        private Directory _currentDirectory;
        private const string home = "$ cd /";
        private const string parent = "$ cd ..";
        public DirectoryManager()
        {
            _baseDirectory= new Directory();
            _directories = new Dictionary<string, Directory>
            {
                { _baseDirectory.Path, _baseDirectory }
            };
            _currentDirectory = _baseDirectory;
        }

        public void RunCommand(string command)
        {
            if (command.StartsWith("$"))
            {
                if (command == home)
                {
                    _currentDirectory = _baseDirectory;
                }
                else if (command == parent)
                {
                    _currentDirectory = _currentDirectory.ParentDirectory ?? _currentDirectory;
                }
                else if (command.Contains("cd"))
                {
                    var directoryName = command.Split(" ")[2];
                    _directories.TryGetValue(_currentDirectory.Path + "/" + directoryName, out var directory);
                    if (directory == null)
                    {
                        directory = new Directory(directoryName, _currentDirectory);
                        _directories.Add(directory.Path, directory);
                    }

                    _currentDirectory = directory;
                }
            }
            else if (command.StartsWith("dir")) { } //handled on cd
            else if (command.StartsWith("ls")) { } //ignored
            else
            {
                _currentDirectory.AddFile(command);
            }
        }

        public IEnumerable<Directory> GetDirectoriesLessThan(int maxSize)
        {
            foreach (var directory in _directories)
            {
                var size = GetSizeForDirectory(directory.Value, true);
                if (size <= maxSize && size > 0)
                    yield return directory.Value;
            }
        }

        public int GetSizeForDirectory(Directory directory) => 
            GetSizeForDirectory(directory, false);

        public int GetSizeForDirectory(Directory directory, bool includeChildDirectories)
        {
            var sum = directory.GetTotalFileSizeForDirectory();
            if (includeChildDirectories)
            {
                var childDirectories = _directories.Where(x => x.Value.ParentDirectory?.Path == directory.Path).ToList();
                foreach (var childDirectory in childDirectories)
                    sum += GetSizeForDirectory(childDirectory.Value, true);
            }
            return sum;
        }

        public int GetSpaceToBeDeleted()
        {
            var size = GetSizeForDirectory(_baseDirectory, true);
            var totalUsedSpace = 70000000 - size;
            var freeSpaceNeeeded = 30000000 - totalUsedSpace;
            return GetDirectoryToDelete(freeSpaceNeeeded);
        }

        private int GetDirectoryToDelete(int minSize)
        {
            var currentMinValue = int.MaxValue;
            Directory? directoryToDelete = null;
            foreach (var directory in _directories)
            {

                var size = GetSizeForDirectory(directory.Value, true);
                if (size >= minSize && size < currentMinValue)
                {
                    currentMinValue = size;
                    directoryToDelete = directory.Value;
                }
                   
            }

            return currentMinValue;
        }
    }
}
