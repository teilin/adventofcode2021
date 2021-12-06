namespace adventofcode.Day02;

public sealed class Dive : ISolver
{
    public async ValueTask ExecutePart1(string inputFilePath)
    {
        var horizontakPosition = 0;
        var divePosition = 0;
        var instructions = (await File.ReadAllLinesAsync(inputFilePath, System.Text.Encoding.UTF8))
            .Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(s => (s[0],int.Parse(s[1])))
            .ToList();
        foreach(var instruction in instructions)
        {
            var steps = instruction.Item2;
            switch(instruction.Item1)
            {
                case "forward":
                    horizontakPosition += steps;
                    break;
                case "down":
                    divePosition += steps;
                    break;
                case "up":
                    divePosition -= steps;
                    break;
                default:
                    break;
            }
        }
        Console.WriteLine($"Product of horizontak and dive; {horizontakPosition*divePosition}");
    }

    public async ValueTask ExecutePart2(string inputFilePath)
    {
        var horizontakPosition = 0;
        var divePosition = 0;
        var aim = 0;
        var instructions = (await File.ReadAllLinesAsync(inputFilePath, System.Text.Encoding.UTF8))
            .Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(s => (s[0],int.Parse(s[1])))
            .ToList();
        foreach(var instruction in instructions)
        {
            var steps = instruction.Item2;
            switch(instruction.Item1)
            {
                case "forward":
                    horizontakPosition += steps;
                    divePosition += aim*steps;
                    break;
                case "down":
                    aim += steps;
                    break;
                case "up":
                    aim -= steps;
                    break;
                default:
                    break;
            }
        }
        Console.WriteLine($"Product of horizontak and dive; {horizontakPosition*divePosition}");
    }

    public bool ShouldExecute(int day)
    {
        return day == 2;
    }
}