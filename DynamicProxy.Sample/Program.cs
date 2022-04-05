using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DynamicProxy.Sample
{
    public class Program
    {
        public static async Task Main(params string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingletonProxy<ICustomerRepository, CustomerRepository, LoggerDispatcherEvent>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}