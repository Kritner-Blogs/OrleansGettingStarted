using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kritner.OrleansGettingStarted.Client.Helpers;
using Kritner.OrleansGettingStarted.GrainInterfaces;
using Orleans;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples
{
    public class ShowoffDashboard : IOrleansFunction
    {
        public string Description => "Starts new activations of several grains, as to show off the OrleansDashboard.";

        public async Task PerformFunction(IClusterClient clusterClient)
        {
            Console.WriteLine("How many activations would you like to do per grain? (100-500 perhaps?)");
            var times = Console.ReadLine();

            if (!int.TryParse(times, out var result))
            {
                Console.WriteLine("invalid input, returning to menu.");
                ConsoleHelpers.ReturnToMenu();
            }

            Console.WriteLine($"About to start {result} instances of a grain. Hold onto your butts.");
            Console.WriteLine("Press any key to get started.");

            for (int i = 0; i < result; i++)
            {
                var grain = clusterClient.GetGrain<IHelloWorld>(Guid.NewGuid());
                await grain.SayHello(i.ToString());
            }

            Console.WriteLine("All done!");
            ConsoleHelpers.ReturnToMenu();
        }
    }
}
