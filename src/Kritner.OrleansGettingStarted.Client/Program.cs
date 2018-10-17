using Kritner.OrleansGettingStarted.GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.Client
{
    public class Program
    {
        const int initializeAttemptsBeforeFailing = 5;
        private static int attempt = 0;

        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await StartClientWithRetries())
                {
                    await DoClientWork(client);
                    await DoStatefulWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> StartClientWithRetries()
        {
            attempt = 0;
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "HelloWorldApp";
                })
                //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IHelloWorld).Assembly).WithReferences())
                // I don't want the chatter of logging from the client for now.
                //.ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect(RetryFilter);
            Console.WriteLine("Client successfully connect to silo host");
            return client;
        }

        private static async Task<bool> RetryFilter(Exception exception)
        {
            if (exception.GetType() != typeof(SiloUnavailableException))
            {
                Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
                return false;
            }
            attempt++;
            Console.WriteLine($"Cluster client attempt {attempt} of {initializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
            if (attempt > initializeAttemptsBeforeFailing)
            {
                return false;
            }
            await Task.Delay(TimeSpan.FromSeconds(4));
            return true;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            // example of calling grains from the initialized client
            var grain = client.GetGrain<IHelloWorld>(Guid.NewGuid());
            var grain2 = client.GetGrain<IHelloWorld>(Guid.NewGuid());

            Console.WriteLine($"{await grain.SayHello("1")}");
            Console.WriteLine($"{await grain2.SayHello("2")}");
            Console.WriteLine($"{await grain.SayHello("3")}");

            PrintSeparatorThing();
        }

        private static async Task DoStatefulWork(IClusterClient client)
        {
            var kritnerGrain = client.GetGrain<IVisitTracker>("kritner@gmail.com");
            var notKritnerGrain = client.GetGrain<IVisitTracker>("notKritner@gmail.com");

            await PrettyPrintGrainVisits(kritnerGrain);
            await PrettyPrintGrainVisits(notKritnerGrain);

            PrintSeparatorThing();
            Console.WriteLine("Ayyy some people are visiting!");

            await kritnerGrain.Visit();
            await kritnerGrain.Visit();
            await notKritnerGrain.Visit();

            PrintSeparatorThing();

            await PrettyPrintGrainVisits(kritnerGrain);
            await PrettyPrintGrainVisits(notKritnerGrain);

            PrintSeparatorThing();
            Console.Write("ayyy kritner's visiting even more!");

            for (int i = 0; i < 5; i++)
            {
                await kritnerGrain.Visit();
            }

            PrintSeparatorThing();

            await PrettyPrintGrainVisits(kritnerGrain);
            await PrettyPrintGrainVisits(notKritnerGrain);
        }

        private static async Task PrettyPrintGrainVisits(IVisitTracker grain)
        {
            Console.WriteLine($"{grain.GetPrimaryKeyString()} has visited {await grain.GetNumberOfVisits()} times");
        }

        private static void PrintSeparatorThing()
        {
            Console.WriteLine($"{Environment.NewLine}-----{Environment.NewLine}");
        }
    }
}
