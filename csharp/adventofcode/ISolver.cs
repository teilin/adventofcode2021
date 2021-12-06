namespace adventofcode;

public interface ISolver
{
    ValueTask ExecutePart1(string inputFilePath);
    ValueTask ExecutePart2(string inputFilePath);
    bool ShouldExecute(int day);
}