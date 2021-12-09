using adventofcode.Day01;
using adventofcode.Day02;
using adventofcode.Day03;
using adventofcode.Day04;
using adventofcode.Day05;
using adventofcode.Day06;
using adventofcode.Day07;
using adventofcode.Day08;
using adventofcode.Day09;
using Microsoft.Extensions.DependencyInjection;

namespace adventofcode.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ISolver,SonarSweep>();
        services.AddScoped<ISolver,Dive>();
        services.AddScoped<ISolver,BinaryDiagnostic>();
        services.AddScoped<ISolver,GiantSquid>();
        services.AddScoped<ISolver,HydrothermalVenture>();
        services.AddScoped<ISolver,Lanternfish>();
        services.AddScoped<ISolver,TreacheryWhales>();
        services.AddScoped<ISolver,SavenSegmentSearch>();
        services.AddScoped<ISolver,SmokeBasin>();

        return services;
    }
}