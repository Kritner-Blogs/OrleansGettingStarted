using System;
using System.Text;
using System.Threading.Tasks;
using Kritner.OrleansGettingStarted.GrainInterfaces;
using Orleans;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples
{
    public class DependencyInjectionEmailService : IOrleansFunction
    {
        public string Description => "Shows off dependency injection within a grain implementation.";

        public async Task PerformFunction(IClusterClient clusterClient)
        {
            var grain = clusterClient.GetGrain<IEmailSenderGrain>(Guid.NewGuid());
            Console.WriteLine("Sending out a totally legit email using whatever service is registered with the IEmailSender on the SiloHost");

            var body = @"
                  .-'---`-.
                ,'          `.
                |             \
                |              \
                \           _  \
                ,\  _    ,'-,/-)\
                ( * \ \,' ,' ,'-)
                 `._,)     -',-')
                   \/         ''/
                    )        / /
                   /       ,'-'
            ";

            await grain.SendEmail(
                "someDude@somePlace.com", 
                new[] { "someOtherDude@someOtherPlace.com" }, 
                "ayyy lmao",
                body
            );
        }
    }
}
