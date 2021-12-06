namespace adventofcode.Day03;

public sealed class BinaryDiagnostic : ISolver
{
    public async ValueTask ExecutePart1(string inputFilePath)
    {
        var gammaRate = 0;
        var epsilonRate = 0;
        var diagnosticReport = (await File.ReadAllLinesAsync(inputFilePath))
            .Select(s => s.ToCharArray())
            .Select(s => s.Select(b => int.Parse(b.ToString())));

        var gammaRateArray = new List<int>();
        var epsilonRateArray = new List<int>();
        for(var i=0;i<diagnosticReport.ElementAt(0).Count();i++)
        {
            var list = new List<int>();
            for(var j=0;j<diagnosticReport.Count();j++)
            {
                list.Add(diagnosticReport.ElementAt(j).ElementAt(i));
            }
            var mostSignificantBit = MostSignificantBit(list.ToArray());
            var leastSignificantBit = LeastSignificantBit(list.ToArray());
            gammaRateArray.Add(mostSignificantBit);
            epsilonRateArray.Add(leastSignificantBit);
        }
        gammaRate = Convert.ToInt32(String.Join("", gammaRateArray.ConvertAll(i => i.ToString()).ToArray()), 2);
        epsilonRate = Convert.ToInt32(String.Join("", epsilonRateArray.ConvertAll(i => i.ToString()).ToArray()), 2);
        Console.WriteLine(gammaRate*epsilonRate);
    }

    public async ValueTask ExecutePart2(string inputFilePath)
    {
        var oxygenGeneratorRateing = 0;
        var co2ScrubberRating = 0;
        var diagnosticReport = (await File.ReadAllLinesAsync(inputFilePath))
            .Select(s => s.ToCharArray())
            .Select(s => s.Select(b => int.Parse(b.ToString())).ToArray()).ToArray();

        var oxygenGeneratorArray = Reduce(diagnosticReport, MostSignificantBit,0)[0];
        var co2ScrubberArray = Reduce(diagnosticReport, LeastSignificantBit,0)[0];

        oxygenGeneratorRateing = Convert.ToInt32(String.Join("", oxygenGeneratorArray.ToList().ConvertAll(i => i.ToString()).ToArray()), 2);
        co2ScrubberRating = Convert.ToInt32(String.Join("", co2ScrubberArray.ToList().ConvertAll(i => i.ToString()).ToArray()), 2);

        Console.WriteLine(oxygenGeneratorRateing*co2ScrubberRating);
    }

    public bool ShouldExecute(int day)
    {
        return day == 3;
    }

    private int[][] Reduce(int[][] array, Func<int[],int> f, int columnIndex)
    {
        if(array.Length == 1)
        {
            return array;
        }
        if(columnIndex >= array[0].Length) throw new Exception("No solution");
        var number = new int[array.Length];
        for(var rowIndex=0;rowIndex<array.Length;rowIndex++)
        {
            number[rowIndex] = array[rowIndex][columnIndex];
        }
        var leadNumber = f.Invoke(number);
        var newSet = new List<int[]>();
        foreach(var set in array)
        {
            if(set[columnIndex] == leadNumber)
            {
                newSet.Add(set);
            }
        }
        return Reduce(newSet.ToArray(), f, columnIndex+1);
    }

    private int MostSignificantBit(int[] array)
    {
        var num1 = array.Where(w => w == 1).Count();
        var num0 = array.Where(w => w == 0).Count();
        if(num1==num0) return 1;
        else return num1 > num0 ? 1 : 0;
    }

    private int LeastSignificantBit(int[] array)
    {
        var num1 = array.Where(w => w == 1).Count();
        var num0 = array.Where(w => w == 0).Count();
        if(num1==num0) return 0;
        else return num1 > num0 ? 0 : 1;
    }
}