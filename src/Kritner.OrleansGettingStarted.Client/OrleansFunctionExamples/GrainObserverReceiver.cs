using System;
using System.Threading.Tasks;
using Kritner.OrleansGettingStarted.Client.Helpers;
using Kritner.OrleansGettingStarted.GrainInterfaces;
using Orleans;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples
{
    public class GrainObserverReceiver : IOrleansFunction, IObserverSample
    {
        private bool _shouldBreakLoop;

        public string Description => "Acts as a receiver of observed messages.  When the observer manager notifies subscribed observers like this class, they take action on the notification.";

        public async Task PerformFunction(IClusterClient clusterClient)
        {
            Console.WriteLine("Observing for behavior, stops once behavior observed.");

            var observerManager = clusterClient.GetGrain<IObservableManager>(0);
            var observerRef = await clusterClient
                .CreateObjectReference<IObserverSample>(this);

            while (!_shouldBreakLoop)
            {
                await observerManager.Subscribe(observerRef);
                await Task.Delay(5000);
            }

            await observerManager.Unsubscribe(observerRef);

            ConsoleHelpers.ReturnToMenu();
        }

        public void ReceiveMessage(string message)
        {
            ConsoleHelpers.LineSeparator();
            Console.WriteLine("Observed Behavior:");
            Console.WriteLine(message);

            _shouldBreakLoop = true;
        }
    }
}
