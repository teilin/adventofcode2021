using System.Collections;

namespace adventofcode.Day10;

public sealed class SyntaxScoring : ISolver
{
    private char[][] NavigationSystem = new char[0][];
    private IList<char[]> Incomplete = new List<char[]>();
    private readonly IDictionary<char,int> CharackterScore = new Dictionary<char,int>
    {
        {')', 3},
        {']', 57},
        {'}',1197},
        {'>',25137}
    };
    private readonly IDictionary<char,char> ValidPairs = new Dictionary<char,char>()
    {
        {')','('},
        {']','['},
        {'}','{'},
        {'>','<'}
    };

    private readonly IDictionary<char,char> MapOpenChar = new Dictionary<char,char>()
    {
        {'(',')'},
        {'[',']'},
        {'{','}'},
        {'<','>'}
    };

    public async ValueTask ExecutePart1(string inputFilePath)
    {
        NavigationSystem = (await File.ReadAllLinesAsync(inputFilePath)).Select(s => s.ToCharArray().ToArray()).ToArray();

        var syntaxErrorScore = new Dictionary<char,int>();
        var isCorrupted = false;
        foreach(var line in NavigationSystem)
        {
            var stack = new Stack<char>();
            foreach(var c in line)
            {
                if(ValidPairs.Values.ToArray().Contains(c)) stack.Push(c);
                else
                {
                    var prevOpening = stack.Pop();
                    if(ValidPairs.ContainsKey(c) && prevOpening != ValidPairs[c])
                    {
                        isCorrupted = true;
                        if(!syntaxErrorScore.ContainsKey(c)) syntaxErrorScore.Add(c,1);
                        else syntaxErrorScore[c]++;
                    }
                }
            }
            if(!isCorrupted) Incomplete.Add(line);
        }
        var sum = 0;
        foreach(KeyValuePair<char,int> r in syntaxErrorScore)
        {
            sum += r.Value * CharackterScore[r.Key];
        }
        Console.WriteLine(sum);
    }

    public ValueTask ExecutePart2(string inputFilePath)
    {
        var scores = new List<long>();
        var stack = new Stack<char>();
        var points = new Dictionary<char, long>
        {
            {')',1},
            {']',2},
            {'}',3},
            {'>',4}
        };

        foreach(var l in Incomplete)
        {
            var adding = new List<char>();
            foreach(var c in l)
            {
                if(ValidPairs.Values.ToArray().Contains(c)) stack.Push(c);
                else
                {
                    stack.Pop();
                }
            }
            while(stack.Count>0)
            {
                var t = stack.Pop();
                if(MapOpenChar.ContainsKey(t))
                {
                    adding.Add(MapOpenChar[t]);
                }
            }
            long score = 0;
            adding.ForEach(c => { score = score*5 + points[c]; });
            scores.Add(score);
        }
        scores = scores.OrderBy(o => o).ToList();
        var middle = Convert.ToInt32(Math.Floor(scores.Count()/2.0));
        Console.WriteLine(scores.ElementAt(middle));
        return ValueTask.CompletedTask;
    }

    public bool ShouldExecute(int day)
    {
        return day==10;
    }
}