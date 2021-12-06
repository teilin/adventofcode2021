using adventofcode.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace adventofcode;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();

        var inputFilePath = string.Empty;
        var puzzleDay = -1;
        var shouldRun = true;

        //Console.WriteLine(GreetWithDependencyInjection(host.Services));
        if(args.Length == 1)
        {
            inputFilePath = args[0];
            puzzleDay = DateTime.Today.Day;
        }
        else if(args.Length == 2)
        {
            inputFilePath = args[0];
            puzzleDay = int.Parse(args[1].AsSpan());
        }
        else
        {
            shouldRun = false;
            Console.WriteLine("Nothing to run");
        }

        if(shouldRun)
        {
            await ExecuteSolution(host.Services, inputFilePath, puzzleDay);
        }

        //return host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
                services.AddDependencies());
    }

    public static async ValueTask ExecuteSolution(IServiceProvider services, string inputFilePath, int puzzleDay)
    {
        using var serviceScope = services.CreateAsyncScope();
        var provider = serviceScope.ServiceProvider;
        var solvers = provider.GetServices<ISolver>();
        var solver = solvers.FirstOrDefault(w => w.ShouldExecute(puzzleDay));
        if(solver == null)
        {
            Console.WriteLine("No solution for given day");
        }
        else
        {
            await solver.ExecutePart1(inputFilePath);
            await solver.ExecutePart2(inputFilePath);
        }
    }
}