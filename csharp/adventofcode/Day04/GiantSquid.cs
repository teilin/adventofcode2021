namespace adventofcode.Day04;

public sealed class GiantSquid : ISolver
{
    private int[] _bingoNumbers = new int[0];
    private string[] _bingoInput = new string[0];
    private readonly IList<BingoResult> _results = new List<BingoResult>();

    public async ValueTask ExecutePart1(string inputFilePath)
    {
        _bingoInput = await File.ReadAllLinesAsync(inputFilePath);
        _bingoNumbers = _bingoInput[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

        var boards = ReadBoards();
        foreach(var board in boards)
        {
            var bingo = PlayBingo(board, _bingoNumbers, 0);
            _results.Add(new BingoResult(bingo.Item1, bingo.Item2));
        }
        var minRounds = _results.MinBy(m => m.NumberOfRounds);
        Console.WriteLine(CalculateScore(minRounds.Board, _bingoNumbers[minRounds.NumberOfRounds]));
    }

    public ValueTask ExecutePart2(string inputFilePath)
    {
        _results.Clear();
        var boards = ReadBoards();
        foreach(var board in boards)
        {
            var bingo = PlayBingo(board, _bingoNumbers, 0);
            _results.Add(new BingoResult(bingo.Item1, bingo.Item2));
        }
        var maxRounds = _results.MaxBy(m => m.NumberOfRounds);
        Console.WriteLine(CalculateScore(maxRounds.Board, _bingoNumbers[maxRounds.NumberOfRounds]));

        return ValueTask.CompletedTask;
    }

    public bool ShouldExecute(int day)
    {
        return day==4;
    }

    private IList<BingoBoard[][]> ReadBoards()
    {
        var boards = new List<BingoBoard[][]>();
        var board = new BingoBoard[5][];
        var bingoRow = 0;
        for(var i=1;i<_bingoInput.Length;i++)
        {
            if(!string.IsNullOrEmpty(_bingoInput[i]))
            {
                board[bingoRow] = _bingoInput[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).Select(s => new BingoBoard(s)).ToArray();
                bingoRow++;
            }
            else
            {
                if(board[0] != null) boards.Add(board);
                board = new BingoBoard[5][];
                bingoRow = 0;
            }
        }
        boards.Add(board);
        return boards;
    }

    private (BingoBoard[][],int) PlayBingo(BingoBoard[][] board, int[] numbers, int numRounds)
    {
        var head = numbers.First();
        var tail = numbers.Skip(1).ToArray();
        if(CheckBingo(board))
        {
            return (board,numRounds-1);
        }
        for(var c=0;c<board[0].Length;c++)
        {
            for(var r=0;r<board.Length;r++)
            {
                if(board[r][c].Number == head)
                {
                    board[r][c].IsMarked = true;
                }
            }
        }
        return PlayBingo(board, tail, numRounds+1);
    }

    private bool CheckBingo(BingoBoard[][] board)
    {
        foreach(var row in board)
        {
            var count = row.Where(w => w.IsMarked).Count();
            if(count == 5) return true;
        }
        for(var c=0;c<board[0].Length;c++)
        {
            var count = 0;
            for(var r=0;r<board.Length;r++)
            {
                if(board[r][c].IsMarked) count++;
            }
            if(count==5) return true;
        }
        return false;
    }

    private int CalculateScore(BingoBoard[][] board, int lastPickedNumber)
    {
        var sumOfUnmarked = 0;
        for(var r=0;r<board.Length;r++)
        {
            for(var c=0;c<board[r].Length;c++)
            {
                if(!board[r][c].IsMarked)
                {
                    sumOfUnmarked += board[r][c].Number;
                }
            }
        }
        return sumOfUnmarked*lastPickedNumber;
    }

    internal struct BingoBoard
    {
        public BingoBoard(int number)
        {
            Number = number;
        }
        public int Number { get; private set; }
        public bool IsMarked { get; set; } = false;
    }

    internal struct BingoResult
    {
        public BingoResult(BingoBoard[][] board, int numRounds)
        {
            Board = board;
            NumberOfRounds = numRounds;
        }

        public BingoBoard[][] Board { get; init; }
        public int NumberOfRounds { get; init; }
    }
}