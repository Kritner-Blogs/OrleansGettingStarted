using Kritner.OrleansGettingStarted.Client.Helpers;
using Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.Client
{
    public class OrleansExamples
    {
        private const string ESCAPE_STRING = "-1";

        private readonly List<IOrleansFunction> _orleansFunctions = new List<IOrleansFunction>()
        {
            new HelloWorld(),
            new MultipleInstantiations(),
            new StatefulWork()            
        };
        
        public async Task ChooseFunction(IClusterClient clusterClient)
        {
            var input = string.Empty;
            while (input != ESCAPE_STRING)
            {
                Console.WriteLine("Pick a function to use for Orleans demonstration:");
                ConsoleHelpers.LineSeparator();
                for (int i = 0; i < _orleansFunctions.Count; i++)
                {
                    Console.WriteLine($" {i} - {_orleansFunctions[i].Description}");
                }

                Console.WriteLine("-1 - to exit");
                ConsoleHelpers.LineSeparator();
                input = Console.ReadLine();

                if (input == ESCAPE_STRING)
                {
                    Console.WriteLine("Exiting...");
                    return;
                }
                    
                if (!int.TryParse(input, out var inputResult))
                {
                    Console.WriteLine("Invalid Input. Please input a number.");
                    continue;
                }
                
                try
                {
                    await _orleansFunctions[inputResult].PerformFunction(clusterClient);
                    ConsoleHelpers.LineSeparator();
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid Input. Please ensure you pick a number/function from the provided list.");
                    ConsoleHelpers.LineSeparator();
                }
            }

            return;
        }
    }
}
