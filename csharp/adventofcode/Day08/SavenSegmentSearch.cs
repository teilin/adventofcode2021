namespace adventofcode.Day08;

public sealed class SavenSegmentSearch : ISolver
{
    private string[][] _input = new string[0][];
    private int[] _numberSegmentsUsed = new int[10];
    private string[][] Segments = new string[0][];
    private string[][] Outputs = new string[0][];
    private int[] UniqueLengths = new int[] { 2, 3, 4, 7 }; // Number of segments for digit 1, 4, 7, or 8
    private readonly Dictionary<string, int> DigitMap = new Dictionary<string, int>()
    {
        { "012345", 0 },
        { "12", 1 },
        { "01346", 2 },
        { "01236", 3 },
        { "1256", 4 },
        { "02356", 5 },
        { "023456", 6 },
        { "012", 7 },
        { "0123456", 8 },
        { "012356", 9 }
    };

    public async ValueTask ExecutePart1(string inputFilePath)
    {
        _input = (await File.ReadAllLinesAsync(inputFilePath)).Select(s => s.Split(" | ", StringSplitOptions.RemoveEmptyEntries)).ToArray();
        Segments = _input.Select(s => s.First().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
        Outputs = _input.Select(s => s.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();


        var numInstances = Outputs.Sum(sum => sum.Count(c => UniqueLengths.Contains(c.Length)));

        Console.WriteLine(numInstances);
    }

    public ValueTask ExecutePart2(string inputFilePath)
    {
        var n =  Enumerable.Range(0, Segments.Length).Sum(s =>
        {
            var cm = GetMap(Segments[s]);
            return int.Parse(String.Join("",Outputs[s].Select(i => DigitMap[String.Join("",i.Select(c => cm[c]).OrderBy(c => c))])));
        });
        Console.WriteLine(n);
        return ValueTask.CompletedTask;
    }

    public bool ShouldExecute(int day)
    {
        return day == 8;
    }   

    private Dictionary<char,int> GetMap(string[] segments)
    {
        var (array, count) = (new char[7],0);
        return UniqueLengths.Select(l => segments.Single(s => s.Length == l)).SelectMany(p =>
            p.Length switch
            {
                2 => p.Select(c => new { key = array[count++] = c, value = segments.Count(s => s.Contains(c)) == 8 ? 1 : 2 }),
                3 => p.Where(c => !array.Contains(c)).Select(c => new { key = array[count++] = c, value = 0 }),
                4 => p.Where(c => !array.Contains(c)).Select(c => new { key = array[count++] = c, value = segments.Count(s => s.Contains(c)) == 6 ? 5 : 6 }),
                7 => p.Where(c => !array.Contains(c)).Select(c => new { key = array[count++] = c, value = segments.Count(s => s.Contains(c)) == 4 ? 4 : 3 }),
                _ => null
            }
        ).ToDictionary(v => v.key, v => v.value);
    }
}