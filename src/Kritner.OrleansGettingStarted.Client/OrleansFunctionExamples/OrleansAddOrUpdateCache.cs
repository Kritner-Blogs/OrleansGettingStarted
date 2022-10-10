using System;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Kritner.OrleansGettingStarted.Client.Helpers;
using Orleans;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples;

public class OrleansAddOrUpdateCache : IOrleansFunction
{
    public string Description => "Adds a value to a cache.  The cache key and internal key/value pairs are all of type string keys for this naive implementation.";
    public async Task PerformFunction(IClusterClient clusterClient)
    {
        Console.WriteLine("Enter a name for a cache, one will be created or retrieved:");
        var cacheName = Console.ReadLine();
        
        var grain = clusterClient.GetGrain<IOrleansCache<string>>(cacheName);
        
        ConsoleHelpers.LineSeparator();
        
        Console.WriteLine($"Enter a key to set the value for within the {grain.GetPrimaryKeyString()} grain.");
        var key = Console.ReadLine();

        ConsoleHelpers.LineSeparator();
        
        Console.WriteLine("What value would you like to set?");
        var value = Console.ReadLine();

        await grain.AddOrUpdate(key, value);
        
        Console.WriteLine("Done.");
        
        ConsoleHelpers.ReturnToMenu();
    }
}
