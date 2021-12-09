namespace adventofcode.Day09;

public sealed class SmokeBasin : ISolver
{
    private int[][] _heightMap = new int[0][];
    private readonly IDictionary<(int x,int y),int> _basins = new Dictionary<(int,int),int>();
    public async ValueTask ExecutePart1(string inputFilePath)
    {
        _heightMap = (await File.ReadAllLinesAsync(inputFilePath))
            .Select(s => s.ToArray())
            .Select(s => s.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

        var lowPoints = new List<int>();
        for(var x=0;x<_heightMap[0].Length;x++)
        {
            for(var y=0;y<_heightMap.Length;y++)
            {
                var surrounding = new List<int>();

                if (y - 1 >= 0) surrounding.Add(_heightMap[y - 1][x]);
                if (y + 1 < _heightMap.Count()) surrounding.Add(_heightMap[y + 1][x]);
                if (x - 1 >= 0) surrounding.Add(_heightMap[y][x - 1]);
                if (x + 1 < _heightMap[0].Length) surrounding.Add(_heightMap[y][x + 1]);

                var value = _heightMap[y][x];
                if (value < surrounding.Min())
                {
                    lowPoints.Add(value);
                    _basins[(x,y)] = 0;
                }
            }
        }
        var sumRiskPoints = lowPoints.Sum(s => s+1);
        Console.WriteLine(sumRiskPoints);
    }

    public async ValueTask ExecutePart2(string inputFilePath)
    {
        foreach(var basin in _basins.Keys)
        {
            await CalculateBasin(basin.x,basin.y,basin,new List<(int,int)>());
        }
        var product = 1;
        foreach(var size in _basins.Values.OrderByDescending(o => o).Take(3))
        {
            product *= size;
        }
        Console.WriteLine(product);
    }

    public bool ShouldExecute(int day)
    {
        return day==9;
    }

    private ValueTask CalculateBasin(int x, int y, (int x, int y) basin, IList<(int x, int y)> path)
    {
        if(path.Contains((x,y))) return ValueTask.CompletedTask;
        path.Add((x,y));
        var value = _heightMap[y][x];
        if(value == 9) return ValueTask.CompletedTask;
        _basins[basin]++;
        if(y-1 >= 0) CalculateBasin(x,y-1,basin,path);
        if(y+1 < _heightMap.Length) CalculateBasin(x,y+1,basin,path);
        if(x-1>=0) CalculateBasin(x-1,y,basin,path);
        if(x+1 < _heightMap[0].Length) CalculateBasin(x+1,y,basin,path);
        return ValueTask.CompletedTask;
    }
}