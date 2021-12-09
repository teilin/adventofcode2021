namespace adventofcode.Day07;

public sealed class TreacheryWhales : ISolver
{
    private int[] _initHorizontalPositions = new int[0];

    public async ValueTask ExecutePart1(string inputFilePath)
    {
        _initHorizontalPositions = (await File.ReadAllTextAsync(inputFilePath))
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.Parse(s))
            .ToArray();

        var horizontalPosition = _initHorizontalPositions.Min();
        var minFuelCost = 0;
        var fuelCost = new List<int>();

        for(var i=_initHorizontalPositions.Min();i<=_initHorizontalPositions.Max();i++)
        {
            foreach(var h in _initHorizontalPositions)
            {
                fuelCost.Add(Math.Abs(h-i));
            }
            var cost = fuelCost.Sum();
            if(cost < minFuelCost || minFuelCost == 0)
            {
                minFuelCost = cost;
                horizontalPosition = i;
            }
            fuelCost.Clear();
        }
        Console.WriteLine(minFuelCost);
    }

    public ValueTask ExecutePart2(string inputFilePath)
    {
        var horizontalPosition = _initHorizontalPositions.Min();
        var minFuelCost = 0;
        var fuelCost = new List<int>();

        for(var i=_initHorizontalPositions.Min();i<=_initHorizontalPositions.Max();i++)
        {
            foreach(var h in _initHorizontalPositions)
            {
                fuelCost.Add(Enumerable.Range(1,Math.Abs(h-i)).Sum());
            }
            var cost = fuelCost.Sum();
            if(cost < minFuelCost || minFuelCost == 0)
            {
                minFuelCost = cost;
                horizontalPosition = i;
            }
            fuelCost.Clear();
        }
        Console.WriteLine(minFuelCost);
        return ValueTask.CompletedTask;
    }

    public bool ShouldExecute(int day)
    {
        return day == 7;
    }
}