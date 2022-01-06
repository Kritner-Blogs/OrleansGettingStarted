using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Kritner.OrleansGettingStarted.Client.Helpers;
using Orleans;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples;

public class MultipleInstantiations : IOrleansFunction
{
    public string Description => "Demonstrates multiple instances of the same grain.";

    public async Task PerformFunction(IClusterClient clusterClient)
    {
        var grain = clusterClient.GetGrain<IHelloWorld>(Guid.NewGuid());
        var grain2 = clusterClient.GetGrain<IHelloWorld>(Guid.NewGuid());

        Console.WriteLine($"{await grain.SayHello("1")}");
        Console.WriteLine($"{await grain2.SayHello("2")}");
        Console.WriteLine($"{await grain.SayHello("3")}");

        ConsoleHelpers.ReturnToMenu();
    }
}