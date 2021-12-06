namespace adventofcode.Day06;

public sealed class Lanternfish : ISolver
{
    private long[] _initFishes = new long[0];
    public async ValueTask ExecutePart1(string inputFilePath)
    {
        _initFishes = (await File.ReadAllTextAsync(inputFilePath))
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => long.Parse(s))
            .ToArray();
        
        var spawn = CalcSpawnFish(_initFishes, 80);
        Console.WriteLine(spawn.Sum());
    }

    public ValueTask ExecutePart2(string inputFilePath)
    {
        var spawn = CalcSpawnFish(_initFishes, 256);
        Console.WriteLine(spawn.Sum());
        return ValueTask.CompletedTask;
    }

    public bool ShouldExecute(int day)
    {
        return day == 6;
    }

    private long[] CalcSpawnFish(long[] init, int numberOfDays)
    {
        var spawn = new long[9];
        foreach(var i in init) spawn[i]++;
        for(var d=0;d<numberOfDays;d++)
        {
            var newborn = spawn[0];
            for(var i=1;i<spawn.Length;i++) spawn[i-1] = spawn[i];
            spawn[6] += newborn;
            spawn[8] = newborn;
        }
        return spawn;
    }
}