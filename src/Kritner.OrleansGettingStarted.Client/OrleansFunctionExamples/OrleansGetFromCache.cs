using System;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Kritner.OrleansGettingStarted.Client.Helpers;
using Orleans;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples;

public class OrleansGetFromCache : IOrleansFunction
{
    public string Description => "Gets a value from a cache with a grain per cache key";
    public async Task PerformFunction(IClusterClient clusterClient)
    {
        Console.WriteLine("Enter a name for a cache, one will be created or retrieved:");
        var cacheName = Console.ReadLine();
        
        var grain = clusterClient.GetGrain<IOrleansCache<string>>(cacheName);
        
        ConsoleHelpers.LineSeparator();
        
        Console.WriteLine($"Enter a key to retrieve the value from within the {grain.GetPrimaryKeyString()} grain.");
        var key = Console.ReadLine();

        var result = await grain.Get(key);

        Console.WriteLine(result is null
            ? $"No result found for key {key} or value was null for the provided key."
            : $"The value of key {key} was:{Environment.NewLine}{result}");

        ConsoleHelpers.ReturnToMenu();
    }
}
