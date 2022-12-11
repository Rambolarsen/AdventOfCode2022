namespace AdventOfCode.Days
{
    public class Day02 : BaseDay
    {
        private readonly ICollection<(Gesture Opponent, Gesture Me)> _input;
        private readonly ICollection<(Gesture Opponent, GameState GameState)> _inputV2;

        private enum Gesture { Rock, Paper, Scissors }
        private enum GameState { Loss, Draw, Win }


        public Day02()
        {
            _input = ParseInput().ToList();
            _inputV2 = ParseInputV2().ToList();
        }

        public override ValueTask<string> Solve_1() 
        {
            var totalScore = 0;
            foreach (var battle in _input)
            {
                var shapeScore = GetShapeScore(battle.Me);
                var gameScore = GetGameScore(battle);
                totalScore += shapeScore + gameScore;
            }
            return new(totalScore.ToString());
        }

        public override ValueTask<string> Solve_2() 
        {
            var totalScore = 0;
            foreach (var battle in _inputV2)
            {
                var shapeScore = GetShapeScore(battle);
                var gameScore = GetGameScore(battle.GameState);
                totalScore += shapeScore + gameScore;
            }
            return new(totalScore.ToString());
        }
         
        private static int GetGameScore((Gesture Opponent, Gesture Me) battle)
        {
            var gamestate = new GameState();
            switch (battle.Opponent)
            {
                case Gesture.Rock:
                    switch (battle.Me)
                    {
                        case Gesture.Rock:
                            gamestate = GameState.Draw;
                            break;
                        case Gesture.Paper:
                            gamestate = GameState.Win;
                            break;
                        case Gesture.Scissors:
                            gamestate = GameState.Loss;
                            break;
                        default:
                            break;
                    }
                    break;
                case Gesture.Paper:
                    switch (battle.Me)
                    {
                        case Gesture.Rock:
                            gamestate = GameState.Loss;
                            break;
                        case Gesture.Paper:
                            gamestate = GameState.Draw;
                            break;
                        case Gesture.Scissors:
                            gamestate = GameState.Win;
                            break;
                        default:
                            break;
                    }
                    break;
                case Gesture.Scissors:
                    switch (battle.Me)
                    {
                        case Gesture.Rock:
                            gamestate = GameState.Win;
                            break;
                        case Gesture.Paper:
                            gamestate = GameState.Loss;
                            break;
                        case Gesture.Scissors:
                            gamestate = GameState.Draw;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return GetGameScore(gamestate);
        }

        private static int GetGameScore(GameState gamestate)
        {
            return gamestate switch
            {
                GameState.Loss => 0,
                GameState.Draw => 3,
                GameState.Win => 6,
                _ => throw new()
            };
        }

        private static int GetShapeScore(Gesture myGesture)
        {
            int shapeScore = 0;
            switch (myGesture)
            {
                case Gesture.Rock:
                    shapeScore = 1;
                    break;
                case Gesture.Paper:
                    shapeScore = 2;
                    break;
                case Gesture.Scissors:
                    shapeScore = 3;
                    break;
                default:
                    break;
            };

            return shapeScore;
        }
        
        private static int GetShapeScore ((Gesture Opponent, GameState GameState) battle)
        {
            var gesture = new Gesture();
            switch (battle.Opponent)
            {
                case Gesture.Rock:
                    switch (battle.GameState)
                    {
                        case GameState.Loss:
                            gesture = Gesture.Scissors;
                            break;
                        case GameState.Draw:
                            gesture = Gesture.Rock;
                            break;
                        case GameState.Win:
                            gesture = Gesture.Paper;
                            break;
                        default:
                            break;
                    }
                    break;
                case Gesture.Paper:
                    switch (battle.GameState)
                    {
                        case GameState.Loss:
                            gesture = Gesture.Rock;
                            break;
                        case GameState.Draw:
                            gesture = Gesture.Paper;
                            break;
                        case GameState.Win:
                            gesture = Gesture.Scissors;
                            break;
                        default:
                            break;
                    }
                    break;
                case Gesture.Scissors:
                    switch (battle.GameState)
                    {
                        case GameState.Loss:
                            gesture = Gesture.Paper;
                            break;
                        case GameState.Draw:
                            gesture = Gesture.Scissors;
                            break;
                        case GameState.Win:
                            gesture = Gesture.Rock;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return GetShapeScore(gesture);
        }

        private IEnumerable<(Gesture, Gesture)> ParseInput()
        {
            var file = new ParsedFile(InputFilePath);

            while (!file.Empty)
            {
                var line = file.NextLine().ToSingleString();

                var opponent = line[0] switch
                {
                    'A' => Gesture.Rock,
                    'B' => Gesture.Paper,
                    'C' => Gesture.Scissors,
                    _ => throw new()
                };
                var me = line[2] switch
                {
                    'X' => Gesture.Rock,
                    'Y' => Gesture.Paper,
                    'Z' => Gesture.Scissors,
                    _ => throw new()
                };

                yield return (opponent, me);
            }
        }

        private IEnumerable<(Gesture, GameState)> ParseInputV2()
        {
            var file = new ParsedFile(InputFilePath);

            while (!file.Empty)
            {
                var line = file.NextLine().ToSingleString();

                var opponent = line[0] switch
                {
                    'A' => Gesture.Rock,
                    'B' => Gesture.Paper,
                    'C' => Gesture.Scissors,
                    _ => throw new()
                };
                var me = line[2] switch
                {
                    'X' => GameState.Loss,
                    'Y' => GameState.Draw,
                    'Z' => GameState.Win,
                    _ => throw new()
                };

                yield return (opponent, me);
            }
        }
    }
}
