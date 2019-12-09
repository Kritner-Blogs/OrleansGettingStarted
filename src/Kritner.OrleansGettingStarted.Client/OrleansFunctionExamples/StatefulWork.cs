using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kritner.OrleansGettingStarted.Client.Helpers;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Orleans;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples
{
    public class StatefulWork : IOrleansFunction
    {
        public string Description => "Demonstrates using stateful grains with numerous instantiations.";

        public async Task PerformFunction(IClusterClient clusterClient)
        {
            var kritnerGrain = clusterClient.GetGrain<IVisitTracker>("kritner@gmail.com");
            var notKritnerGrain = clusterClient.GetGrain<IVisitTracker>("notKritner@gmail.com");

            await PrettyPrintGrainVisits(kritnerGrain);
            await PrettyPrintGrainVisits(notKritnerGrain);

            ConsoleHelpers.LineSeparator();
            Console.WriteLine("Ayyy some people are visiting!");

            await kritnerGrain.Visit();
            await kritnerGrain.Visit();
            await notKritnerGrain.Visit();

            ConsoleHelpers.LineSeparator();

            await PrettyPrintGrainVisits(kritnerGrain);
            await PrettyPrintGrainVisits(notKritnerGrain);

            ConsoleHelpers.LineSeparator();
            Console.Write("ayyy kritner's visiting even more!");

            for (int i = 0; i < 5; i++)
            {
                await kritnerGrain.Visit();
            }

            ConsoleHelpers.LineSeparator();

            await PrettyPrintGrainVisits(kritnerGrain);
            await PrettyPrintGrainVisits(notKritnerGrain);

            ConsoleHelpers.ReturnToMenu();
        }

        private static async Task PrettyPrintGrainVisits(IVisitTracker grain)
        {
            Console.WriteLine($"{grain.GetPrimaryKeyString()} has visited {await grain.GetNumberOfVisits()} times");
        }
    }
}
