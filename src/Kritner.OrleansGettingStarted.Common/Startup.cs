using Kritner.OrleansGettingStarted.Common.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kritner.OrleansGettingStarted.Common
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        public Startup(
            IHostingEnvironment hostingEnvironment, 
            IConfigurationBuilder configurationBuilder
        )
        {
            HostingEnvironment = hostingEnvironment;
            Configuration = configurationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddOptions();
            serviceCollection.Configure<OrleansConfig>(Configuration.GetSection(nameof(OrleansConfig)));
        }
    }
}
