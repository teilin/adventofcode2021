namespace adventofcode.Day01;

public sealed class SonarSweep : ISolver
{
    public async ValueTask ExecutePart1(string inputFilePath)
    {
        var sonardepths = (await File.ReadAllLinesAsync(inputFilePath)).Select(s => int.Parse(s)).ToArray();
        var numIncreases = 0;
        for(var i=1;i<sonardepths.Count();i++)
        {
            if(sonardepths[i] > sonardepths[i-1]) numIncreases++;
        }
        Console.WriteLine($"Number of increases: {numIncreases}");
    }

    public async ValueTask ExecutePart2(string inputFilePath)
    {
        var sonardepths = (await File.ReadAllLinesAsync(inputFilePath)).Select(s => int.Parse(s)).ToArray();
        var numIncreases = 0;
        var newDepth = new List<int>();
        for(var i=0; i<sonardepths.Length-2;i++)
        {
            newDepth.Add(sonardepths[i]+sonardepths[i+1]+sonardepths[i+2]);
        }
        for(var i=1;i<newDepth.Count();i++)
        {
            if(newDepth[i] > newDepth[i-1]) numIncreases++;
        }
        Console.WriteLine($"Number of increases: {numIncreases}");
    }

    public bool ShouldExecute(int day)
    {
        return day == 1;
    }
}