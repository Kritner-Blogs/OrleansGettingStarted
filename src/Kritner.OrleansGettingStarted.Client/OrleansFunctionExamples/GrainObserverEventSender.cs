using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kritner.OrleansGettingStarted.Client.Helpers;
using Kritner.OrleansGettingStarted.GrainInterfaces;
using Orleans;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples
{
    public class GrainObserverEventSender : IOrleansFunction
    {
        public string Description => "This function can be used to send a message to subscribed observers.";

        public async Task PerformFunction(IClusterClient clusterClient)
        {
            var grain = clusterClient.GetGrain<IObserverManager>(0);

            Console.WriteLine("Enter a message to send to subscribed observers.");
            var message = Console.ReadLine();

            await grain.SendMessageToObservers(message);

            ConsoleHelpers.ReturnToMenu();
        }
    }
}
