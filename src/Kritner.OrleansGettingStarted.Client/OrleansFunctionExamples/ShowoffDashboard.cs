using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Kritner.OrleansGettingStarted.Client.Helpers;
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
            Console.ReadKey();

            for (int i = 0; i < result; i++)
            {
                var helloGrain = clusterClient.GetGrain<IHelloWorld>(
                    Guid.NewGuid()
                );
                await helloGrain.SayHello(i.ToString());

                var statefulGrain = clusterClient.GetGrain<IVisitTracker>(
                    i.ToString()
                );
                await statefulGrain.Visit();
            }

            Console.WriteLine("All done!");
            ConsoleHelpers.ReturnToMenu();
        }
    }
}
